using UnityEngine;

public class AnimalSelectController : MonoBehaviour
{
    public GameFlowController flow;

    // 동물 선택 버튼(ChooseName 하위)의 OnClick에 AnimalType 순서(int)와 함께 연결
    // 0=Horse, 1=Bear, 2=Boar, 3=Deer, 4=Fox, 5=Wolf, 6=Rabbit
    public void SelectAnimal(int animalIndex)
    {
        flow.OnAnimalConfirmed(animalIndex);
    }
}
