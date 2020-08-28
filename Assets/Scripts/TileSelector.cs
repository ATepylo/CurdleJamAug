using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Tilemaps;

public class TileSelector : MonoBehaviour
{
    public float _moveSpeed = 1;
    public Transform movePoint;
    Vector2 _movement;
    Tiles _currentTile;
    private Vector3 _rot = new Vector3(0.0f,0.0f,-90);
    float buttonCoolDown = 0.25f;
    bool _canTurn = true;
    private void OnEnable()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        _movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, _moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.5f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                movePoint.position += new Vector3(_movement.x * 0.5f, 0.0f, 0.0f);
            }
            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                movePoint.position += new Vector3(0.0f, _movement.y/2, 0.0f);
            }
        }
        if (_canTurn)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _currentTile.gameObject.transform.DORotate(_rot, 0.2f, RotateMode.WorldAxisAdd); StartCoroutine(InputDelay());
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
