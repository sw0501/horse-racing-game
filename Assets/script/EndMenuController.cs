using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuController : MonoBehaviour
{
    public GameObject endMenu;
    public GameObject endMenuUIRoot; // 스케일 1인 UI 컨테이너(텍스트/버튼). endMenu와 분리된 오브젝트일 수 있음
    public TMP_Text recordSummaryText; // 선택 사항 — 비워두면 표시 생략
    public TMP_Text betResultText; // 선택 사항 — 비워두면 표시 생략

    // 결과 화면에서 1등 레인을 강조 표시하기 위한 순위 텍스트 참조(Ranking.Lane1~5) — 비워두면 강조 생략
    public TMP_Text[] rankTexts;
    public Color winnerColor = new Color(0.85f, 0.65f, 0.25f, 1f);
    public Color normalRankColor = new Color(0.96f, 0.92f, 0.82f, 1f);

    public void ShowResults()
    {
        HighlightWinnerRank();
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
            betResultText.text = BuildBetResult();
        }
    }

    // "처음으로" 버튼의 OnClick에 연결 — 씬을 다시 불러와 모든 상태를 초기화
    public void ReturnToStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // 베팅 결과 + 포인트 정산(#29). 건 포인트는 이미 시작 시 차감됐으므로 적중 시에만 지급한다.
    private string BuildBetResult()
    {
        int lane = RaceConfig.PredictedLane;
        if (lane <= 0)
        {
            return "예측을 하지 않았습니다. (보유 " + LocalRecordManager.GetPoints() + "P)";
        }

        string laneDesc = lane + "번 " + RaceConfig.GetAnimalName(RaceConfig.LaneAnimals[lane]);

        if (lane != RaceConfig.LastWinningLane)
        {
            RaceConfig.LastPayout = 0;
            string lost = RaceConfig.BetAmount > 0 ? " −" + RaceConfig.BetAmount + "P" : "";
            string winner = RaceConfig.LastWinningLane + "번 "
                + RaceConfig.GetAnimalName(RaceConfig.LaneAnimals[RaceConfig.LastWinningLane]);

            return "예측 실패 — " + laneDesc + " 예측, "
                + RaceConfig.WithSubjectParticle(winner) + " 우승했습니다." + lost
                + "\n보유 " + LocalRecordManager.GetPoints() + "P";
        }

        float odds = RaceConfig.GetOdds(lane);
        RaceConfig.LastPayout = Mathf.RoundToInt(RaceConfig.BetAmount * odds);

        if (RaceConfig.LastPayout > 0)
        {
            LocalRecordManager.AddPoints(RaceConfig.LastPayout);
        }

        return "예측 적중! " + laneDesc + " 우승 (×" + odds.ToString("0.0") + ")"
            + (RaceConfig.LastPayout > 0 ? "  +" + RaceConfig.LastPayout + "P" : "")
            + "\n보유 " + LocalRecordManager.GetPoints() + "P";
    }

    // 1등으로 완주한 레인의 순위 텍스트("1")를 금색·큰 글씨로 강조, 나머지는 기본색으로 되돌림
    private void HighlightWinnerRank()
    {
        if (rankTexts == null) return;

        foreach (TMP_Text rankText in rankTexts)
        {
            if (rankText == null) continue;

            bool isWinner = rankText.text == "1";
            rankText.color = isWinner ? winnerColor : normalRankColor;
            rankText.fontStyle = isWinner ? FontStyles.Bold : FontStyles.Normal;
        }
    }
}
