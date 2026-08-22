using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimalSelectController : MonoBehaviour
{
    // 인덱스 = AnimalType 순서(0=Horse,1=Bear,2=Boar,3=Deer,4=Fox,5=Wolf,6=Rabbit)와 일치시켜 연결
    public Image[] buttonImages;
    public Color normalColor = new Color(0.55f, 0.38f, 0.18f, 1f);
    public Color selectedColor = new Color(0.85f, 0.65f, 0.25f, 1f);

    // 현재 선택을 글자로도 알려주는 라벨(선택 사항 — 비워두면 생략)
    public TMP_Text selectionLabel;
    public string labelPrefix = "동물 선택";

    // 색상 외 신호 — 선택된 버튼만 살짝 키운다(작은 화면에서 색 차이만으로는 구분이 어려움)
    public float selectedScale = 1.08f;

    // 같은 StartMenu에 붙어 있는 베팅 컨트롤러(선택 사항 — 없으면 RaceConfig에 직접 쓴다)
    public BettingController betting;

    private void OnEnable()
    {
        UpdateHighlight();
    }

    // 동물 선택 버튼 OnClick에 AnimalType 순서(int)와 함께 연결 — 화면 전환 없이 선택만 저장.
    // 레인별로 동물이 달라진 뒤(#28)로는 "응원할 동물"을 고르는 의미이며,
    // 그 동물이 배정된 레인으로 베팅도 함께 옮겨 두 UI가 어긋나지 않게 한다.
    public void SelectAnimal(int animalIndex)
    {
        RaceConfig.SelectedAnimal = (AnimalType)animalIndex;

        int lane = RaceConfig.GetLaneOf(RaceConfig.SelectedAnimal);
        if (lane > 0)
        {
            if (betting == null) betting = GetComponent<BettingController>();
            if (betting != null)
            {
                betting.SetPredictedLane(lane);
            }
            else
            {
                RaceConfig.PredictedLane = lane;
            }
        }

        UpdateHighlight();
    }

    private void UpdateHighlight()
    {
        int selected = (int)RaceConfig.SelectedAnimal;

        if (buttonImages != null)
        {
            for (int i = 0; i < buttonImages.Length; i++)
            {
                if (buttonImages[i] == null) continue;

                bool isSelected = (selected == i);
                buttonImages[i].color = isSelected ? selectedColor : normalColor;
                buttonImages[i].rectTransform.localScale = isSelected ? Vector3.one * selectedScale : Vector3.one;
            }
        }

        if (selectionLabel != null)
        {
            int lane = RaceConfig.GetLaneOf(RaceConfig.SelectedAnimal);
            selectionLabel.text = labelPrefix + " — 현재: "
                + RaceConfig.GetAnimalName(RaceConfig.SelectedAnimal)
                + (lane > 0 ? " (" + lane + "번 레인)" : " (이번 판 미출전)");
        }
    }
}
