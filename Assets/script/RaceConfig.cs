using UnityEngine;

public enum AnimalType
{
    Horse,
    Bear,
    Boar,
    Deer,
    Fox,
    Wolf,
    Rabbit
}

public enum WeatherType
{
    Normal,
    Rainy,
    Sunny
}

public static class RaceConfig
{
    public const int MinHorseCount = 2;
    public const int MaxHorseCount = 5;

    public static int HorseCount = MaxHorseCount;
    public static AnimalType SelectedAnimal = AnimalType.Horse;
    public static WeatherType CurrentWeather = WeatherType.Normal;

    // 레인별 동물 종. 인덱스 = 레인 번호(1~5), 0번은 쓰지 않는다.
    // 베팅 전에 정해져야 정보로서 의미가 있으므로 GameFlowController.Awake()에서 배정한다.
    public static AnimalType[] LaneAnimals = new AnimalType[MaxHorseCount + 1];

    // Fox/Rabbit은 Animator Controller가 없어 실제로 배정 가능한 종은 이 5가지뿐
    public static readonly AnimalType[] SelectableAnimals =
    {
        AnimalType.Horse, AnimalType.Bear, AnimalType.Boar, AnimalType.Deer, AnimalType.Wolf
    };

    private static readonly string[] AnimalNames = { "말", "곰", "멧돼지", "사슴", "여우", "늑대", "토끼" };

    public static string GetAnimalName(AnimalType type)
    {
        int i = (int)type;
        return (i >= 0 && i < AnimalNames.Length) ? AnimalNames[i] : "-";
    }

    // 한글 받침 유무에 따라 주격 조사를 붙인다("늑대가" / "곰이").
    // 한글 음절은 U+AC00부터 28개 종성 주기로 배열되므로 나머지가 0이면 받침이 없다.
    public static string WithSubjectParticle(string word)
    {
        if (string.IsNullOrEmpty(word)) return word;

        char last = word[word.Length - 1];
        if (last < 0xAC00 || last > 0xD7A3) return word + "이";

        bool hasFinalConsonant = ((last - 0xAC00) % 28) != 0;
        return word + (hasFinalConsonant ? "이" : "가");
    }

    // 레인마다 서로 다른 종을 무작위 배정(Fisher-Yates). 레인 수가 종 수보다 많을 일은 없다.
    public static void AssignRandomLaneAnimals()
    {
        AnimalType[] pool = (AnimalType[])SelectableAnimals.Clone();
        for (int i = pool.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            AnimalType tmp = pool[i];
            pool[i] = pool[j];
            pool[j] = tmp;
        }

        for (int lane = 1; lane <= MaxHorseCount; lane++)
        {
            LaneAnimals[lane] = pool[(lane - 1) % pool.Length];
        }
    }

    // 해당 종이 배정된 레인 번호. 없으면 0.
    public static int GetLaneOf(AnimalType type)
    {
        for (int lane = 1; lane <= HorseCount; lane++)
        {
            if (LaneAnimals[lane] == type) return lane;
        }

        return 0;
    }

    // 하우스 마진 — 1보다 작을수록 지급이 짜다
    private const float PayoutMargin = 0.85f;

    // 레인의 "강함" — 빠를수록, 덜 넘어질수록 크다
    private static float GetStrength(int lane)
    {
        AnimalStats.Stats s = AnimalStats.Get(LaneAnimals[lane]);
        float avgSpeed = (s.minSpeed + s.maxSpeed) * 0.5f;
        return avgSpeed / Mathf.Max(0.1f, s.fallRiskMultiplier);
    }

    // 배당률 — 참가 레인 전체 강함의 합을 해당 레인의 강함으로 나눈 역비례 값.
    // 이러면 강한 레인일수록 배당이 낮아지고, 참가 마릿수에 따라 기준치도 자연히 따라간다.
    public static float GetOdds(int lane)
    {
        if (lane < 1 || lane > HorseCount) return 0f;

        float total = 0f;
        for (int i = 1; i <= HorseCount; i++)
        {
            total += GetStrength(i);
        }

        float mine = GetStrength(lane);
        if (mine <= 0f || total <= 0f) return 0f;

        float odds = (total / mine) * PayoutMargin;
        return Mathf.Round(Mathf.Clamp(odds, 1.1f, 20f) * 10f) / 10f;
    }

    // 게임 시작 화면에서 켜고 끌 수 있는 기능들 (기본값 true = 전부 켜짐)
    public static bool UseAnimalStats = true;
    public static bool UseBoostZones = true;
    public static bool SaveLocalRecords = true;
    public static bool UseFallRecovery = true;

    // 베팅(예측) 시스템 — 0이면 예측 안 함
    public static int PredictedLane;
    public static int LastWinningLane;

    // 이번 판에 건 포인트와 그 결과 수령액(결과 화면 표시용).
    // 잔고 자체는 판을 넘어 유지되어야 하므로 여기가 아니라 LocalRecordManager(PlayerPrefs)에 둔다.
    public static int BetAmount;
    public static int LastPayout;
    public const int DefaultBetAmount = 100;

    // 고를 수 있는 판돈. 인덱스가 곧 버튼 순서다.
    public static readonly int[] BetAmountOptions = { 100, 500, 1000 };

    // 플레이어가 고른 판돈(잔고가 모자라면 GameFlowController가 낮춰 잡는다)
    public static int SelectedBetAmount = DefaultBetAmount;

    // wait.cs가 실제로 말을 움직이기 시작하는 시점에 기록 (완주 기록 계산용)
    public static float RaceStartTime;

    public static void ResetToDefaults()
    {
        HorseCount = MaxHorseCount;
        SelectedAnimal = AnimalType.Horse;
        CurrentWeather = WeatherType.Normal;
        UseAnimalStats = true;
        UseBoostZones = true;
        SaveLocalRecords = true;
        UseFallRecovery = true;
        PredictedLane = 0;
        LastWinningLane = 0;
        RaceStartTime = 0f;
        BetAmount = 0;
        LastPayout = 0;
        SelectedBetAmount = DefaultBetAmount;
    }

    public static void SetHorseCountFromDropdown(int dropdownValue)
    {
        HorseCount = Mathf.Clamp(dropdownValue + MinHorseCount, MinHorseCount, MaxHorseCount);
    }

    public static string GetWeatherLabel()
    {
        switch (CurrentWeather)
        {
            case WeatherType.Rainy: return "비 (전체 속도 감소)";
            case WeatherType.Sunny: return "맑음 (낙마 확률 감소)";
            default: return "보통";
        }
    }
}
