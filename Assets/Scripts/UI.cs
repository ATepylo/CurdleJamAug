using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    private int _check;
    [SerializeField] Text _turnsUsed;
    TileSelector _turnAccess;
    // Start is called before the first frame update
    private void OnEnable()
    {
        _turnAccess = FindObjectOfType<TileSelector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_check != _turnAccess._turns)
        {
            _check = _turnAccess._turns;
            _turnsUsed.text = "Turns Used \n \n" + "<b>" + _check + "</b>";
        }
    }
}
