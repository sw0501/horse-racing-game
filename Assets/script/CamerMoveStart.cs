using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerMoveStart : MonoBehaviour
{

    public GameObject camerscript;

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
        camerscript.GetComponent<CameraMove>().enabled = true;
        this.gameObject.SetActive(false);
    }
}
