using UnityEngine;

public class BettingController : MonoBehaviour
{
    // StartMenu의 예측 버튼(OnClick에 int 인자로 연결)에서 호출 — 0 = 예측 안함, 1~5 = 레인 번호
    public void SetPredictedLane(int lane)
    {
        RaceConfig.PredictedLane = lane;
    }
}
