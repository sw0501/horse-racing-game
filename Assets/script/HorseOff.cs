using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HorseOff : MonoBehaviour
{

    public TMP_Text textMeshPro1;
    public TMP_Text textMeshPro2;
    public TMP_Text textMeshPro3;
    public TMP_Text textMeshPro4;
    public TMP_Text textMeshPro5;

    public GameFlowController gameFlow;
    public PhotoFinishController photoFinish;
    public float photoFinishThresholdSeconds = 0.3f;

    int cnt = 1;
    float firstFinishTime = -1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int placement = cnt;

        if (placement == 1)
        {
            firstFinishTime = Time.time;

            if (int.TryParse(collision.gameObject.tag, out int winningLane))
            {
                RaceConfig.LastWinningLane = winningLane;
                LocalRecordManager.RecordRaceResult(winningLane, Time.time - RaceConfig.RaceStartTime);
            }
        }
        else if (placement == 2 && photoFinish != null && Time.time - firstFinishTime <= photoFinishThresholdSeconds)
        {
            photoFinish.Trigger();
        }

        if(collision.gameObject.tag == "1")
        {
            textMeshPro1.text = cnt.ToString();
            Debug.Log(collision.GetComponentInParent<Horse_Move>().cnt);
        }
        else if (collision.gameObject.tag == "2")
        {
            textMeshPro2.text = cnt.ToString();
        }
        else if(collision.gameObject.tag == "3")
        {
            textMeshPro3.text = cnt.ToString();
        }
        else if (collision.gameObject.tag == "4")
        {
            textMeshPro4.text = cnt.ToString();
        }
        else if (collision.gameObject.tag == "5")
        {
            textMeshPro5.text = cnt.ToString();
        }

        cnt++;

        collision.gameObject.SetActive(false);

        if (cnt - 1 >= RaceConfig.HorseCount && gameFlow != null)
        {
            gameFlow.OnRaceFinished();
        }
    }
}


