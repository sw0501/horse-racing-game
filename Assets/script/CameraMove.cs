using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private const string HorseNamePrefix = "horse_";

    private Horse_Move[] cachedHorses;

    private void Start()
    {
        CacheHorses();
    }

    private void FixedUpdate()
    {
        float averageSpeed = GetAverageActiveHorseSpeed();
        this.transform.position = new Vector3(this.transform.position.x + averageSpeed, this.transform.position.y, this.transform.position.z);
    }

    // horse_1~horse_5 참조를 최초 1회만 GameObject.Find로 캐싱 (매 프레임 Find 호출 방지)
    private void CacheHorses()
    {
        cachedHorses = new Horse_Move[RaceConfig.MaxHorseCount];

        for (int i = 1; i <= RaceConfig.MaxHorseCount; i++)
        {
            GameObject horse = GameObject.Find(HorseNamePrefix + i);
            if (horse != null)
            {
                cachedHorses[i - 1] = horse.GetComponent<Horse_Move>();
            }
        }
    }

    private float GetAverageActiveHorseSpeed()
    {
        if (cachedHorses == null)
        {
            CacheHorses();
        }

        float total = 0f;
        int count = 0;

        for (int i = 0; i < RaceConfig.HorseCount && i < cachedHorses.Length; i++)
        {
            Horse_Move move = cachedHorses[i];
            if (move == null) continue;

            total += move.HorseSpeed;
            count++;
        }

        return count > 0 ? total / count : 0f;
    }
}
