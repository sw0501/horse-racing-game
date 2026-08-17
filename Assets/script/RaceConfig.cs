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

public static class RaceConfig
{
    public const int MinHorseCount = 2;
    public const int MaxHorseCount = 5;

    public static int HorseCount = MaxHorseCount;
    public static AnimalType SelectedAnimal = AnimalType.Horse;

    // 게임 시작 화면에서 켜고 끌 수 있는 기능들 (기본값 true = 전부 켜짐)
    public static bool UseAnimalStats = true;
    public static bool UseBoostZones = true;
    public static bool SaveLocalRecords = true;

    // wait.cs가 실제로 말을 움직이기 시작하는 시점에 기록 (완주 기록 계산용)
    public static float RaceStartTime;

    public static void ResetToDefaults()
    {
        HorseCount = MaxHorseCount;
        SelectedAnimal = AnimalType.Horse;
        UseAnimalStats = true;
        UseBoostZones = true;
        SaveLocalRecords = true;
        RaceStartTime = 0f;
    }

    public static void SetHorseCountFromDropdown(int dropdownValue)
    {
        HorseCount = Mathf.Clamp(dropdownValue + MinHorseCount, MinHorseCount, MaxHorseCount);
    }
}
