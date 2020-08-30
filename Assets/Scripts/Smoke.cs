using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Smoke : MonoBehaviour
{
    [SerializeField] GameObject[] smoke;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SmokeBillow());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator SmokeBillow()
    {
        yield return new WaitForSeconds(1.8f);
        GameObject i = (GameObject)Instantiate(smoke[Random.Range(0, smoke.Length)], transform);
        StartCoroutine(SmokeBillow());
    }

}
