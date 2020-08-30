using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleOneScript : MonoBehaviour
{
    public int rows = 3;
    public int cols = 3;
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

    private GameObject goalTile;
    public GameObject GetGoal()
    {
        return goalTile;
    }

    private BeetMovement beet;

    [SerializeField] GameObject[] Tiles = new GameObject[9];


    [SerializeField]
    int maxMoves;

    private TileSelector selector;

    private void Awake()
    {
        beet = FindObjectOfType<BeetMovement>();
        board = new GameObject[rows, cols];
        GenerateGrid();
        //Debug.Log("awake");
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

        //set goal tile to whichever one you want
        goalTile = board[1, 1];
        board[1,1].GetComponent<Tiles>().SetGoal(true);
    }
}
