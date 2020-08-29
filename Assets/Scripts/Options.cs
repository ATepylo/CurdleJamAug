using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public static bool created = false;

    private int numberofRows;
    public int GetRows()
    {
        return numberofRows;
    }
    public void SetRows(int i)
    {
        numberofRows = i;
    }

    private float beetSpeed;
    public float GetSpeed()
    {
        return beetSpeed;
    }
    public void SetSpeed(float f)
    {
        beetSpeed = f;
    }


    // Start is called before the first frame update
    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
