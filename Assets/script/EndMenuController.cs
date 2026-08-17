using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuController : MonoBehaviour
{
    public GameObject endMenu;
    public GameObject endMenuUIRoot; // 스케일 1인 UI 컨테이너(텍스트/버튼). endMenu와 분리된 오브젝트일 수 있음
    public TMP_Text recordSummaryText; // 선택 사항 — 비워두면 표시 생략
    public TMP_Text betResultText; // 선택 사항 — 비워두면 표시 생략

    public void ShowResults()
    {
        if (endMenu != null)
        {
            endMenu.SetActive(true);
        }

        if (endMenuUIRoot != null)
        {
            endMenuUIRoot.SetActive(true);
        }

        if (RaceConfig.SaveLocalRecords && recordSummaryText != null)
        {
            float bestTime = LocalRecordManager.GetBestFinishTime();
            string bestTimeText = bestTime >= 0f ? bestTime.ToString("F2") + "초" : "-";
            recordSummaryText.text = "누적 " + LocalRecordManager.GetTotalRaces() + "판 플레이 / 최고 기록 " + bestTimeText;
        }

        if (betResultText != null)
        {
            if (RaceConfig.PredictedLane <= 0)
            {
                betResultText.text = "";
            }
            else if (RaceConfig.PredictedLane == RaceConfig.LastWinningLane)
            {
                betResultText.text = "예측 적중! " + RaceConfig.PredictedLane + "번이 우승했습니다.";
            }
            else
            {
                betResultText.text = "예측 실패 — " + RaceConfig.PredictedLane + "번을 찍었지만 " + RaceConfig.LastWinningLane + "번이 우승했습니다.";
            }
        }
    }

    // "처음으로" 버튼의 OnClick에 연결 — 씬을 다시 불러와 모든 상태를 초기화
    public void ReturnToStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
