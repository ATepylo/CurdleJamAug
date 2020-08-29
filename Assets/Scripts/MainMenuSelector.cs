using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuSelector : MonoBehaviour
{
    private Options option;

    public Dropdown rowDropDown;
    public Slider speedSlider;

    // Start is called before the first frame update
    void Start()
    {
        option = FindObjectOfType<Options>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            StartRandom();
            Debug.Log(option.GetRows());
        }
    }

    public void StartRandom()
    {
        //sets the options for row/columns and beet speed when starting a random run
        if(rowDropDown.value == 0)
            option.SetRows(3);
        else if (rowDropDown.value == 1)
            option.SetRows(5);
        else if (rowDropDown.value == 2)
            option.SetRows(7);

        option.SetSpeed(speedSlider.value);
        //load the scene with random creation
        //SceneManager.LoadScene(1);
    }

    public void StartPuzzleLevel()
    {
        //to load puzzle level
    }

}
