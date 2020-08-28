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
    }

    // Update is called once per frame
    void Update()
    {
        if (_check != _turnAccess._turns)
        {
            _check = _turnAccess._turns;
            _turnsUsed.text = "Turns Used \n \n" + "<b>" + _check + "</b> /" + maxTurns;
        }
    }
}
