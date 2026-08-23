using TMPro;
using UnityEngine;

public class Select_Horse_Number : MonoBehaviour
{
    // Dropdown의 OnValueChanged에 연결됨 (실제 확정 값은 GameFlowController가 NextButton 클릭 시점에 다시 읽음)
    public void ChangeHorseNum()
    {
        RaceConfig.SetHorseCountFromDropdown(this.GetComponent<TMP_Dropdown>().value);

        // 참가 레인이 바뀌면 베팅 버튼의 출전/미출전 표시도 따라가야 한다.
        // 씬에 BettingController는 하나뿐이라 순서를 따지지 않는 FindAnyObjectByType로 충분하다
        // (FindFirstObjectByType는 instance ID 순서 의존으로 Unity 6에서 deprecated).
        BettingController betting = FindAnyObjectByType<BettingController>(FindObjectsInactive.Include);
        if (betting != null)
        {
            betting.RefreshLaneInfo();
        }
    }
}
