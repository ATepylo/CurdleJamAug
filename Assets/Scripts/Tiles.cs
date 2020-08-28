using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    public enum TileType { A,B,C,D,Start,Goal};
    public TileType myDesignation;
    //public Transform entry1;
    //public Transform entry2;
    //public Transform entry3;
    //public Transform entry4;
    public Transform[] entryPoints;
    public Transform centre;

    public Sprite sprite;

    public bool beetIsOn;

    public int maxTurns;
    public int turns;

    [SerializeField]
    private bool isGoal;
    public void SetGoal(bool b)
    {
        isGoal = b;
    }
    public bool GetGoal()
    {
        return isGoal;
    }

    public List<GameObject> adjacentTile;

    public int GetEntryPoints()
    {
        return entryPoints.Length;
    }

    public void Rotate()
    {
        transform.rotation *= Quaternion.Euler(0, 0, -90);
    }
}
