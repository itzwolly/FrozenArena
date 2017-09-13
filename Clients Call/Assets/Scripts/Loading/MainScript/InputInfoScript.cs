﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputInfoScript : TileEditScript
{
    [SerializeField] private List<InputField> _strings;
    [SerializeField] private List<InputField> _fields;
    [SerializeField] private List<Slider> _sliders;
    [SerializeField] private List<GameObject> _slidersBackground;
    private int _delay = 60;

    [SerializeField] private Button StartAccept;
    // Update is called once per frame
    private bool _Started;
    private void Start()
    {
        if (_slidersBackground.Count != (_sliders.Count*3))
            throw new Exception("Did not complete corectly the sliders and sliders background");
        Selection = 0;
        Counter = 0;
        Select(_strings[Selection].GetComponent<Image>());
        foreach (InputField field in _fields)
        {
            field.text = field.placeholder.GetComponent<Text>().text;
        }

        _Started = false;
    }
    void Update () {
		if(Input.GetKeyUp(KeyCode.Space))
        {
            StartAccept.onClick.Invoke();
        }


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
    }
    public override void EditTile(GameObject tile)
    {
    }
    public override void UpdateSelected(int i)
    {
        float nr=0;
        if (Selection < _strings.Count)
        {

        }
        else if (Selection < _strings.Count + _fields.Count)
        {
            nr = Convert.ToSingle(_fields[Selection - _strings.Count].text);
            nr += i;
        }
        else
        {
            nr = Convert.ToSingle(_sliders[Selection - _strings.Count - _fields.Count].value);
            nr += i/10f;
        }
        if (nr < 0)
            nr = 0;
        if (Selection < _strings.Count)
        {

        }
        else if (Selection < (_strings.Count + _fields.Count))
            _fields[Selection - _strings.Count].text = nr.ToString();
        else
            _sliders[Selection - _strings.Count - _fields.Count].value = nr;
    }
    public override void ChangeSelection(int i)
    {
        if (Selection < _strings.Count)
            Deselect(_strings[Selection].GetComponent<Image>());
        else if (Selection < _strings.Count + _fields.Count)
            Deselect(_fields[Selection - _strings.Count].GetComponent<Image>());
        else
        {
            Deselect(_slidersBackground[(Selection - _strings.Count - _fields.Count) * 3].GetComponent<Image>());
            Deselect(_slidersBackground[(Selection - _strings.Count - _fields.Count) * 3+1].GetComponent<Image>());
            Deselect(_slidersBackground[(Selection - _strings.Count - _fields.Count) * 3+2].GetComponent<Image>());
        }
        Selection += i;
        if (Selection > _fields.Count +_sliders.Count+_strings.Count-1)
            Selection = 0;
        else if (Selection < 0)
        {
            Selection = _fields.Count+_sliders.Count+_strings.Count - 1;
        }
        Debug.Log("_selection = "+Selection);
        if (Selection < _strings.Count)
            Select(_strings[Selection].GetComponent<Image>());
        else if (Selection < _strings.Count + _fields.Count)
            Select(_fields[Selection - _strings.Count].GetComponent<Image>());
        else
        {
            Select(_slidersBackground[(Selection - _strings.Count - _fields.Count) * 3].GetComponent<Image>());
            Select(_slidersBackground[(Selection - _strings.Count - _fields.Count) * 3+1].GetComponent<Image>());
            Select(_slidersBackground[(Selection - _strings.Count - _fields.Count) * 3+2].GetComponent<Image>());
        }
    }

}
