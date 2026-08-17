using System.Collections;
using UnityEngine;

public class StartLightController : MonoBehaviour
{
    // 순서대로 켜질 신호등 오브젝트(예: 빨강 -> 노랑 -> 초록)
    public GameObject[] lights;
    public float intervalSeconds = 1f;
    public wait waitScript;
    public AudioClip lightSound; // 선택 사항 — 점등마다 재생, 비워두면 생략

    public void BeginSequence()
    {
        gameObject.SetActive(true);
        StartCoroutine(RunSequence());
    }

    private IEnumerator RunSequence()
    {
        foreach (GameObject light in lights)
        {
            if (light != null) light.SetActive(false);
        }

        for (int i = 0; i < lights.Length; i++)
        {
            if (lights[i] == null) continue;

            lights[i].SetActive(true);

            if (lightSound != null)
            {
                AudioSource.PlayClipAtPoint(lightSound, transform.position);
            }

            yield return new WaitForSeconds(intervalSeconds);

            if (i < lights.Length - 1)
            {
                lights[i].SetActive(false);
            }
        }

        // 신호등 연출이 이미 출발 대기 역할을 했으므로 wait.cs의 추가 지연은 건너뜀
        waitScript.BeginWait(0f);
        gameObject.SetActive(false);
    }
}
