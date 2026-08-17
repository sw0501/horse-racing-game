using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameFlowController : MonoBehaviour
{
    private const string HorseNamePrefix = "horse_";

    public GameObject startMenu;
    public GameObject chooseAnimalMenu;
    public GameObject endMenu;

    public Select_Horse_Number horseNumberSelector;
    public StartLightController startLight;
    public wait waitScript;
    public EndMenuController endMenuController;
    public LiveRankingController liveRanking;

    // 게임 플레이 전 온오프 선택용 토글(StartMenu에 배치). 비워두면 항상 켜진 것으로 간주.
    public Toggle useAnimalStatsToggle;
    public Toggle useBoostZonesToggle;
    public Toggle saveLocalRecordsToggle;

    // 오늘의 날씨 표시(선택 사항)
    public TMP_Text weatherText;

    private void Awake()
    {
        RaceConfig.ResetToDefaults();
        RaceConfig.CurrentWeather = (WeatherType)Random.Range(0, 3);

        if (weatherText != null)
        {
            weatherText.text = "오늘의 날씨: " + RaceConfig.GetWeatherLabel();
        }

        if (startMenu != null) startMenu.SetActive(true);
        if (chooseAnimalMenu != null) chooseAnimalMenu.SetActive(false);
        if (endMenu != null) endMenu.SetActive(false);

        // 신호등은 BeginRace() 직전까지는 화면에 보이면 안 됨(패널 배경이 항상 켜져 있으면 다른 화면과 겹침)
        if (startLight != null) startLight.gameObject.SetActive(false);
    }

    // NextButton의 OnClick에 추가로 연결 (기존 StartMenu.SetActive(false) 호출은 유지)
    public void OnHorseCountConfirmed()
    {
        TMP_Dropdown dropdown = horseNumberSelector.GetComponent<TMP_Dropdown>();
        RaceConfig.SetHorseCountFromDropdown(dropdown.value);
        ApplyActiveLanes();

        RaceConfig.UseAnimalStats = useAnimalStatsToggle == null || useAnimalStatsToggle.isOn;
        RaceConfig.UseBoostZones = useBoostZonesToggle == null || useBoostZonesToggle.isOn;
        RaceConfig.SaveLocalRecords = saveLocalRecordsToggle == null || saveLocalRecordsToggle.isOn;
        // RaceConfig.PredictedLane은 BettingController 버튼 클릭 시 직접 설정됨(여기서 다시 읽지 않음)

        if (chooseAnimalMenu != null)
        {
            chooseAnimalMenu.SetActive(true);
        }
        else
        {
            BeginRace();
        }
    }

    // 동물 선택 버튼(AnimalSelectController)에서 호출
    public void OnAnimalConfirmed(int animalIndex)
    {
        RaceConfig.SelectedAnimal = (AnimalType)animalIndex;
        ApplySelectedAnimal();

        if (chooseAnimalMenu != null)
        {
            chooseAnimalMenu.SetActive(false);
        }

        BeginRace();
    }

    // HorseOff에서 모든 참가 동물이 결승선을 통과하면 호출
    public void OnRaceFinished()
    {
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

    private void ApplySelectedAnimal()
    {
        AnimalStats.Stats stats = AnimalStats.Get(RaceConfig.SelectedAnimal);

        for (int i = 1; i <= RaceConfig.HorseCount; i++)
        {
            GameObject horse = GameObject.Find(HorseNamePrefix + i);
            if (horse == null) continue;

            AnimalVisual visual = horse.GetComponent<AnimalVisual>();
            if (visual != null)
            {
                visual.Apply(RaceConfig.SelectedAnimal);
            }

            if (!RaceConfig.UseAnimalStats) continue;

            Horse_Move move = horse.GetComponent<Horse_Move>();
            if (move == null) continue;

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
