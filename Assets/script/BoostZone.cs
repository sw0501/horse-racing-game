using UnityEngine;

public class BoostZone : MonoBehaviour
{
    // 1보다 크면 부스트, 1보다 작으면 장애물(감속)
    public float speedMultiplier = 1.6f;
    public float durationSeconds = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!RaceConfig.UseBoostZones) return;

        Horse_Move horse = collision.gameObject.GetComponent<Horse_Move>();
        if (horse == null) return;

        horse.ApplySpeedMultiplier(speedMultiplier, durationSeconds);
    }
}
