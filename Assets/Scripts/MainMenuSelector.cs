using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuSelector : MonoBehaviour
{
    private Options option;

    public Slider rowsSlider;
    public Slider speedSlider;

    // Start is called before the first frame update
    void Start()
    {
        option = FindObjectOfType<Options>();

    }

    // Update is called once per frame
    void Update()
    {
        
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
