using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class wait : MonoBehaviour
{
    public GameObject h1;
    public GameObject h2;
    public GameObject h3;
    public GameObject h4;
    public GameObject h5;
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

        h1.GetComponent<Animator>().enabled = true;
        h1.GetComponent<Horse_Move>().enabled = true;
        h2.GetComponent<Animator>().enabled = true;
        h2.GetComponent<Horse_Move>().enabled = true;
        h3.GetComponent<Animator>().enabled = true;
        h3.GetComponent<Horse_Move>().enabled = true;
        h4.GetComponent<Animator>().enabled = true;
        h4.GetComponent<Horse_Move>().enabled = true;
        h5.GetComponent<Animator>().enabled = true;
        h5.GetComponent<Horse_Move>().enabled = true;
        poppy.SetActive(true);
        this.gameObject.SetActive(false);

    }

}
