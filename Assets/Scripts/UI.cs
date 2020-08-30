using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI : MonoBehaviour
{
    private int _check;
    [SerializeField] Text _turnsUsed;
    
    TileSelector _turnAccess;
    int maxTurns;
    PuzzleOneScript pOne;
    PuzzleTwoScript pTwo;
    PuzzleX pX;

    public GameObject selector;
    private int currentSelection;
    private bool canMove;
    //buttons for gameplay loop
    public Button restart;
    public Button menu;
    public Button quit;
    public Button next;
    private List<Button> buttons = new List<Button>(); 
    //public GameObject levelEndButtons;
    private GridManager gridM;

    //Timer
    public float l_Time = 120;
    private float score;
    public float GetScore()
    {
        return score;
    }
    public void AddScore(float f)
    {
        score += f;
    }

    [SerializeField] Text t_Text;
    [SerializeField] Text score_Text;
    [SerializeField] GameObject t_Size;
    Vector2 t_StartSize;
    Vector3 largeScale;
    // Start is called before the first frame update
    private void OnEnable()
    {
        _turnAccess = FindObjectOfType<TileSelector>();
        if(SceneManager.GetActiveScene().name == "PuzzleOne")
        {
            pOne = FindObjectOfType<PuzzleOneScript>();
            maxTurns = pOne.getMaxMoves();
            _turnsUsed.text = _check.ToString() + " / " + maxTurns.ToString();
        }
        else if(SceneManager.GetActiveScene().name == "PuzzleTwo")
        {
            pTwo = FindObjectOfType<PuzzleTwoScript>();
            maxTurns = pTwo.getMaxMoves();
            _turnsUsed.text = _check.ToString() + " / " + maxTurns.ToString();
        }
        else if(SceneManager.GetActiveScene().name == "PuzzleX")
        {
            pX = FindObjectOfType<PuzzleX>();
            maxTurns = pX.getMaxMoves();
            _turnsUsed.text = _check.ToString() + " / " + maxTurns.ToString(); //"Turns Used \n \n" + "<b>" + _check + "</b> /" + maxTurns;
        }


        t_StartSize = t_Size.transform.localScale;
        largeScale = t_Text.transform.localScale * 1.1f;

        gridM = FindObjectOfType<GridManager>();


        currentSelection = 0;
        buttons.Add(restart);
        buttons.Add(menu);
        buttons.Add(quit);
        buttons.Add(next);
        selector.SetActive(false);

        //levelEndButtons.SetActive(false);
        restart.gameObject.SetActive(false);
        menu.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);
        next.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(WaitForStart());
        if (_check != _turnAccess._turns)
        {
            _check = _turnAccess._turns;
            _turnsUsed.text = "<b>" + _check + "</b> /" + maxTurns;
        }


        if (selector.activeSelf)
        {
            if (Input.GetAxisRaw("Vertical") > 0.1f && canMove && currentSelection > 0)
            {
                currentSelection--;
                AudioMan.a_Instance.PlayOneShotByName("UI_Move");
                selector.transform.position = new Vector2(buttons[currentSelection].transform.GetChild(1).transform.position.x - 100, buttons[currentSelection].transform.GetChild(1).transform.position.y);
                canMove = false;
            }
            else if (Input.GetAxisRaw("Vertical") < -0.1f && canMove && currentSelection < buttons.Count - 1)
            {
                currentSelection++;
                AudioMan.a_Instance.PlayOneShotByName("UI_Move");
                selector.transform.position = new Vector2(buttons[currentSelection].transform.GetChild(1).transform.position.x - 100, buttons[currentSelection].transform.GetChild(1).transform.position.y);
                canMove = false;
            }

            if (Input.GetAxisRaw("Vertical") >= -0.1f && Input.GetAxisRaw("Vertical") <= 0.1f)
            {
                canMove = true;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(buttons[currentSelection].gameObject.activeSelf)
                {
                    AudioMan.a_Instance.PlayOneShotByName("UI_Select");
                    buttons[currentSelection].onClick.Invoke();
                }
            }

        }

        score_Text.text = score.ToString();


    }

    void TimerTextFormat()
    {
        l_Time -= Time.deltaTime;
        if (Mathf.Round(l_Time % 60) >= 10)
        {
            t_Text.text = $"{Mathf.Floor((l_Time / 60))}:{Mathf.Floor(l_Time % 60)}";
        }
        if (Mathf.Floor(l_Time % 60) < 10)
        {
            t_Text.text = $"{Mathf.Round((l_Time / 60))}:0{Mathf.Floor(l_Time % 60)}";
        }
        if (l_Time <= 0)
        {
            t_Text.text = "Times Up!";
            
        }
        if (l_Time <= 30)
        {
            Hurry();
        }
    }

    void Hurry()
    {        
        //t_Text.transform.localScale = Vector2.Lerp(t_Text.transform.localScale, largeScale, Mathf.PingPong(Time.time, .5f));
        //t_Size.transform.position = Vector3.Lerp(t_StartSize, new Vector3(t_Size.transform.position.x, t_Size.transform.position.y + 5, t_Size.transform.position.z), Mathf.PingPong(Time.time, .5f));
        t_Text.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 1f));
    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(0.5f);
        if (t_Text != null)
        {
            TimerTextFormat();
        }
    }   

    public void ShowWinButtons()
    {
        //Debug.Log("show them");
        //levelEndButtons.SetActive(true);
        restart.gameObject.SetActive(true);
        menu.gameObject.SetActive(true);
        quit.gameObject.SetActive(true);
        if(!gridM)
        {
            next.gameObject.SetActive(true);
        }
        selector.SetActive(true);
        currentSelection = 0;
        selector.transform.position = new Vector2(buttons[currentSelection].transform.GetChild(1).transform.position.x - 100, buttons[currentSelection].transform.GetChild(1).transform.position.y);
    }

    public void ShowLoseButtons()
    {
        restart.gameObject.SetActive(true);
        menu.gameObject.SetActive(true);
        quit.gameObject.SetActive(true);
        selector.SetActive(true);
        currentSelection = 0;
        selector.transform.position = new Vector2(buttons[currentSelection].transform.GetChild(1).transform.position.x - 100, buttons[currentSelection].transform.GetChild(1).transform.position.y);
    }


    public void Restart()
    {
        if(gridM)
        {
             gridM.ResetGrid();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //levelEndButtons.SetActive(false);
        restart.gameObject.SetActive(false);
        menu.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);
        next.gameObject.SetActive(false);
        selector.SetActive(false);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void Next()
    {
        Debug.Log(SceneManager.sceneCountInBuildSettings);

        if ((SceneManager.GetActiveScene().buildIndex + 1) < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
            SceneManager.LoadScene(0);

    }

    public void TotalScore()
    {
        //Debug.Log(_check);
        //Debug.Log(maxTurns);
        score += (maxTurns - _check) * 100;
    }
}
