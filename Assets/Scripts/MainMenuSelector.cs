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
    public Image[] speedText;

    //for main menu
    [SerializeField] GameObject[] menuButtons;
    [SerializeField] GameObject pointer;
    int menuSelection = 0;

    //for play menu
    [SerializeField] GameObject[] playButtons;
    int playSelection = 0;

    //for options menu
    [SerializeField] GameObject[] rowsButtons;
    [SerializeField] GameObject[] speedButtons;
    [SerializeField] GameObject backButton;
    int optionRow = 0;
    int optionCol = 0;


    private bool canMove;

    private enum Menu { main, play, options }
    private Menu currentMenu = Menu.main;

    //Options Panel
    [SerializeField] Animator p_Anim;
    [SerializeField] Animator s_Anim;
    // Start is called before the first frame update
    void Start()
    {
        currentMenu = Menu.main;
        option = FindObjectOfType<Options>();
        canMove = true;
        pointer.transform.position = new Vector2(menuButtons[0].transform.position.x + 3, menuButtons[0].transform.position.y);
        menuSelection = 0;

        rowNumber.gameObject.SetActive(false);
        rowText.enabled = false;
        speedSlider.gameObject.SetActive(false);
        foreach (var item in speedText)
        {
            item.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        switch(currentMenu)
        {
            case Menu.main:

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

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (menuSelection == 0)
                    {
                        currentMenu = Menu.play;
                        playSelection = 0;
                        StartCoroutine(SetPlayCursor());
                        s_Anim.SetBool("drop", true);

                        //StartRandom();
                    }
                    else if (menuSelection == 1)
                    {
                        optionRow = 0;
                        optionCol = 0;
                        StartCoroutine(SetOptionCursor());
                        p_Anim.SetBool("drop", false);
                        //if (rowNumber.gameObject.activeSelf)
                        //{
                        //    p_Anim.SetBool("drop", false);
                        //    rowNumber.gameObject.SetActive(false);
                        //    rowText.enabled = false;
                        //    speedSlider.gameObject.SetActive(false);
                        //    foreach (var item in speedText)
                        //    {
                        //        item.enabled = false;
                        //    }
                        //}
                        //else
                        //{
                        //    StartCoroutine(OptionsTimer());
                        //    //rowNumber.gameObject.SetActive(true);
                        //    //rowText.enabled = true;
                        //    //speedSlider.gameObject.SetActive(true);
                        //    //foreach (var item in speedText)
                        //    //{
                        //    //    item.enabled = true;
                        //    //}
                        //}
                    }
                    else if (menuSelection == 2)
                    {
                        Application.Quit();
                    }
                }


                break;
            case Menu.play:
                if (Input.GetAxisRaw("Vertical") > 0.1f && canMove && playSelection > 0)
                {
                    playSelection--;
                    pointer.transform.position = new Vector2(playButtons[playSelection].transform.position.x + 2, playButtons[playSelection].transform.position.y);
                    canMove = false;
                }
                else if (Input.GetAxisRaw("Vertical") < -0.1f && canMove && playSelection < playButtons.Length - 1)
                {
                    playSelection++;
                    pointer.transform.position = new Vector2(playButtons[playSelection].transform.position.x + 2, playButtons[playSelection].transform.position.y);
                    canMove = false;
                }


                if (Input.GetAxisRaw("Vertical") >= -0.1f && Input.GetAxisRaw("Vertical") <= 0.1f)
                {
                    canMove = true;
                }

                if(Input.GetKeyDown(KeyCode.Space))
                {
                    if(playSelection == 0)
                    {
                        SceneManager.LoadScene(2); // have first scene in build index 2
                    }
                    else if(playSelection == 1)
                    {
                        SceneManager.LoadScene(1); // have random scene in build index 1
                    }
                    else if(playSelection == 2)
                    {
                        currentMenu = Menu.main;
                        pointer.transform.position = new Vector2(menuButtons[menuSelection].transform.position.x + 3, menuButtons[menuSelection].transform.position.y);
                        s_Anim.SetBool("drop", false);
                    }
                }

                break;
            case Menu.options:
                //add options for rows here



                break;

        }

        

    }

    IEnumerator OptionsTimer()
    {
        p_Anim.SetBool("drop", true);
        yield return new WaitForSeconds(1.5f);
        rowNumber.gameObject.SetActive(true);
        rowText.enabled = true;
        speedSlider.gameObject.SetActive(true);
        foreach (var item in speedText)
        {
            item.enabled = true;
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

    IEnumerator SetPlayCursor()
    {
        yield return new WaitForSeconds(1.5f);
        pointer.transform.position = new Vector2(playButtons[playSelection].transform.position.x + 2, playButtons[playSelection].transform.position.y);
    }

    IEnumerator SetOptionCursor()
    {
        yield return new WaitForSeconds(1.5f);
        pointer.transform.position = new Vector2(rowsButtons[0].transform.position.x + 2, rowsButtons[0].transform.position.y);
        //pointer.transform.position = new Vector2(speedButtons[0].transform.position.x + 2, speedButtons[0].transform.position.y);
    }

}
