using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjust : MonoBehaviour
{
    private GridManager gridM;
    private Camera cammy;

    public GameObject backGround;
    public GameObject edgeBushes;
    
    // Start is called before the first frame update
    void Start()
    {
        gridM = FindObjectOfType<GridManager>();
        cammy = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gridM.cols == 3)
        {
            cammy.orthographicSize = 3;
            edgeBushes.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            backGround.transform.localScale = new Vector3(1f, 1,0);
        }
        else if (gridM.cols == 5)
        {
            cammy.orthographicSize = 3.7f;
            edgeBushes.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            backGround.transform.localScale = new Vector3(1.2f, 1.2f, 0);
        }
        else if (gridM.cols == 7)
        {
            cammy.orthographicSize = 4.5f;
            edgeBushes.transform.localScale = new Vector3(0.72f, 0.72f, 0.72f);
            backGround.transform.localScale = new Vector3(1.6f, 1.6f, 0);
        }
    }
}
