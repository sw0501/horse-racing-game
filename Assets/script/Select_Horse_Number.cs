using TMPro;
using UnityEngine;

public class Select_Horse_Number : MonoBehaviour
{
    // Dropdown의 OnValueChanged에 연결됨 (실제 확정 값은 GameFlowController가 NextButton 클릭 시점에 다시 읽음)
    public void ChangeHorseNum()
    {
        RaceConfig.SetHorseCountFromDropdown(this.GetComponent<TMP_Dropdown>().value);

        // 참가 레인이 바뀌면 베팅 버튼의 출전/미출전 표시도 따라가야 한다
        BettingController betting = FindFirstObjectByType<BettingController>(FindObjectsInactive.Include);
        if (betting != null)
        {
            betting.RefreshLaneInfo();
        }
    }
}
