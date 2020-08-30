using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEditor;
using UnityEngine.Assertions.Must;

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
    [SerializeField] Animator mainMenu_Anim;
    [SerializeField] Animator credits_Anim;
    // Start is called before the first frame update
    void Start()
    {
        currentMenu = Menu.main;
        option = FindObjectOfType<Options>();
        canMove = true;
        pointer.transform.position = new Vector2(menuButtons[0].transform.position.x + 3, menuButtons[0].transform.position.y);
        menuSelection = 0;

        pointer.gameObject.SetActive(false);

        rowNumber.gameObject.SetActive(false);
        rowText.enabled = false;
        speedSlider.gameObject.SetActive(false);
        foreach (var item in speedText)
        {
            item.enabled = false;
        }
        StartCoroutine(CursorVisibilityTimer());
    }

    // Update is called once per frame
    void Update()
    {

        switch(currentMenu)
        {
            case Menu.main:
                if (pointer.GetComponent<SpriteRenderer>().sortingOrder != 25)
                {
                    StartCoroutine(HandLayerTimer(25));
                }

                if (Input.GetAxisRaw("Vertical") > 0.1f && canMove && menuSelection > 0)
                {
                    menuSelection--;
                    AudioMan.a_Instance.PlayOneShotByName("UI_Move");
                    pointer.transform.position = new Vector2(menuButtons[menuSelection].transform.position.x + 2, menuButtons[menuSelection].transform.position.y);
                    canMove = false;
                }
                else if (Input.GetAxisRaw("Vertical") < -0.1f && canMove && menuSelection < menuButtons.Length - 1)
                {
                    menuSelection++;
                    AudioMan.a_Instance.PlayOneShotByName("UI_Move");
                    pointer.transform.position = new Vector2(menuButtons[menuSelection].transform.position.x + 2, menuButtons[menuSelection].transform.position.y);
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
                        AudioMan.a_Instance.PlayOneShotByName("UI_Select");
                        mainMenu_Anim.SetBool("Up", false);
                        playSelection = 0;
                        StartCoroutine(SetPlayCursor());
                        s_Anim.SetBool("drop", true);
                        currentMenu = Menu.play;

                        //StartRandom();
                    }
                    else if (menuSelection == 1)
                    {
                        AudioMan.a_Instance.PlayOneShotByName("UI_Select");
                        mainMenu_Anim.SetBool("Up", false);
                        optionRow = 0;
                        optionCol = 0;
                        StartCoroutine(SetOptionCursor());
                        p_Anim.SetBool("drop", true);
                        currentMenu = Menu.options;
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
                    else if(menuSelection == 2)
                    {
                        AudioMan.a_Instance.PlayOneShotByName("UI_Select");
                        credits_Anim.SetBool("drop", true);
                        StartCoroutine(CreditsTimer());
                    }
                    else if (menuSelection == 3)
                    {
                        AudioMan.a_Instance.PlayOneShotByName("UI_Select");
                        mainMenu_Anim.SetBool("Up", false);
                        StartCoroutine(QuitTimer());
                    }
                }


                break;
            case Menu.play:
                if (pointer.GetComponent<SpriteRenderer>().sortingOrder != 23)
                {
                    StartCoroutine(HandLayerTimer(23));
                }
                if (Input.GetAxisRaw("Vertical") > 0.1f && canMove && playSelection > 0)
                {
                    playSelection--;
                    AudioMan.a_Instance.PlayOneShotByName("UI_Move");
                    pointer.transform.position = new Vector2(playButtons[playSelection].transform.position.x + 2, playButtons[playSelection].transform.position.y);
                    canMove = false;
                }
                else if (Input.GetAxisRaw("Vertical") < -0.1f && canMove && playSelection < playButtons.Length - 1)
                {
                    playSelection++;
                    AudioMan.a_Instance.PlayOneShotByName("UI_Move");
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
                        AudioMan.a_Instance.PlayOneShotByName("UI_Select");
                        SceneManager.LoadScene(2); // have first scene in build index 2
                    }
                    else if(playSelection == 1)
                    {
                        AudioMan.a_Instance.PlayOneShotByName("UI_Select");
                        SceneManager.LoadScene(1); // have random scene in build index 1
                    }
                    else if(playSelection == 2)
                    {
                        AudioMan.a_Instance.PlayOneShotByName("UI_Select");
                        currentMenu = Menu.main;
                        StartCoroutine(SetMenuCursor());
                        s_Anim.SetBool("drop", false);
                        mainMenu_Anim.SetBool("Up", true);
                    }
                }

                break;
            case Menu.options:
                //add options for rows here
                if(optionRow == 0)
                {
                    if (Input.GetAxisRaw("Horizontal") > 0.1f && canMove && optionCol < rowsButtons.Length - 1)
                    {
                        optionCol++;
                        AudioMan.a_Instance.PlayOneShotByName("UI_Move");
                        pointer.transform.position = new Vector2(rowsButtons[optionCol].transform.position.x, rowsButtons[optionCol].transform.position.y);
                        canMove = false;
                    }
                    else if (Input.GetAxisRaw("Horizontal") < -0.1f && canMove && optionCol > 0)
                    {
                        optionCol--;
                        AudioMan.a_Instance.PlayOneShotByName("UI_Move");
                        pointer.transform.position = new Vector2(rowsButtons[optionCol].transform.position.x, rowsButtons[optionCol].transform.position.y);
                        canMove = false;
                    }

                    if (Input.GetAxisRaw("Horizontal") >= -0.1f && Input.GetAxisRaw("Horizontal") <= 0.1f)
                    {
                        canMove = true;
                    }

                    if(Input.GetKeyDown(KeyCode.Space))
                    {
                        AudioMan.a_Instance.PlayOneShotByName("UI_Select");
                        if (optionCol == 0)
                            option.SetRows(3);
                        else if (optionCol == 1)
                            option.SetRows(5);
                        else if (optionCol == 2)
                            option.SetRows(7);

                        optionCol = 0;
                        pointer.transform.position = new Vector2(speedButtons[optionCol].transform.position.x + 1, speedButtons[optionCol].transform.position.y);
                        optionRow++;
                    }
                }
                else if(optionRow == 1)
                {
                    if (Input.GetAxisRaw("Horizontal") > 0.1f && canMove && optionCol < rowsButtons.Length - 1)
                    {
                        optionCol++;
                        AudioMan.a_Instance.PlayOneShotByName("UI_Move");
                        pointer.transform.position = new Vector2(speedButtons[optionCol].transform.position.x + 1, speedButtons[optionCol].transform.position.y);
                        canMove = false;
                    }
                    else if (Input.GetAxisRaw("Horizontal") < -0.1f && canMove && optionCol > 0)
                    {
                        optionCol--;
                        AudioMan.a_Instance.PlayOneShotByName("UI_Move");
                        pointer.transform.position = new Vector2(speedButtons[optionCol].transform.position.x + 1, speedButtons[optionCol].transform.position.y);
                        canMove = false;
                    }

                    if (Input.GetAxisRaw("Horizontal") >= -0.1f && Input.GetAxisRaw("Horizontal") <= 0.1f)
                    {
                        canMove = true;
                    }

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        AudioMan.a_Instance.PlayOneShotByName("UI_Select");
                        if (optionCol == 0)
                            option.SetSpeed(0.2f);
                        else if (optionCol == 1)
                            option.SetSpeed(0.5f);
                        else if (optionCol == 2)
                            option.SetSpeed(0.8f);

                        optionCol = 0;
                        pointer.transform.position = new Vector2(backButton.transform.position.x + 1, backButton.transform.position.y);
                        optionRow++;
                    }
                }
                else if(optionRow == 2)
                {
                    if(Input.GetKeyDown(KeyCode.Space))
                    {
                        AudioMan.a_Instance.PlayOneShotByName("UI_Select");
                        currentMenu = Menu.main;
                        StartCoroutine(SetMenuCursor());
                        p_Anim.SetBool("drop", false);
                        mainMenu_Anim.SetBool("Up", true);
                    }                    
                }

                break;

        }

        

    }

    IEnumerator QuitTimer()
    {
        yield return new WaitForSeconds(1.5f);
        Debug.Log("quit");
        Application.Quit();
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

    IEnumerator HandLayerTimer(int x)
    {
        yield return new WaitForSeconds(1.5f);
        pointer.GetComponent<SpriteRenderer>().sortingOrder = x;
   

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
    IEnumerator CursorVisibilityTimer()
    {
        yield return new WaitForSeconds(2.5f);
        pointer.gameObject.SetActive(true);
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
        pointer.transform.position = new Vector2(rowsButtons[0].transform.position.x, rowsButtons[0].transform.position.y);
        //pointer.transform.position = new Vector2(speedButtons[0].transform.position.x + 2, speedButtons[0].transform.position.y);
    }

    IEnumerator SetMenuCursor()
    {
        yield return new WaitForSeconds(1.5f);
        pointer.transform.position = new Vector2(menuButtons[menuSelection].transform.position.x + 3, menuButtons[menuSelection].transform.position.y);
    }

    IEnumerator CreditsTimer()
    {
        yield return new WaitForSeconds(5);
        credits_Anim.SetBool("drop", false);
    }

}
