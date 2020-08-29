using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEditor;

public class MainMenuSelector : MonoBehaviour
{
    //******************THIS IS BROKEN!!**********************
    private Options option;

    public Slider rowsSlider;
    public Slider speedSlider;

    [SerializeField] GameObject[] menuButtons;
    [SerializeField] GameObject pointer;
    int menuSelection = 0;
    // Start is called before the first frame update
    void Start()
    {
        option = FindObjectOfType<Options>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1)
        {
            if (Input.GetAxisRaw("Vertical") == -1)
            {
                menuSelection++;
                pointer.transform.DOMoveY(/*menuButtons[menuSelection-1].transform.position.y*/pointer.transform.position.x -1, 1, false);
            }
            else if (Input.GetAxisRaw("Vertical") == 1)
            {
                menuSelection--;
                pointer.transform.DOMoveY(/*menuButtons[menuSelection-1].transform.position.y*/pointer.transform.position.x + 1, 1, false);
            }
        }
    }

    public void StartRandom()
    {
        //sets the options for row/columns and beet speed when starting a random run
        option.SetRows((int)rowsSlider.value);
        option.SetSpeed(speedSlider.value);
        //load the scene with random creation
        SceneManager.LoadScene(1);
    }

    public void StartPuzzleLevel()
    {
        //to load puzzle level
    }

}
