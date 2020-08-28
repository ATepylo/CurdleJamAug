using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetMovement : MonoBehaviour
{
    //have a reference to the grid as well so we can place the beet at a tile to start
    private Tiles currentTile;
    
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
        
        
        //set up the beet on the board
        //currentTile = Grid[0,0];
        //transform.position = currentTile.centre;
        //nextWaypoint = currentTile.entry1; //how can we ensure that the point it heads to is within the board? if we can rotate the piece the beet is on then the player can orient it before the beet starts moving

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
                /*
                foreach(Tiles tile in Grid) // probly a more efficient way to do this
                if(transform.position is on the tile and its ! current tile then call EntreTile with the new tile
                */
            }
        }       
    }


    //want a delay before beet starts moving so that player can 
    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(3);
        currentState = moveState.moving;
    }

    //called when a beet entres a new tile to find it the 
    private void EntreTile(Tiles newTile)
    {
        currentTile = newTile;
        if(Vector2.Distance(transform.position, currentTile.entry1.position) <= searchDistance)
        {
            entry = currentTile.entry1;
            exit = currentTile.entry2;
            nextWaypoint = currentTile.centre;
        }
        else if(Vector2.Distance(transform.position, currentTile.entry2.position) <= searchDistance)
        {
            entry = currentTile.entry2;
            exit = currentTile.entry1;
            nextWaypoint = currentTile.centre;
        }
        else
        {
            //if it is not at either entry point, the beat will splater at the side of the tile
        }
    }

}
