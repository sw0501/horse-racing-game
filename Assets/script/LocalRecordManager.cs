using UnityEngine;

public static class LocalRecordManager
{
    private const string WinCountKeyPrefix = "HorseRace_WinCount_Lane";
    private const string TotalRacesKey = "HorseRace_TotalRaces";
    private const string BestTimeKey = "HorseRace_BestFinishTime";
    private const string PointsKey = "HorseRace_Points";

    // 판을 넘어 유지되어야 하므로 RaceConfig(씬 재로드 시 ResetToDefaults로 초기화됨)가 아니라
    // 여기 PlayerPrefs에 둔다.
    public const int StartingPoints = 1000;

    public static int GetPoints()
    {
        return PlayerPrefs.GetInt(PointsKey, StartingPoints);
    }

    public static void AddPoints(int delta)
    {
        PlayerPrefs.SetInt(PointsKey, Mathf.Max(0, GetPoints() + delta));
        PlayerPrefs.Save();
    }

    // 잔고가 바닥나면 다시 시작할 수 있게 초기 포인트로 되돌린다
    public static void RefillPointsIfEmpty()
    {
        if (GetPoints() < RaceConfig.DefaultBetAmount)
        {
            PlayerPrefs.SetInt(PointsKey, StartingPoints);
            PlayerPrefs.Save();
        }
    }

    public static void RecordRaceResult(int winningLane, float winningTimeSeconds)
    {
        if (!RaceConfig.SaveLocalRecords) return;

        string laneKey = WinCountKeyPrefix + winningLane;
        PlayerPrefs.SetInt(laneKey, PlayerPrefs.GetInt(laneKey, 0) + 1);
        PlayerPrefs.SetInt(TotalRacesKey, PlayerPrefs.GetInt(TotalRacesKey, 0) + 1);

        float bestTime = PlayerPrefs.GetFloat(BestTimeKey, -1f);
        if (bestTime < 0f || winningTimeSeconds < bestTime)
        {
            PlayerPrefs.SetFloat(BestTimeKey, winningTimeSeconds);
        }

        PlayerPrefs.Save();
    }

    public static int GetWinCount(int lane)
    {
        return PlayerPrefs.GetInt(WinCountKeyPrefix + lane, 0);
    }

    public static int GetTotalRaces()
    {
        return PlayerPrefs.GetInt(TotalRacesKey, 0);
    }

    // 기록이 없으면 -1
    public static float GetBestFinishTime()
    {
        return PlayerPrefs.GetFloat(BestTimeKey, -1f);
    }
}
