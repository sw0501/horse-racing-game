using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMoveEnd : MonoBehaviour
{

    public GameObject camerascript;
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
        camerascript.GetComponent<CameraMove>().enabled = false;
        this.gameObject.SetActive(false);
    }
}
