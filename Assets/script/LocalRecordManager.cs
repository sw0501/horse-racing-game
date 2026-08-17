using UnityEngine;

public static class LocalRecordManager
{
    private const string WinCountKeyPrefix = "HorseRace_WinCount_Lane";
    private const string TotalRacesKey = "HorseRace_TotalRaces";
    private const string BestTimeKey = "HorseRace_BestFinishTime";

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
