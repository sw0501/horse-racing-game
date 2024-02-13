using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class wait : MonoBehaviour
{
    public GameObject h1;
    public GameObject h2;
    public GameObject h3;
    public GameObject h4;
    public GameObject h5;
    public GameObject poppy;

    // Start is called before the first frame update

    IEnumerator Wait()
    { 
        yield return new WaitForSeconds(4.5f);

        h1.GetComponent<Animator>().enabled = true;
        h1.GetComponent<Horse_Move>().enabled = true;
        h2.GetComponent<Animator>().enabled = true;
        h2.GetComponent<Horse_Move>().enabled = true;
        h3.GetComponent<Animator>().enabled = true;
        h3.GetComponent<Horse_Move>().enabled = true;
        h4.GetComponent<Animator>().enabled = true;
        h4.GetComponent<Horse_Move>().enabled = true;
        h5.GetComponent<Animator>().enabled = true;
        h5.GetComponent<Horse_Move>().enabled = true;
        poppy.SetActive(true);
        this.gameObject.SetActive(false);

    }

    void Start()
    {
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
