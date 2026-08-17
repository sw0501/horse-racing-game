using UnityEngine;
using UnityEngine.UI;

public class Select_Horse_Number : MonoBehaviour
{
    // Dropdown의 OnValueChanged에 연결됨 (실제 확정 값은 GameFlowController가 NextButton 클릭 시점에 다시 읽음)
    public void ChangeHorseNum()
    {
        RaceConfig.SetHorseCountFromDropdown(this.GetComponent<Dropdown>().value);
    }
}
