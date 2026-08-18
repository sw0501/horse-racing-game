using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class LiveRankingController : MonoBehaviour
{
    private const string HorseNamePrefix = "horse_";

    public TMP_Text liveRankingText;
    public float updateIntervalSeconds = 0.2f;

    private Horse_Move[] cachedHorses;
    private int[] laneNumbers;
    private List<int> activeIndices = new List<int>();
    private float timer;

    private void Start()
    {
        CacheHorses();

        if (liveRankingText != null)
        {
            liveRankingText.text = "출발 대기 중...";
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < updateIntervalSeconds) return;
        timer = 0f;

        RefreshRanking();
    }

    private void CacheHorses()
    {
        cachedHorses = new Horse_Move[RaceConfig.MaxHorseCount];
        laneNumbers = new int[RaceConfig.MaxHorseCount];

        for (int i = 1; i <= RaceConfig.MaxHorseCount; i++)
        {
            GameObject horse = GameObject.Find(HorseNamePrefix + i);
            if (horse == null) continue;

            cachedHorses[i - 1] = horse.GetComponent<Horse_Move>();
            laneNumbers[i - 1] = i;
        }
    }

    private void RefreshRanking()
    {
        if (cachedHorses == null)
        {
            CacheHorses();
        }

        if (liveRankingText == null)
        {
            return;
        }

        activeIndices.Clear();
        for (int i = 0; i < RaceConfig.HorseCount && i < cachedHorses.Length; i++)
        {
            if (cachedHorses[i] != null)
            {
                activeIndices.Add(i);
            }
        }

        activeIndices.Sort((a, b) =>
            cachedHorses[b].transform.position.x.CompareTo(cachedHorses[a].transform.position.x));

        StringBuilder sb = new StringBuilder();
        for (int rank = 0; rank < activeIndices.Count; rank++)
        {
            int lane = laneNumbers[activeIndices[rank]];
            sb.Append(rank + 1).Append("위: ").Append(lane).Append("번");
            if (rank < activeIndices.Count - 1)
            {
                sb.Append('\n');
            }
        }

        liveRankingText.text = sb.ToString();
    }
}
