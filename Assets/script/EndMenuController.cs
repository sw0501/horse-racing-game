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

        // 정산은 표시와 무관하게 항상 실행한다(#47). betResultText는 선택 필드라
        // 이 안에서 지급하면 참조가 비었을 때 당첨금이 조용히 사라진다.
        string betMessage = SettleBet();

        if (betResultText != null)
        {
            betResultText.text = betMessage;
        }
    }

    // "처음으로" 버튼의 OnClick에 연결 — 씬을 다시 불러와 모든 상태를 초기화
    public void ReturnToStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // 포인트를 두 번 지급하지 않기 위한 빗장. 씬이 리로드되면 함께 사라진다.
    private bool betSettled;

    // 베팅 정산(#29) + 결과 메시지 생성. 건 포인트는 이미 시작 시 차감됐으므로
    // 적중이면 배당만큼 지급하고, 경주가 무효면 판돈을 돌려준다.
    private string SettleBet()
    {
        int lane = RaceConfig.PredictedLane;
        int winningLane = RaceConfig.LastWinningLane;

        if (lane <= 0)
        {
            RaceConfig.LastPayout = 0;
            return "예측을 하지 않았습니다. (보유 " + LocalRecordManager.GetPoints() + "P)";
        }

        string laneDesc = lane + "번 " + RaceConfig.GetAnimalName(RaceConfig.LaneAnimals[lane]);

        // 완주자가 한 명도 없는 판(참가 전원이 영구 낙마해 워치독이 종료시킨 경우).
        // HorseOff는 1등이 결승선을 통과할 때만 LastWinningLane을 쓰므로 0으로 남는다.
        // 이 값을 그대로 쓰면 존재하지 않는 "0번 말"이 우승자로 출력됐다(#46).
        if (winningLane <= 0)
        {
            RaceConfig.LastPayout = 0;

            string refund = "";
            if (RaceConfig.BetAmount > 0 && !betSettled)
            {
                // 경주 자체가 성립하지 않았으므로 판돈은 돌려준다
                LocalRecordManager.AddPoints(RaceConfig.BetAmount);
                betSettled = true;
                refund = "  +" + RaceConfig.BetAmount + "P 환불";
            }

            return "경주 무효 — 완주한 동물이 없습니다." + refund
                + "\n보유 " + LocalRecordManager.GetPoints() + "P";
        }

        if (lane != winningLane)
        {
            RaceConfig.LastPayout = 0;
            string lost = RaceConfig.BetAmount > 0 ? " −" + RaceConfig.BetAmount + "P" : "";
            string winner = winningLane + "번 "
                + RaceConfig.GetAnimalName(RaceConfig.LaneAnimals[winningLane]);

            return "예측 실패 — " + laneDesc + " 예측, "
                + RaceConfig.WithSubjectParticle(winner) + " 우승했습니다." + lost
                + "\n보유 " + LocalRecordManager.GetPoints() + "P";
        }

        float odds = RaceConfig.GetOdds(lane);
        RaceConfig.LastPayout = Mathf.RoundToInt(RaceConfig.BetAmount * odds);

        if (RaceConfig.LastPayout > 0 && !betSettled)
        {
            LocalRecordManager.AddPoints(RaceConfig.LastPayout);
            betSettled = true;
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

            // HorseOff가 "1 <size=55%>사슴</size>" 형태로 쓰므로 앞쪽 등수 토큰만 본다
            string placement = rankText.text.Split(' ')[0];
            bool isWinner = placement == "1";
            rankText.color = isWinner ? winnerColor : normalRankColor;
            rankText.fontStyle = isWinner ? FontStyles.Bold : FontStyles.Normal;
        }
    }
}
