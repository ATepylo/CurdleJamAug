﻿using DG.Tweening;
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
                if(row == 0 && col == 0) //prevents first square from being blank
                {
                    GameObject tile = (GameObject)Instantiate(/*referenceTile*/ Tiles[Random.Range(0, Tiles.Length-1)], transform);
                    float posX = col * tileSize;
                    float posY = row * -tileSize;
                    tile.transform.localScale = Vector3.one;
                    tile.transform.position = new Vector2(posX, posY);
                    tile.name = "C" + col + "R" + row + " " + tile.GetComponent<SpriteRenderer>().sprite.name.ToString();
                    board[row, col] = tile;
                }
                else if(row == rows - 1 && col == cols - 1) //prevents last square from being blank
                {
                    GameObject tile = (GameObject)Instantiate(/*referenceTile*/ Tiles[Random.Range(0, Tiles.Length-1)], transform);
                    float posX = col * tileSize;
                    float posY = row * -tileSize;
                    tile.transform.localScale = Vector3.one;
                    tile.transform.position = new Vector2(posX, posY);
                    tile.name = "C" + col + "R" + row + " " + tile.GetComponent<SpriteRenderer>().sprite.name.ToString();
                    board[row, col] = tile;
                }
                else
                {
                    GameObject tile = (GameObject)Instantiate(/*referenceTile*/ Tiles[Random.Range(0, Tiles.Length)], transform);
                    float posX = col * tileSize;
                    float posY = row * -tileSize;
                    tile.transform.localScale = Vector3.one;
                    tile.transform.position = new Vector2(posX, posY);
                    tile.name = "C" + col + "R" + row + " " + tile.GetComponent<SpriteRenderer>().sprite.name.ToString();
                    board[row, col] = tile;
                }
                
            }
        }
      //  Destroy(referenceTile);
        float gridW = cols * tileSize;
        float gridH = rows * tileSize;

        transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);

        //added these as first attempt to prevent unsolvable situations
        SetAdjacent();
        CheckForBlanks();

        //sets ups the goal 
        board[rows-1, cols-1].GetComponent<Tiles>().SetGoal(true);
    }

    void CheckGridRules()
    {
        if(board[0,0].gameObject != Resources.Load("Prefabs/Start Tile"))
        {
       
        }
    }

    private void SetAdjacent()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if((row - 1) >= 0)
                    board[row, col].GetComponent<Tiles>().adjacentTile.Add(board[row - 1, col]);
                if ((row + 1) <= rows - 1)
                    board[row, col].GetComponent<Tiles>().adjacentTile.Add(board[row + 1, col]);
                if ((col - 1) >= 0)
                    board[row, col].GetComponent<Tiles>().adjacentTile.Add(board[row, col - 1]);
                if ((col + 1) <= cols - 1)
                    board[row, col].GetComponent<Tiles>().adjacentTile.Add(board[row, col + 1]);

            }
        }
    }

    private void CheckForBlanks()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                int i = 0;
                foreach (GameObject adj in board[row, col].GetComponent<Tiles>().adjacentTile)
                {
                    if (adj == Tiles[4])
                        i++;
                }
                if (i == board[row, col].GetComponent<Tiles>().adjacentTile.Count)
                {
                    board[row, col].GetComponent<Tiles>().adjacentTile[board[row, col].GetComponent<Tiles>().adjacentTile.Count - 1] = (GameObject)Instantiate(/*referenceTile*/ Tiles[Random.Range(0, Tiles.Length - 1)], transform);
                    float posX = col * tileSize;
                    float posY = row * -tileSize;
                    board[row, col].GetComponent<Tiles>().adjacentTile[board[row, col].GetComponent<Tiles>().adjacentTile.Count - 1].transform.localScale = Vector3.one;
                    board[row, col].GetComponent<Tiles>().adjacentTile[board[row, col].GetComponent<Tiles>().adjacentTile.Count - 1].transform.position = new Vector2(posX, posY);
                    board[row, col].GetComponent<Tiles>().adjacentTile[board[row, col].GetComponent<Tiles>().adjacentTile.Count - 1].name = "C" + col + "R" + row + " " + board[row, col].GetComponent<Tiles>().adjacentTile[board[row, col].GetComponent<Tiles>().adjacentTile.Count - 1].GetComponent<SpriteRenderer>().sprite.name.ToString();
                    //board[row, col] = tile;
                }
            }
        }
    }
}
