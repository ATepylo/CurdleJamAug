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
        nextWaypoint = currentTile.entryPoints[0];
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
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);// to ensure is always visible over the tiles
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
        float minDistance = 10;
        foreach (GameObject tile in gridTiles)
        {
            if(currentTile.name != tile.name)
            {
              
                if (Vector2.Distance(transform.position, tile.transform.position) < minDistance)
                {
                    minDistance = Vector2.Distance(transform.position, tile.transform.position);
                    nextTile = tile.GetComponent<Tiles>();
                }
                
            }            
        }
       
        EntreTile(nextTile);
    }

    //called when a beet entres a new tile to find it the 
    private void EntreTile(Tiles newTile)
    {
        
        currentTile = newTile;
    
        bool currentPoint = false;

        if(currentTile.GetEntryPoints() > 0)
        {
            //gets the entry points and exist points of the tile it's entering (if its not a blank tile)
            //right now just moves to the next left entry point but could try and randomize it
            for(int i = 0; i < currentTile.entryPoints.Length; i++)
            {
                if (Vector2.Distance(transform.position, currentTile.entryPoints[i].position) <= searchDistance + 0.3f)
                {
                    entry = currentTile.entryPoints[i];
                    exit = currentTile.entryPoints[(i + 1) % currentTile.entryPoints.Length];
                    nextWaypoint = currentTile.centre;
                    currentPoint = true;
                }
            }           
            //if the beet runs into a wall will gameover
            if(!currentPoint)
            {
                //add gameoverstate
                currentState = moveState.stopped;
                Debug.Log("dead");
            }
            //if the beet makes it to the last tile wins the game 
            else if(currentPoint && currentTile.GetGoal())
            {
                //add winstate
                currentState = moveState.stopped;
                Debug.Log("winner winner chicken dinner");
            }

            //if (Vector2.Distance(transform.position, currentTile.entryPoints[0].position) <= searchDistance + 0.3f)
            //{
            //    entry = currentTile.entryPoints[0];
            //    exit = currentTile.entryPoints[1];
            //    nextWaypoint = currentTile.centre;
            //}
            //else if (Vector2.Distance(transform.position, currentTile.entryPoints[1].position) <= searchDistance + 0.3f)
            //{
            //    entry = currentTile.entryPoints[1];
            //    exit = currentTile.entryPoints[0];
            //    nextWaypoint = currentTile.centre;
            //}
            //else
            //{
            //    //if it is not at either entry point, the beat will splater at the side of the tile
            //    currentState = moveState.stopped;
            //    Debug.Log("dead");
            //}
        }
        else
        {
            //if it is not at either entry point, the beat will splater at the side of the tile
            currentState = moveState.stopped;
            Debug.Log("dead cuase blank space");
        }

    }

}
