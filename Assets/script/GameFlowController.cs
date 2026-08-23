using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameFlowController : MonoBehaviour
{
    private const string HorseNamePrefix = "horse_";

    [Header("화면 (메인/게임/종료)")]
    public GameObject startMenu; // 메인 화면 — 별도 화면 느낌의 풀스크린 모달
    public GameObject endMenu;
    public GameObject rankingPanel; // 게임 화면에서만 보이는 순위 HUD(완주 순서)

    public Select_Horse_Number horseNumberSelector;
    public StartLightController startLight;
    public wait waitScript;
    public EndMenuController endMenuController;
    public LiveRankingController liveRanking;

    // 게임 플레이 전 온오프 선택용 토글(메인 화면에 배치). 비워두면 항상 켜진 것으로 간주.
    public Toggle useAnimalStatsToggle;
    public Toggle useBoostZonesToggle;
    public Toggle saveLocalRecordsToggle;
    public Toggle useFallRecoveryToggle;

    // 오늘의 날씨 표시(선택 사항)
    public TMP_Text weatherText;

    private void Awake()
    {
        RaceConfig.ResetToDefaults();
        RaceConfig.CurrentWeather = (WeatherType)Random.Range(0, 3);

        // 드롭다운은 값이 "바뀔 때만" ChangeHorseNum을 호출하므로, 시작 시점에는
        // 화면에 보이는 값(기본 2마리)과 RaceConfig.HorseCount(기본 5)가 어긋난다.
        // 베팅 UI가 참가하지도 않을 레인을 보여주지 않도록 여기서 먼저 맞춘다.
        SyncHorseCountFromDropdown();

        // 베팅 전에 정해져야 판단 근거가 되므로 여기서 배정한다(#28)
        RaceConfig.AssignRandomLaneAnimals();
        LocalRecordManager.RefillPointsIfEmpty();

        if (weatherText != null)
        {
            weatherText.text = "오늘의 날씨: " + RaceConfig.GetWeatherLabel();
        }

        // 메인 화면만 보이고, 게임 화면/종료 화면 요소는 전부 숨김
        if (startMenu != null) startMenu.SetActive(true);
        if (endMenu != null) endMenu.SetActive(false);
        if (rankingPanel != null) rankingPanel.SetActive(false);
        if (liveRanking != null) liveRanking.gameObject.SetActive(false);
        if (startLight != null) startLight.gameObject.SetActive(false);
    }

    // 메인 화면의 "게임 시작" 버튼 OnClick에 연결 — 마릿수/옵션/동물/베팅 선택을 전부 확정하고
    // 메인 화면 → 게임 화면으로 전환
    private void SyncHorseCountFromDropdown()
    {
        if (horseNumberSelector == null) return;

        TMP_Dropdown dropdown = horseNumberSelector.GetComponent<TMP_Dropdown>();
        if (dropdown == null) return;

        RaceConfig.SetHorseCountFromDropdown(dropdown.value);
    }

    public void OnGameStartClicked()
    {
        SyncHorseCountFromDropdown();
        ApplyActiveLanes();

        RaceConfig.UseAnimalStats = useAnimalStatsToggle == null || useAnimalStatsToggle.isOn;
        RaceConfig.UseBoostZones = useBoostZonesToggle == null || useBoostZonesToggle.isOn;
        RaceConfig.SaveLocalRecords = saveLocalRecordsToggle == null || saveLocalRecordsToggle.isOn;
        RaceConfig.UseFallRecovery = useFallRecoveryToggle == null || useFallRecoveryToggle.isOn;
        // RaceConfig.SelectedAnimal / PredictedLane은 각 버튼 클릭 시 이미 저장돼 있음(여기서 다시 읽지 않음)

        ApplyLaneAnimals();

        // 예측한 레인이 있으면 포인트를 건다(#29). 잔고에서 즉시 차감하고,
        // 적중 시 EndMenuController가 배당률만큼 지급한다.
        if (RaceConfig.PredictedLane > 0 && LocalRecordManager.GetPoints() >= RaceConfig.SelectedBetAmount)
        {
            RaceConfig.BetAmount = RaceConfig.SelectedBetAmount;
            LocalRecordManager.AddPoints(-RaceConfig.BetAmount);
        }
        else
        {
            RaceConfig.BetAmount = 0;
        }

        if (startMenu != null) startMenu.SetActive(false);

        // 게임 화면 진입 — 순위 HUD 노출
        if (rankingPanel != null) rankingPanel.SetActive(true);
        if (liveRanking != null)
        {
            liveRanking.gameObject.SetActive(true);
            // OnRaceFinished에서 컴포넌트를 꺼두므로, 씬 리로드 없이 다시 시작하는
            // 경로가 생겨도 HUD가 멈춘 채 남지 않도록 여기서 되살린다
            liveRanking.enabled = true;
        }

        raceInProgress = true;
        BeginRace();
    }

    public HorseOff finishLine;

    private bool raceInProgress;

    // 영구 낙마한 말은 결승선을 통과할 수 없어 HorseOff의 종료 조건이 영원히 성립하지 않는다.
    // (낙마 구간 x=130 ~ 결승선 x=167 사이에서 멈추면 그대로 게임이 멈춰 있었다)
    // 더 이상 아무도 전진할 수 없게 되면 여기서 레이스를 마무리한다.
    private void Update()
    {
        if (!raceInProgress) return;

        for (int i = 1; i <= RaceConfig.HorseCount; i++)
        {
            GameObject horse = GameObject.Find(HorseNamePrefix + i);
            if (horse == null) continue; // 완주해서 비활성화된 말

            Horse_Move move = horse.GetComponent<Horse_Move>();
            if (move == null) continue;

            // 달리고 있거나, 넘어졌어도 다시 일어날 가능성이 남아 있으면 계속 기다린다
            if (!move.IsDown || move.CanStillRecover) return;
        }

        if (finishLine != null)
        {
            finishLine.MarkUnfinishedAsRetired();
        }

        OnRaceFinished();
    }

    // HorseOff에서 모든 참가 동물이 결승선을 통과하면 호출 — 게임 화면 → 종료 화면 전환
    public void OnRaceFinished()
    {
        if (!raceInProgress) return;
        raceInProgress = false;

        if (liveRanking != null)
        {
            liveRanking.enabled = false;
        }

        if (endMenuController != null)
        {
            endMenuController.ShowResults();
        }
        else if (endMenu != null)
        {
            endMenu.SetActive(true);
        }
    }

    private void ApplyActiveLanes()
    {
        for (int i = 1; i <= RaceConfig.MaxHorseCount; i++)
        {
            GameObject horse = GameObject.Find(HorseNamePrefix + i);
            if (horse != null)
            {
                horse.SetActive(i <= RaceConfig.HorseCount);
            }
        }
    }

    private const int BaseFallCheckRange = 50000;

    // 레인마다 서로 다른 동물이 배정되므로(#28), 레인별로 각자의 스탯/외형을 적용한다.
    private void ApplyLaneAnimals()
    {
        for (int i = 1; i <= RaceConfig.HorseCount; i++)
        {
            GameObject horse = GameObject.Find(HorseNamePrefix + i);
            if (horse == null) continue;

            AnimalType animal = RaceConfig.LaneAnimals[i];

            AnimalVisual visual = horse.GetComponent<AnimalVisual>();
            if (visual != null)
            {
                visual.Apply(animal);
            }

            if (!RaceConfig.UseAnimalStats) continue;

            Horse_Move move = horse.GetComponent<Horse_Move>();
            if (move == null) continue;

            AnimalStats.Stats stats = AnimalStats.Get(animal);
            move.minSpeed = stats.minSpeed;
            move.maxSpeed = stats.maxSpeed;
            move.fallCheckRange = Mathf.Max(move.fallCheckTarget + 1, Mathf.RoundToInt(BaseFallCheckRange / stats.fallRiskMultiplier));
        }
    }

    private void BeginRace()
    {
        ApplyWeather();

        if (startLight != null)
        {
            startLight.BeginSequence();
        }
        else
        {
            waitScript.BeginWait();
        }
    }

    // 동물별 스탯 적용 이후, 실제 출발 직전에 날씨 효과를 곱연산으로 추가 적용
    private void ApplyWeather()
    {
        if (RaceConfig.CurrentWeather == WeatherType.Normal) return;

        for (int i = 1; i <= RaceConfig.HorseCount; i++)
        {
            GameObject horse = GameObject.Find(HorseNamePrefix + i);
            if (horse == null) continue;

            Horse_Move move = horse.GetComponent<Horse_Move>();
            if (move == null) continue;

            if (RaceConfig.CurrentWeather == WeatherType.Rainy)
            {
                move.minSpeed *= 0.8f;
                move.maxSpeed *= 0.8f;
            }
            else if (RaceConfig.CurrentWeather == WeatherType.Sunny)
            {
                move.fallCheckRange = Mathf.RoundToInt(move.fallCheckRange * 1.5f);
            }
        }
    }
}
