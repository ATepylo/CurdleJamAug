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

    //public Slider rowsSlider;
    public Dropdown rowNumber;
    public Slider speedSlider;
    public Text rowText;
    public Text speedText;

    [SerializeField] GameObject[] menuButtons;
    [SerializeField] GameObject pointer;
    int menuSelection = 0;

    private bool canMove;
    // Start is called before the first frame update
    void Start()
    {
        option = FindObjectOfType<Options>();
        canMove = true;
        pointer.transform.position = new Vector2(menuButtons[0].transform.position.x + 3, menuButtons[0].transform.position.y);
        menuSelection = 0;

        rowNumber.gameObject.SetActive(false);
        rowText.enabled = false;
        speedSlider.gameObject.SetActive(false);
        speedText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1)
        //{
        //    if (Input.GetAxisRaw("Vertical") == -1)
        //    {
        //        menuSelection++;
        //        pointer.transform.DOMoveY(/*menuButtons[menuSelection-1].transform.position.y*/pointer.transform.position.x -1, 1, false);
        //    }
        //    else if (Input.GetAxisRaw("Vertical") == 1)
        //    {
        //        menuSelection--;
        //        pointer.transform.DOMoveY(/*menuButtons[menuSelection-1].transform.position.y*/pointer.transform.position.x + 1, 1, false);
        //    }
        //}

        if (Input.GetAxisRaw("Vertical") > 0.1f && canMove && menuSelection > 0)
        {
            menuSelection--;
            pointer.transform.position = new Vector2(menuButtons[menuSelection].transform.position.x + 3, menuButtons[menuSelection].transform.position.y);
            canMove = false;
        }
        else if (Input.GetAxisRaw("Vertical") < -0.1f && canMove && menuSelection < menuButtons.Length - 1)
        {
            menuSelection++;
            pointer.transform.position = new Vector2(menuButtons[menuSelection].transform.position.x + 3, menuButtons[menuSelection].transform.position.y);
            canMove = false;
        }


        if (Input.GetAxisRaw("Vertical") >= -0.1f && Input.GetAxisRaw("Vertical") <= 0.1f)
        {
            canMove = true;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(menuSelection == 0)
            {
                StartRandom();
            }
            else if(menuSelection == 1)
            {
                if(rowNumber.gameObject.activeSelf)
                {
                    rowNumber.gameObject.SetActive(false);
                    rowText.enabled = false;
                    speedSlider.gameObject.SetActive(false);
                    speedText.enabled = false;
                }
                else
                {
                    rowNumber.gameObject.SetActive(true);
                    rowText.enabled = true;
                    speedSlider.gameObject.SetActive(true);
                    speedText.enabled = true;
                }
            }
            else if(menuSelection == 2)
            {
                Application.Quit();
            }
        }

    }

    public void StartRandom()
    {
        //sets the options for row/columns and beet speed when starting a random run
        if (rowNumber.value == 0)
            option.SetRows(3);
        else if (rowNumber.value == 1)
            option.SetRows(5);
        else if (rowNumber.value == 2)
            option.SetRows(7);

        option.SetSpeed(speedSlider.value);
        //load the scene with random creation
        SceneManager.LoadScene(1);
    }

    public void StartPuzzleLevel()
    {
        //to load puzzle level
    }

}
