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

    // Start is called before the first frame update
    void Start()
    {
        LiveOrDie = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        cnt++;

        float die = Random.Range(1, 10000);
        
        
        if(LiveOrDie == 0) 
        {
            if (die == 44)
            {
                LiveOrDie = 1;
            }
        }


        //����ġ �߰�
        // y�� z ���� �����ϰ� x �ุ �����ϰ� ����

        float TempSpeed = Random.Range(0.05f, 0.2f);
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
