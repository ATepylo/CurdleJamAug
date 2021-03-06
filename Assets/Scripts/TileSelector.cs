﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Tilemaps;

public class TileSelector : MonoBehaviour
{
    public int _turns;
    public float _moveSpeed = 1;
    public Transform movePoint;
    Vector2 _movement;
    Tiles _currentTile;
    private Vector3 _rot = new Vector3(0.0f, 0.0f, -90);
    float buttonCoolDown = 0.25f;
    bool _canTurn = true;
    public LayerMask stopSelector;

    private enum PuzzleState { freePlay, Puzzle}
    private PuzzleState currentState = PuzzleState.freePlay;

    private void OnEnable()
    {
        _currentTile = null;
        movePoint.parent = null;

        if(FindObjectOfType<GridManager>())
        {
            currentState = PuzzleState.freePlay;
        }
        else
        {
            currentState = PuzzleState.Puzzle;
        }

    }

    private int maxMoves = 1000;
    public void SetMoves(int i)
    {
        maxMoves = i;
    } 
 

    void Update()
    {
        _movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, _moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.02f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(_movement.x, 0.0f, 0.0f), 0.2f, stopSelector))
                {
                    movePoint.position += new Vector3(_movement.x, 0.0f, 0.0f);
                }
            }   

            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0.0f, _movement.y, 0.0f), 0.2f, stopSelector))
                {
                    movePoint.position += new Vector3(0.0f, _movement.y, 0.0f);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && _canTurn)
        {
            if(currentState == PuzzleState.freePlay)
            {
                _currentTile.gameObject.transform.DORotate(_rot, 0.2f, RotateMode.WorldAxisAdd); StartCoroutine(InputDelay());
                AudioMan.a_Instance.PlayOneShotByName("Turn");
                _turns++;
            }
            else if(currentState == PuzzleState.Puzzle && _turns < maxMoves && !_currentTile.GetBeetOn())
            {
                _currentTile.gameObject.transform.DORotate(_rot, 0.2f, RotateMode.WorldAxisAdd); StartCoroutine(InputDelay());
                AudioMan.a_Instance.PlayOneShotByName("Turn");
                _turns++;
            }
            
        }
    }

    IEnumerator InputDelay()
    {
        _canTurn = false;
        yield return new WaitForSeconds(buttonCoolDown);
        _canTurn = true;
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.GetComponent<Tiles>()) _currentTile = c.gameObject.GetComponent<Tiles>();
    }
}
