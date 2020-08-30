using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billow : MonoBehaviour
{
    Vector3 spawn;
    private void OnEnable()
    {
        spawn = transform.position;
    }
    private void Update()
    {
        transform.position += Vector3.up * 0.1f * Time.deltaTime;
        if (Vector3.Distance(spawn, transform.position) > 5) Destroy(this.gameObject);
    }
}
