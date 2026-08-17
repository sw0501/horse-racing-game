using UnityEngine;
using UnityEngine.UI;

public class BettingController : MonoBehaviour
{
    // 인덱스 = 예측 버튼 순서(0=안함,1~5=레인 번호)와 일치시켜 연결
    public Image[] buttonImages;
    public Color normalColor = new Color(0.55f, 0.38f, 0.18f, 1f);
    public Color selectedColor = new Color(0.85f, 0.65f, 0.25f, 1f);

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
        if (buttonImages == null) return;

        for (int i = 0; i < buttonImages.Length; i++)
        {
            if (buttonImages[i] == null) continue;
            buttonImages[i].color = (RaceConfig.PredictedLane == i) ? selectedColor : normalColor;
        }
    }
}
