using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    float h1speed;
    float h2speed;
    float h3speed;
    float h4speed;
    float h5speed;

    // Start is called before the first frame update
    void Start()
    {
        h1speed = GameObject.Find("horse_1").GetComponent<Horse_Move>().HorseSpeed;
        h2speed = GameObject.Find("horse_2").GetComponent<Horse_Move>().HorseSpeed;
        h3speed = GameObject.Find("horse_3").GetComponent<Horse_Move>().HorseSpeed;
        h4speed = GameObject.Find("horse_4").GetComponent<Horse_Move>().HorseSpeed;
        h5speed = GameObject.Find("horse_5").GetComponent<Horse_Move>().HorseSpeed;
    }

    private float max(float a,float b)
    {
        if (a > b)
        {
            return a;
        }
        else return b;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        h1speed = GameObject.Find("horse_1").GetComponent<Horse_Move>().HorseSpeed;
        h2speed = GameObject.Find("horse_2").GetComponent<Horse_Move>().HorseSpeed;
        h3speed = GameObject.Find("horse_3").GetComponent<Horse_Move>().HorseSpeed;
        h4speed = GameObject.Find("horse_4").GetComponent<Horse_Move>().HorseSpeed;
        h5speed = GameObject.Find("horse_5").GetComponent<Horse_Move>().HorseSpeed;
        float averagespeed = (h1speed + h2speed + h3speed + h4speed + h5speed)/5;
        this.transform.position = new Vector3(this.transform.position.x + averagespeed, this.transform.position.y, this.transform.position.z);
    }
}
