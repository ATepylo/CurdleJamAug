using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Transition : MonoBehaviour
{
    float _transitionSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalMoveY(20, _transitionSpeed, false);

    }

    // Update is called once per frame
    void Update()
    {
        //testing the load next scene transition
        if (Input.GetKeyUp(KeyCode.Space)) transform.DOLocalMoveY(-0.5f, _transitionSpeed, false);
    }
}
