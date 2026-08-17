using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuController : MonoBehaviour
{
    public GameObject endMenu;
    public TMP_Text recordSummaryText; // 선택 사항 — 비워두면 표시 생략

    public void ShowResults()
    {
        if (endMenu != null)
        {
            endMenu.SetActive(true);
        }

        if (RaceConfig.SaveLocalRecords && recordSummaryText != null)
        {
            float bestTime = LocalRecordManager.GetBestFinishTime();
            string bestTimeText = bestTime >= 0f ? bestTime.ToString("F2") + "초" : "-";
            recordSummaryText.text = "누적 " + LocalRecordManager.GetTotalRaces() + "판 플레이 / 최고 기록 " + bestTimeText;
        }
    }

    // "처음으로" 버튼의 OnClick에 연결 — 씬을 다시 불러와 모든 상태를 초기화
    public void ReturnToStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
