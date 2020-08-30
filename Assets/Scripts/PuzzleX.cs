using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleX : MonoBehaviour
{
    public int rows = 5;
    public int cols = 5;
    public float tileSize = 1;
    //added a 2d array so the beet can use it
    private GameObject[,] board;
    public GameObject[,] GetBoard()
    {
        return board;
    }
    public int getMaxMoves()
    {
        return maxMoves;
    }

    public GameObject goal;

    private GameObject goalTile;
    public GameObject GetGoal()
    {
        return goalTile;
    }

    private BeetMovement beet;

    [SerializeField] GameObject[] Tiles = new GameObject[25];


    [SerializeField]
    int maxMoves;

    private TileSelector selector;

    private void Awake()
    {
        beet = FindObjectOfType<BeetMovement>();
        board = new GameObject[rows, cols];
        GenerateGrid();
    }
    // Start is called before the first frame update
    void Start()
    {
        selector = FindObjectOfType<TileSelector>();
        selector.SetMoves(maxMoves);
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void GenerateGrid()
    {

        //GameObject referenceTile = (GameObject)Instantiate(Resources.Load("Prefabs/Test Tile"));
        int timesThrough = 0;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject tile = (GameObject)Instantiate(/*referenceTile*/ Tiles[timesThrough], transform);
                float posX = col * tileSize;
                float posY = row * -tileSize;
                tile.transform.localScale = Vector3.one;
                tile.transform.position = new Vector2(posX, posY);
                tile.name = "C" + col + "R" + row + " " + tile.GetComponent<SpriteRenderer>().sprite.name.ToString();
                board[row, col] = tile;
                timesThrough++;
            }
        }
        float gridW = cols * tileSize;
        float gridH = rows * tileSize;

        transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);

        goalTile = board[0, 0];
        board[0, 0].GetComponent<Tiles>().SetGoal(true);
        goalTile.transform.rotation *= Quaternion.Euler(0, 0, 180);
        goalTile.transform.localScale = new Vector2(1.77f, 1.77f);

        ////set up a goal tile (off the board)
        //goalTile = (GameObject)Instantiate(Resources.Load("Prefabs/Goal Tile"), transform);/*new Vector2(Random.Range(0, cols - 1), (rows + 1) * tileSize))*/;
        ////goalTile.transform.position = new Vector2(Random.Range(0, cols - 2), ((-rows/2) * tileSize)-1);
        //goalTile.transform.position = new Vector2(cols / 2, ((rows / 2) * tileSize) + 1);
        //goalTile.transform.rotation *= Quaternion.Euler(0, 0, 180);
        //goalTile.GetComponent<Tiles>().SetGoal(true);

        //rotates the tiles to correct orientation on start
        board[2, 2].transform.rotation *= Quaternion.Euler(0, 0, -90);
        board[4,4].transform.rotation *= Quaternion.Euler(0, 0, -90);
    }
}
