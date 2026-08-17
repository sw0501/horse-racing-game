using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Horse_Move : MonoBehaviour
{
    public int cnt = 0;
    public float HorseSpeed;
    public AnimatorControllerParameter walk;
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

    // Start is called before the first frame update
    void Start()
    {
        LiveOrDie = 0;
        flag = false;
        startY = this.transform.position.y;
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

    private void FixedUpdate()
    {

        cnt++;

        int die = Random.Range(1, fallCheckRange);

        if (flag && LiveOrDie == 0)
        {
            if (die == fallCheckTarget)
            {
                LiveOrDie = 1;
            }
        }


        //가중치 추가
        // z 축은 고정, x 축은 항상 전진, y 축은 시작 위치 기준 좁은 범위에서 랜덤하게 흔들림

        if (Time.time > speedMultiplierEndTime)
        {
            speedMultiplier = 1f;
        }

        float TempSpeed = Random.Range(minSpeed, maxSpeed) * speedMultiplier;
        HorseSpeed = TempSpeed;

        if (LiveOrDie == 1)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            this.GetComponent<Animator>().enabled= false;
        }

        else
        {
            yOffset += Random.Range(-verticalWobbleSpeed, verticalWobbleSpeed);
            yOffset = Mathf.Clamp(yOffset, -verticalWobbleRange, verticalWobbleRange);

            this.transform.position = new Vector3(this.transform.position.x + TempSpeed, startY + yOffset, this.transform.position.z);
        }

    }
}
