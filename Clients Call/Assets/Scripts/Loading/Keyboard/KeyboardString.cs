using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DLLLibrary;
using UnityEngine.UI;

public class KeyboardString : MonoBehaviour {
    //[SerializeField] List<GameObject> _keys;
    [SerializeField] GameObject _startKey;
    [SerializeField] Text _displayText;
    [SerializeField] List<Text> _connectedTexts;
    [SerializeField] CreateSceneButton _controller;
    private string _string;
    public string String
    {
        get { return _string; }
    }
    private GameObject _selected;
    private bool _shift;
    private bool _shiftPressed;
	// Use this for initialization
	void Start () {
        _selected = _startKey;
        Shared.Select(_selected.GetComponent<Image>());
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Shared.Deselect(_selected.GetComponent<Image>());
            _selected = _selected.GetComponent<Key>().Down;
            Shared.Select(_selected.GetComponent<Image>());
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Shared.Deselect(_selected.GetComponent<Image>());
            _selected = _selected.GetComponent<Key>().Up;
            Shared.Select(_selected.GetComponent<Image>());
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Shared.Deselect(_selected.GetComponent<Image>());
            _selected = _selected.GetComponent<Key>().Left;
            Shared.Select(_selected.GetComponent<Image>());
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Shared.Deselect(_selected.GetComponent<Image>());
            _selected = _selected.GetComponent<Key>().Right;
            Shared.Select(_selected.GetComponent<Image>());
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //_controller.SetInactiveEditing();
            string nextLetter = _selected.GetComponent<Key>().Letter;
            if (nextLetter == "->")
            {
                _selected.GetComponent<Button>().onClick.Invoke();
            }
            else if (nextLetter == "^")
            {
                if (_shift == true)
                {
                    _shiftPressed = true;
                    _shift = false;
                }
                else if (_shiftPressed)
                {
                    _shiftPressed = false;
                }
                else
                    _shift = true;
            }
            else if (nextLetter == "-")
            {
                if (_string.Length > 0)
                {
                    _string = _string.Remove(_string.Length - 1);
                    UpdateTexts();
                }
            }
            else
            {
                if (_shift || _shiftPressed)
                {
                    nextLetter = nextLetter.ToUpper();
                    _shift = false;
                }
                _string += nextLetter;
                UpdateTexts();
            }
        }
    }
    private void UpdateTexts()
    {
        _displayText.text = _string;
        foreach(Text t in _connectedTexts)
        {
            t.text = _string;
        }
    }
}
