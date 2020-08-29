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

    //buttons for gameplay loop
    public Button restart;
    public Button menu;
    public Button quit;
    private GridManager gridM;

    //Timer
    float l_Time = 120;
    [SerializeField] Text t_Text;
    [SerializeField] GameObject t_Size;
    Vector2 t_StartSize;
    // Start is called before the first frame update
    private void OnEnable()
    {
        _turnAccess = FindObjectOfType<TileSelector>();
        if(SceneManager.GetActiveScene().name == "PuzzleOne")
        {
            pOne = FindObjectOfType<PuzzleOneScript>();
            maxTurns = pOne.getMaxMoves();
            _turnsUsed.text = "Turns Used \n \n" + "<b>" + _check + "</b> /" + maxTurns;
        }
        t_StartSize = t_Size.transform.localScale;


        gridM = FindObjectOfType<GridManager>();
        restart.gameObject.SetActive(false);
        menu.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(WaitForStart());
        if (_check != _turnAccess._turns)
        {
            _check = _turnAccess._turns;
            _turnsUsed.text = "Turns Used \n \n" + "<b>" + _check + "</b> /" + maxTurns;
        }
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
        t_Size.transform.position = Vector3.Lerp(t_StartSize, new Vector3(t_Size.transform.position.x, t_Size.transform.position.y + 5, t_Size.transform.position.z), Mathf.PingPong(Time.time, .5f));
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


    public void ShowButtons()
    {
        restart.gameObject.SetActive(true);
        menu.gameObject.SetActive(true);
        quit.gameObject.SetActive(true);
    }


    public void Restart()
    {
        gridM.ResetGrid();
        restart.gameObject.SetActive(false);
        menu.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
