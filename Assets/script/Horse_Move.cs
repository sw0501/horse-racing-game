using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Horse_Move : MonoBehaviour
{
    public int cnt = 0;
    public float HorseSpeed;
    public AnimatorControllerParameter walk;
    int LiveOrDie;
    public bool flag;

    // Start is called before the first frame update
    void Start()
    {
        LiveOrDie = 0;
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        cnt++;

        float die = Random.Range(1, 50000);
        /*float die = Random.Range(1, 100);*/


        if (flag && LiveOrDie == 0) 
        {
            if (die == 44)
            {
                LiveOrDie = 1;
            }
        }


        //가중치 추가
        // y와 z 축을 고정하고 x 축만 랜덤하게 변경

        float TempSpeed = Random.Range(0.02f, 0.2f);
        /*float TempSpeed = Random.Range(0.5f, 1.0f);*/
        HorseSpeed = TempSpeed;

        if (LiveOrDie == 1)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            this.GetComponent<Animator>().enabled= false;
        }

        else
        {
            this.transform.position = new Vector3(this.transform.position.x + TempSpeed, this.transform.position.y, this.transform.position.z);
        }

    }
}
