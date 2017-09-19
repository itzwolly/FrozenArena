using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniBoostEdit : TileEditScript {

    [SerializeField] private List<InputField> _fields;
    [SerializeField] private List<Dropdown> _dropFields;
    private int _delay = 60;
    // Use this for initialization
    private GameObject _tile;
    void Start()
    {
        Selection = 0;
        Counter = 0;
        Select(_fields[Selection].GetComponent<Image>());
        foreach (InputField field in _fields)
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
        tile.GetComponent<OneWayBoost>().SpeedBoost = Convert.ToSingle(_fields[0].text);
        tile.GetComponent<OneWayBoost>().Direction = (OneWayBoost.DirectionValue)_dropFields[0].value;
    }
    public override void UpdateSelected(int i)
    {
        float nr;
        if (Selection < _fields.Count)
            nr = Convert.ToSingle(_fields[Selection].text);
        else
            nr = _dropFields[Selection - _fields.Count].value;
        nr += i;
        if (nr < 0)
            nr = 0;
        if (Selection < _fields.Count)
            _fields[Selection].text = nr.ToString();
        else
            _dropFields[Selection - _fields.Count].value = (int)nr;
    }

    public override void ChangeSelection(int i)
    {
        if (Selection < _fields.Count)
            Deselect(_fields[Selection].GetComponent<Image>());
        else
            Deselect(_dropFields[Selection - _fields.Count].GetComponent<Image>());
        Selection += i;
        if (Selection > (_fields.Count+_dropFields.Count - 1))
            Selection = 0;
        else if (Selection < 0)
        {
            Selection = _fields.Count - 1;
        }
        //Debug.Log("_selection = "+_selection);
        if (Selection < _fields.Count)
            Select(_fields[Selection].GetComponent<Image>());
        else
            Select(_dropFields[Selection - _fields.Count].GetComponent<Image>());
    }
}
