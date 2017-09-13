using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombEdit : TileEditScript {

    // Use this for initialization
    [SerializeField]private List<InputField> _fields;
    private int _delay = 60;

    private GameObject _tile;
    void Start () {
        Selection = 0;
        Counter = 0;
        Select(_fields[Selection].GetComponent<Image>());
        foreach(InputField field in _fields)
        {
            field.text = field.placeholder.GetComponent<Text>().text;
        }
	}
	
	// Update is called once per frame
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeSelection(1);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeSelection(-1);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PressedLeft = true;
            UpdateSelected(-1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PressedRight = true;
            UpdateSelected(1);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            PressedLeft = false;
            Counter = 0;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            PressedRight = false;
            Counter = 0;
        }

        if (PressedRight)
        {
            if (Counter > _delay)
            {
                UpdateSelected(1);
            }
            else
            {
                Counter++;
            }
        }
        if (PressedLeft)
        {
            if (Counter > _delay)
            {
                UpdateSelected(-1);
            }
            else
            {
                Counter++;
            }
        }

        EditTile(_tile);
    }
    public override void EditTile(GameObject tile)
    {
        if (_tile != tile)
        {
            _tile = tile;
        }
        tile.GetComponent<BombTile>().PowerMultiplier = Convert.ToInt32(_fields[0].text);
        tile.GetComponent<BombTile>().MaxDistance = Convert.ToInt32(_fields[1].text);
        tile.GetComponent<BombTile>().ExplodeTimer = Convert.ToInt32(_fields[2].text);
        tile.GetComponent<BombTile>().ResetTime = Convert.ToInt32(_fields[3].text);
    }

    public override void UpdateSelected(int i)
    {
        int nr = Convert.ToInt32(_fields[Selection].text);
        nr += i;
        if (nr < 0)
            nr = 0;
        _fields[Selection].text = nr.ToString();
    }

    public override void ChangeSelection(int i)
    {
        Deselect(_fields[Selection].GetComponent<Image>());
        Selection+= i;
        if (Selection > _fields.Count - 1)
            Selection = 0;
        else if (Selection<0)
        {
            Selection = _fields.Count - 1;
        }
        //Debug.Log("_selection = "+_selection);
        Select(_fields[Selection].GetComponent<Image>());
    }

    
}
