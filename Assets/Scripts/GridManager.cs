using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Tilemaps;
public class GridManager : MonoBehaviour
{

    //TO DO: BUILD ARRAY CONTAINING TILE TYPES AND REPLACE THE RESOURCES.LOAD WITH IT
    public int rows = 3;
    public int cols = 3;
    public float tileSize = 1;
    //added a 2d array so the beet can use it
    private GameObject[,] board;
    public GameObject[,] GetBoard()
    {
        return board;
    }

    [SerializeField] GameObject[] Tiles = new GameObject[6];


    private void Awake()
    {
        board = new GameObject[rows, cols];
        GenerateGrid();
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        //GenerateGrid();
        //board = new GameObject[rows, cols];
        CheckGridRules();
    }

    private void GenerateGrid()
    {
        
        //GameObject referenceTile = (GameObject)Instantiate(Resources.Load("Prefabs/Test Tile"));

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject tile = (GameObject)Instantiate(/*referenceTile*/ Tiles[Random.Range(0,Tiles.Length)], transform);
                float posX = col * tileSize;
                float posY = row * -tileSize;
                tile.transform.localScale = Vector3.one;
                tile.transform.position = new Vector2(posX, posY);
                tile.name = "C" + col + "R" + row + " " + tile.GetComponent<SpriteRenderer>().sprite.name.ToString();
                board[row, col] = tile;
            }
        }
      //  Destroy(referenceTile);
        float gridW = cols * tileSize;
        float gridH = rows * tileSize;

        transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);

        //sets ups the goal 
        board[rows-1, cols-1].GetComponent<Tiles>().SetGoal(true);
    }

    void CheckGridRules()
    {
        if(board[0,0].gameObject != Resources.Load("Prefabs/Start Tile"))
        {
       
        }
    }
}
