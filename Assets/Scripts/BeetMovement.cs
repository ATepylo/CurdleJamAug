using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetMovement : MonoBehaviour
{
    private GridManager grid;
    private PuzzleOneScript p1Grid;
    private PuzzleTwoScript p2Grid;
    private PuzzleX pXGrid;
    public GameObject[,] gridTiles;

    public Tiles currentTile;
    private Tiles nextTile;
    
    [SerializeField]
    private float moveSpeed;
    public float GetSpeed()
    {
        return moveSpeed;
    }

    private Transform entry;
    private Transform exit;
    private Transform nextWaypoint;
    private GameObject goalTile;
    [SerializeField]
    private float searchDistance; //so that the beet knows when it is close to a waypoint


    public enum moveState { moving, stopped, lose, win }
    private moveState currentState = moveState.stopped;
    public moveState GetMoveState()
    {
        return currentState;
    }

    Animator anim;

    private Options option;
    private UI ui;

    //for showing where the beet is going
    public GameObject directionIndicator;
    private Vector3 direction;
    private Vector3 followLocation;
    public Vector3 GetFollowLoc()
    {
        return followLocation;
    }

    private bool coolDown = false;

    // Start is called before the first frame update
    void Start()
    {
        if(FindObjectOfType<Options>())
        {
            option = FindObjectOfType<Options>();
            moveSpeed = option.GetSpeed();
        }
        ui = FindObjectOfType<UI>();

        SetUpBeet();
    }

    public void SetUpBeet()
    {
        coolDown = false;
        currentState = moveState.stopped;
        StartCoroutine(DelayedStart());
        anim = GetComponentInChildren<Animator>();
        //setup the beet on the board
        if(FindObjectOfType<GridManager>())
        {
            grid = FindObjectOfType<GridManager>();
            gridTiles = grid.GetBoard();
            currentTile = grid.startTile.GetComponent<Tiles>();/*gridTiles[0, 0].GetComponent<Tiles>(); *///sets it up on the 0 tile, we can change this if we want to start in other places
            goalTile = grid.GetGoal();
        }
        else if(FindObjectOfType<PuzzleOneScript>())
        {
            p1Grid = FindObjectOfType<PuzzleOneScript>();
            gridTiles = p1Grid.GetBoard();
            currentTile = gridTiles[0, 0].GetComponent<Tiles>();
            goalTile = p1Grid.GetGoal();
        }
        else if (FindObjectOfType<PuzzleTwoScript>())
        {
            p2Grid = FindObjectOfType<PuzzleTwoScript>();
            gridTiles = p2Grid.GetBoard();
            currentTile = gridTiles[2, 2].GetComponent<Tiles>();
            goalTile = p2Grid.GetGoal();
        }
        else if(FindObjectOfType<PuzzleX>())
        {
            pXGrid = FindObjectOfType<PuzzleX>();
            gridTiles = pXGrid.GetBoard();
            currentTile = gridTiles[4, 4].GetComponent<Tiles>();
            goalTile = pXGrid.GetGoal();
        }
        //when you add a new puzzle add it to the above. Only need to add here

        currentTile.SetBeetOn(true);
        nextWaypoint = currentTile.entryPoints[0];
        exit = nextWaypoint;
        transform.position = currentTile.transform.position;

        anim.SetTrigger("Walk");
        anim.SetBool("Lose", false);
        anim.SetBool("Win", false);

    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case moveState.moving:
                Move();
                break;
            case moveState.stopped:
                break;
            case moveState.lose:
                anim.SetBool("Lose", true);
                if (!coolDown)
                {
                    StartCoroutine(ShowLoseUI());
                    coolDown = true;
                }
                break;
            case moveState.win:
                anim.SetBool("Win", true);
                if (!coolDown)
                {
                    StartCoroutine(ShowWinUI());
                    ui.TotalScore();
                    coolDown = true;
                }
                break;
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, nextWaypoint.position, moveSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);// to ensure is always visible over the tiles

        direction = exit.transform.position - transform.position;
        directionIndicator.transform.position = transform.position + direction.normalized * 0.75f;
        followLocation = transform.position - direction.normalized * 0.75f;

        //to switch waypoints
        if(Vector2.Distance(transform.position, nextWaypoint.position) <= searchDistance)
        {
            if(nextWaypoint == currentTile.centre)
            {
                if(currentTile.name == goalTile.name)
                {
                    currentState = moveState.win;
                }
                else
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
        
        if(Vector2.Distance(transform.position, goalTile.transform.position) < minDistance )
        {
            nextTile = goalTile.GetComponent<Tiles>();
        }
        
       
        EntreTile(nextTile);
    }

    //called when a beet entres a new tile to find it the 
    private void EntreTile(Tiles newTile)
    {
        currentTile.SetBeetOn(false);

        currentTile = newTile;

        currentTile.SetBeetOn(true);
    
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
                    //use this to allway turn left
                    //exit = currentTile.entryPoints[(i + 1) % currentTile.entryPoints.Length]; 
                    //use this to chose an exist at random (not the entry point)
                    int randExit = Random.Range(0, currentTile.entryPoints.Length);
                    if(randExit == i) { randExit = (randExit + 1) % currentTile.entryPoints.Length; }
                    exit = currentTile.entryPoints[randExit];

                    nextWaypoint = currentTile.centre;
                    currentPoint = true;
                }
            }

            if (currentTile.GetComponent<Tiles>().GetGoal() && currentPoint)
            {
                nextWaypoint = currentTile.centre;
                //currentState = moveState.win;
            }
            else if(!currentPoint)
            {
                //if it is not at either entry point, the beat will splater at the side of the tile
                currentState = moveState.lose;
                Debug.Log("dead here");
            }

            //if the beet runs into a wall will gameover
            //if(!currentPoint)
            //{
            //    //add gameoverstate
            //    currentState = moveState.lose;
            //    Debug.Log("dead");
            //}
            ////if the beet makes it to the last tile wins the game 
            //else if(currentPoint && currentTile.GetGoal())
            //{
            //    //add winstate
            //    currentState = moveState.win;
            //    Debug.Log("winner winner chicken dinner");
            //}
            

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
            
        }
        else
        {
            //if it is not at either entry point, the beat will splater at the side of the tile
            currentState = moveState.lose;
            //Debug.Log("dead cuase blank space");
        }

    }


    IEnumerator ShowWinUI()
    {
        yield return new WaitForSeconds(2.5f);
        ui.ShowWinButtons();
    }

    IEnumerator ShowLoseUI()
    {
        yield return new WaitForSeconds(2.5f);
        ui.ShowLoseButtons();
    }
}
