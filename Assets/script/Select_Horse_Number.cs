using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_Horse_Number : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.gameObject.GetComponent<Dropdown>().value);
        //this.gameObject.;
    }

    public void ChangeHorseNum()
    {
        Debug.Log(this.GetComponent<Dropdown>().value.ToString());
    }
}
