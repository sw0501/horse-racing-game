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

    // 순위별 팡파레 — index 0 = 1등, 1 = 2등 ... 순위가 배열 길이를 넘어가면 재생 생략(선택 사항)
    public AudioClip[] rankFanfares;

    int cnt = 1;
    float firstFinishTime = -1f;

    // 완주하지 못한 레인(영구 낙마)의 순위 칸을 비워두지 않고 기권 표시로 채운다.
    // GameFlowController가 레이스를 강제 종료할 때 호출한다.
    public void MarkUnfinishedAsRetired()
    {
        TMP_Text[] rankTexts = { textMeshPro1, textMeshPro2, textMeshPro3, textMeshPro4, textMeshPro5 };

        for (int i = 0; i < rankTexts.Length && i < RaceConfig.HorseCount; i++)
        {
            if (rankTexts[i] != null && string.IsNullOrEmpty(rankTexts[i].text))
            {
                rankTexts[i].text = FormatRank("-", i + 1);
            }
        }
    }

    // 레인마다 동물이 다르므로(#28) 등수만으로는 누가 몇 등인지 알 수 없다.
    // 패널이 좁아 동물 이름은 리치 텍스트로 작게 덧붙인다.
    private string FormatRank(string placement, int lane)
    {
        return placement + " <size=55%>" + RaceConfig.GetAnimalName(RaceConfig.LaneAnimals[lane]) + "</size>";
    }

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
            textMeshPro1.text = FormatRank(cnt.ToString(), 1);
        }
        else if (collision.gameObject.tag == "2")
        {
            textMeshPro2.text = FormatRank(cnt.ToString(), 2);
        }
        else if(collision.gameObject.tag == "3")
        {
            textMeshPro3.text = FormatRank(cnt.ToString(), 3);
        }
        else if (collision.gameObject.tag == "4")
        {
            textMeshPro4.text = FormatRank(cnt.ToString(), 4);
        }
        else if (collision.gameObject.tag == "5")
        {
            textMeshPro5.text = FormatRank(cnt.ToString(), 5);
        }

        int fanfareIndex = placement - 1;
        if (rankFanfares != null && fanfareIndex >= 0 && fanfareIndex < rankFanfares.Length && rankFanfares[fanfareIndex] != null)
        {
            AudioSource.PlayClipAtPoint(rankFanfares[fanfareIndex], transform.position);
        }

        cnt++;

        collision.gameObject.SetActive(false);

        if (cnt - 1 >= RaceConfig.HorseCount && gameFlow != null)
        {
            gameFlow.OnRaceFinished();
        }
    }
}


