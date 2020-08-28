using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetMovement : MonoBehaviour
{
    private GridManager grid;
    public GameObject[,] gridTiles;

    public Tiles currentTile;
    private Tiles nextTile;
    
    [SerializeField]
    private float moveSpeed;

    private Transform entry;
    private Transform exit;
    private Transform nextWaypoint;
    [SerializeField]
    private float searchDistance; //so that the beet knows when it is close to a waypoint


    private enum moveState { moving, stopped }
    private moveState currentState = moveState.stopped;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayedStart());

        //setup the beet on the board
        grid = FindObjectOfType<GridManager>();
        gridTiles = grid.GetBoard();
        currentTile = gridTiles[0,0].GetComponent<Tiles>(); //sets it up on the 0 tile, we can change this if we want to start in other places
        nextWaypoint = currentTile.entry1;
        exit = nextWaypoint;
        transform.position = currentTile.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case moveState.moving:
                Move();
                break;
            case moveState.stopped:
                break;
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, nextWaypoint.position, moveSpeed * Time.deltaTime);

        //to switch waypoints
        if(Vector2.Distance(transform.position, nextWaypoint.position) <= searchDistance)
        {
            if(nextWaypoint == currentTile.centre)
            {
                nextWaypoint = exit;
            }
            else if(nextWaypoint == exit)
            {
                FindClosestTile();             
            }
        }       
    }


    //want a delay before beet starts moving so that player can 
    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(3);
        currentState = moveState.moving;
    }

    private void FindClosestTile()
    {
        Debug.Log("finding closest tile");
        float minDistance = 10;
        foreach (GameObject tile in gridTiles)
        {
            if(currentTile.name != tile.name)
            {
                //Debug.Log(tile.name);
                if (Vector2.Distance(transform.position, tile.transform.position) < minDistance)
                {
                    minDistance = Vector2.Distance(transform.position, tile.transform.position);
                    nextTile = tile.GetComponent<Tiles>();
                }
                //Debug.Log(tile.name + " " + Vector2.Distance(transform.position, tile.transform.position));
            }            
        }
        Debug.Log(currentTile.name);
        Debug.Log(nextTile.name);
        EntreTile(nextTile);
    }

    //called when a beet entres a new tile to find it the 
    private void EntreTile(Tiles newTile)
    {
        Debug.Log("entered new tile");
        currentTile = newTile;
        Debug.Log(currentTile);
        if(Vector2.Distance(transform.position, currentTile.entry1.position) <= searchDistance + 0.3f)
        {
            entry = currentTile.entry1;
            exit = currentTile.entry2;
            nextWaypoint = currentTile.centre;
        }
        else if(Vector2.Distance(transform.position, currentTile.entry2.position) <= searchDistance + 0.3f)
        {
            entry = currentTile.entry2;
            exit = currentTile.entry1;
            nextWaypoint = currentTile.centre;
        }
        else
        {
            //if it is not at either entry point, the beat will splater at the side of the tile
            currentState = moveState.stopped;
            Debug.Log("dead");
        }
    }

}
