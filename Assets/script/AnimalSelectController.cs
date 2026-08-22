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

    // 인덱스 = AnimalType 순서
    private static readonly string[] AnimalNames = { "말", "곰", "멧돼지", "사슴", "여우", "늑대", "토끼" };

    private void OnEnable()
    {
        UpdateHighlight();
    }

    // 동물 선택 버튼 OnClick에 AnimalType 순서(int)와 함께 연결 — 화면 전환 없이 선택만 저장
    public void SelectAnimal(int animalIndex)
    {
        RaceConfig.SelectedAnimal = (AnimalType)animalIndex;
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
            string name = (selected >= 0 && selected < AnimalNames.Length) ? AnimalNames[selected] : "-";
            selectionLabel.text = labelPrefix + " — 현재: " + name;
        }
    }
}
