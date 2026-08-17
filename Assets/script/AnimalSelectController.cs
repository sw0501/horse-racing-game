using UnityEngine;
using UnityEngine.UI;

public class AnimalSelectController : MonoBehaviour
{
    // 인덱스 = AnimalType 순서(0=Horse,1=Bear,2=Boar,3=Deer,4=Fox,5=Wolf,6=Rabbit)와 일치시켜 연결
    public Image[] buttonImages;
    public Color normalColor = new Color(0.55f, 0.38f, 0.18f, 1f);
    public Color selectedColor = new Color(0.85f, 0.65f, 0.25f, 1f);

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
        if (buttonImages == null) return;

        for (int i = 0; i < buttonImages.Length; i++)
        {
            if (buttonImages[i] == null) continue;
            buttonImages[i].color = ((int)RaceConfig.SelectedAnimal == i) ? selectedColor : normalColor;
        }
    }
}
