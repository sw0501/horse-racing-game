using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BettingController : MonoBehaviour
{
    // 인덱스 = 예측 버튼 순서(0=안함,1~5=레인 번호)와 일치시켜 연결
    public Image[] buttonImages;
    public Color normalColor = new Color(0.55f, 0.38f, 0.18f, 1f);
    public Color selectedColor = new Color(0.85f, 0.65f, 0.25f, 1f);

    // 현재 예측을 글자로도 알려주는 라벨(선택 사항 — 비워두면 생략)
    public TMP_Text selectionLabel;
    public string labelPrefix = "우승 예측";

    // 색상 외 신호 — 선택된 버튼만 살짝 키운다
    public float selectedScale = 1.08f;

    // 인덱스 = buttonImages와 동일(0=안함, 1~5=레인). 각 레인에 배정된 동물과 배당률을 표시한다.
    public TMP_Text[] buttonLabels;
    // 현재 보유 포인트 표시(선택 사항)
    public TMP_Text pointsLabel;

    private void Start()
    {
        RefreshLaneInfo();
    }

    // 레인별 동물/배당률은 판마다 바뀌므로 런타임에 버튼 라벨을 다시 쓴다(#28, #29)
    public void RefreshLaneInfo()
    {
        if (buttonLabels != null)
        {
            for (int lane = 1; lane < buttonLabels.Length && lane <= RaceConfig.HorseCount; lane++)
            {
                if (buttonLabels[lane] == null) continue;

                buttonLabels[lane].text = lane + "번\n"
                    + RaceConfig.GetAnimalName(RaceConfig.LaneAnimals[lane])
                    + "\n×" + RaceConfig.GetOdds(lane).ToString("0.0");
            }
        }

        UpdatePointsLabel();
    }

    private void UpdatePointsLabel()
    {
        if (pointsLabel == null) return;

        pointsLabel.text = "보유 " + LocalRecordManager.GetPoints() + "P"
            + (RaceConfig.PredictedLane > 0 ? "  /  베팅 " + RaceConfig.DefaultBetAmount + "P" : "");
    }

    private void OnEnable()
    {
        UpdateHighlight();
    }

    // StartMenu의 예측 버튼(OnClick에 int 인자로 연결)에서 호출 — 0 = 예측 안함, 1~5 = 레인 번호
    public void SetPredictedLane(int lane)
    {
        RaceConfig.PredictedLane = lane;
        UpdateHighlight();
    }

    private void UpdateHighlight()
    {
        if (buttonImages != null)
        {
            for (int i = 0; i < buttonImages.Length; i++)
            {
                if (buttonImages[i] == null) continue;

                bool isSelected = (RaceConfig.PredictedLane == i);
                buttonImages[i].color = isSelected ? selectedColor : normalColor;
                buttonImages[i].rectTransform.localScale = isSelected ? Vector3.one * selectedScale : Vector3.one;
            }
        }

        if (selectionLabel != null)
        {
            int lane = RaceConfig.PredictedLane;
            selectionLabel.text = labelPrefix + " — 현재: "
                + (lane <= 0
                    ? "예측 안 함"
                    : lane + "번 " + RaceConfig.GetAnimalName(RaceConfig.LaneAnimals[lane])
                      + " (×" + RaceConfig.GetOdds(lane).ToString("0.0") + ")");
        }

        UpdatePointsLabel();
    }
}
