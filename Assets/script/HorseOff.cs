using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HorseOff : MonoBehaviour
{

    public TMP_Text textMeshPro1;
    public TMP_Text textMeshPro2;
    public TMP_Text textMeshPro3;
    public TMP_Text textMeshPro4;
    public TMP_Text textMeshPro5;

    int cnt = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "1")
        {
            textMeshPro1.text = cnt.ToString();
            Debug.Log(collision.GetComponentInParent<Horse_Move>().cnt);
        }
        else if (collision.gameObject.tag == "2")
        {
            textMeshPro2.text = cnt.ToString();
        }
        else if(collision.gameObject.tag == "3")
        {
            textMeshPro3.text = cnt.ToString();
        }
        else if (collision.gameObject.tag == "4")
        {
            textMeshPro4.text = cnt.ToString();
        }
        else if (collision.gameObject.tag == "5")
        {
            textMeshPro5.text = cnt.ToString();
        }

        cnt++;

        collision.gameObject.SetActive(false);
        
    }
}


