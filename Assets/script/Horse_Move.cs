using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Horse_Move : MonoBehaviour
{
    public int cnt = 0;
    public float HorseSpeed;
    int LiveOrDie;
    public bool flag;

    public float verticalWobbleRange = 0.3f;
    public float verticalWobbleSpeed = 0.05f;

    public float minSpeed = 0.02f;
    public float maxSpeed = 0.2f;

    public int fallCheckRange = 50000;
    public int fallCheckTarget = 44;

    float startY;
    float yOffset;

    float speedMultiplier = 1f;
    float speedMultiplierEndTime = 0f;

    public AudioClip fallSound; // 선택 사항 — 비워두면 재생 생략

    [Header("낙마 후 재기립")]
    // 낙마한 뒤 이 시간이 지나야 재기립 판정을 시작한다
    public float fallRecoveryDelaySeconds = 2f;
    // 재기립 판정 1회당 성공 확률(0이면 재기립 없음 — 기존 동작과 동일)
    [Range(0f, 1f)]
    public float fallRecoveryChance = 0.35f;
    // 재기립 직후 이 배율로 감속된 채 달린다(따라잡는 맛은 남기되 즉시 복귀는 막음)
    public float fallRecoverySpeedMultiplier = 0.7f;
    public float fallRecoverySlowSeconds = 2f;

    float fallRecoveryCheckTime = -1f;
    bool hasRecovered;

    // Start is called before the first frame update
    void Start()
    {
        LiveOrDie = 0;
        flag = false;
        startY = this.transform.position.y;
        hasRecovered = false;
        fallRecoveryCheckTime = -1f;
    }

    // 낙마해서 멈춰 있는 상태인가
    public bool IsDown
    {
        get { return LiveOrDie == 1; }
    }

    // 아직 재기립 가능성이 남아 있는가. false이고 IsDown이면 이 말은 영원히 못 움직인다.
    public bool CanStillRecover
    {
        get { return !hasRecovered && RaceConfig.UseFallRecovery && fallRecoveryChance > 0f; }
    }

    // BoostZone 등 트랙 위 트리거에서 일시적인 속도 배율을 적용할 때 호출
    // multiplier > 1이면 부스트, < 1이면 감속(장애물)
    public void ApplySpeedMultiplier(float multiplier, float durationSeconds)
    {
        speedMultiplier = multiplier;
        speedMultiplierEndTime = Time.time + durationSeconds;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 낙마한 말이 지연 시간 경과 후 확률적으로 다시 일어난다.
    // 한 경기당 한 번만 복귀할 수 있게 해서, 계속 넘어졌다 일어나며 끝나지 않게 한다.
    private void TryRecoverFromFall()
    {
        if (LiveOrDie != 1) return;
        if (hasRecovered) return;
        if (!RaceConfig.UseFallRecovery) return;
        if (fallRecoveryChance <= 0f) return;
        if (fallRecoveryCheckTime < 0f || Time.time < fallRecoveryCheckTime) return;

        // 판정은 초당 1회 — FixedUpdate마다 굴리면 확률이 사실상 100%가 된다
        fallRecoveryCheckTime = Time.time + 1f;

        if (Random.value > fallRecoveryChance) return;

        LiveOrDie = 0;
        hasRecovered = true;

        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.enabled = true;
        }

        ApplySpeedMultiplier(fallRecoverySpeedMultiplier, fallRecoverySlowSeconds);
    }

    private void FixedUpdate()
    {

        cnt++;

        int die = Random.Range(1, fallCheckRange);

        if (flag && LiveOrDie == 0)
        {
            if (die == fallCheckTarget)
            {
                LiveOrDie = 1;
                fallRecoveryCheckTime = Time.time + fallRecoveryDelaySeconds;

                if (fallSound != null)
                {
                    AudioSource.PlayClipAtPoint(fallSound, transform.position);
                }
            }
        }

        TryRecoverFromFall();


        //가중치 추가
        // z 축은 고정, x 축은 항상 전진, y 축은 시작 위치 기준 좁은 범위에서 랜덤하게 흔들림

        if (Time.time > speedMultiplierEndTime)
        {
            speedMultiplier = 1f;
        }

        if (LiveOrDie == 1)
        {
            // 낙마한 말은 실제로 멈춰 있으므로 속도도 0으로 고정한다.
            // (CameraMove가 이 값의 평균으로 카메라를 움직이기 때문에, 갱신을 계속하면
            //  멈춘 말의 유령 속도가 카메라를 앞서 나가게 만든다)
            HorseSpeed = 0f;
            this.GetComponent<Animator>().enabled= false;
        }

        else
        {
            float TempSpeed = Random.Range(minSpeed, maxSpeed) * speedMultiplier;
            HorseSpeed = TempSpeed;

            yOffset += Random.Range(-verticalWobbleSpeed, verticalWobbleSpeed);
            yOffset = Mathf.Clamp(yOffset, -verticalWobbleRange, verticalWobbleRange);

            this.transform.position = new Vector3(this.transform.position.x + TempSpeed, startY + yOffset, this.transform.position.z);
        }

    }
}
