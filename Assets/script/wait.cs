using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wait : MonoBehaviour
{
    private const string HorseNamePrefix = "horse_";

    public GameObject poppy;

    public float startDelaySeconds = 4.5f;

    // GameFlowController(또는 StartLightController)가 준비 완료 시점에 호출
    public void BeginWait()
    {
        BeginWait(startDelaySeconds);
    }

    public void BeginWait(float delaySeconds)
    {
        StartCoroutine(Wait(delaySeconds));
    }

    IEnumerator Wait(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);

        RaceConfig.RaceStartTime = Time.time;

        StartActiveHorses();

        if (poppy != null)
        {
            poppy.SetActive(true);
        }

        this.gameObject.SetActive(false);
    }

    // 참가 중인 레인의 Animator/Horse_Move를 켜서 실제 출발시킨다.
    // GameFlowController/CameraMove/LiveRankingController와 동일한 조회 방식을 쓴다.
    // (GameObject.Find는 비활성 오브젝트를 찾지 못하므로, GameFlowController.ApplyActiveLanes가
    //  이미 꺼둔 미참가 레인은 여기서 자연스럽게 제외된다)
    private void StartActiveHorses()
    {
        for (int i = 1; i <= RaceConfig.HorseCount; i++)
        {
            GameObject horse = GameObject.Find(HorseNamePrefix + i);
            if (horse == null) continue;

            Animator animator = horse.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = true;
            }

            Horse_Move move = horse.GetComponent<Horse_Move>();
            if (move != null)
            {
                move.enabled = true;
            }
        }
    }
}
