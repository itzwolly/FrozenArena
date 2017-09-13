using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopOutMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject UpOption;
    [SerializeField] private GameObject RightOption;
    [SerializeField] private GameObject DownOption;
    [SerializeField] private GameObject LeftOption;
    [SerializeField] private GameObject Controller;

    private List<GameObject> _shownOptions;

    private CreateSceneButton _controllerScript;
    private GameObject _selected;
    private GameObject _selectedOption;

    private int _selection;
    private bool _verticalSelect;
    private bool _horizontalSelect;
    // Use this for initialization
    void Start () {
        _controllerScript = Controller.GetComponent<CreateSceneButton>();
	}

    public Action ToDo;

    private void NextSelection()
    {
        Color col = _shownOptions[_selection].GetComponent<Image>().color;
        col.a = 0.5f;
        _shownOptions[_selection].GetComponent<Image>().color = col;
        _selection++;
        if (_selection > _shownOptions.Count-1)
            _selection = 0;
        col = _shownOptions[_selection].GetComponent<Image>().color;
        col.a = 1;
        _selected = _shownOptions[_selection];
        _shownOptions[_selection].GetComponent<Image>().color = col;
        ToDo = _shownOptions[_selection].GetComponent<Button>().onClick.Invoke;

        //Debug.Log("Next selection");
    }

    private void PreviouseSelection()
    {
        Color col = _shownOptions[_selection].GetComponent<Image>().color;
        col.a = 0.5f;
        _shownOptions[_selection].GetComponent<Image>().color = col;
        _selection--;
        if (_selection < 0)
            _selection = _shownOptions.Count - 1;
        col = _shownOptions[_selection].GetComponent<Image>().color;
        col.a = 1;
        _shownOptions[_selection].GetComponent<Image>().color = col;
        ToDo = _shownOptions[_selection].GetComponent<Button>().onClick.Invoke;
        //Debug.Log("Previous selection");
    }

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_verticalSelect)
            {
                Deselect();
                _verticalSelect = false;
            }
            else if(_horizontalSelect)
            {
                PreviouseSelection();
            }
            else
            {
                ShowOptions(UpOption);
                _selectedOption = UpOption;
                _verticalSelect = true;
            }


        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_verticalSelect)
            {
                Deselect();
                _verticalSelect = false;
            }
            else if (_horizontalSelect)
            {
                NextSelection();
            }
            else
            {
                ShowOptions(DownOption);
                _selectedOption = DownOption;
                ToDo = _controllerScript.SelectDeleteBlock;
                _verticalSelect = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_verticalSelect)
            {
                PreviouseSelection();
            }
            else if (_horizontalSelect)
            {
                Deselect();
                _horizontalSelect = false;
            }
            else
            {
                ShowOptions(LeftOption);
                _selectedOption = LeftOption;
                _horizontalSelect = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_verticalSelect)
            {
                NextSelection();
            }
            else if (_horizontalSelect)
            {
                Deselect();
                _horizontalSelect = false;
            }
            else
            {
                ShowOptions(RightOption);
                _selectedOption = RightOption;
                _horizontalSelect = true;
            }
        }

    }

    private void ShowOptions(GameObject selection)
    {
        _selection = 0;
        _shownOptions = new List<GameObject>();
        Select(selection);
        foreach (Transform t in selection.transform)
        {
            t.gameObject.SetActive(true);
            _shownOptions.Add(t.gameObject);
        }
        Color col = _shownOptions[_selection].GetComponent<Image>().color;
        col.a = 1;
        _shownOptions[_selection].GetComponent<Image>().color = col;
        ToDo = _shownOptions[_selection].GetComponent<Button>().onClick.Invoke;
    }

    private void HideOptions(GameObject selection)
    {
        _shownOptions = new List<GameObject>();
        foreach (Transform t in selection.transform)
        {
            t.gameObject.SetActive(false);
        }
        //Color col = _shownOptions[_selection].GetComponent<Image>().color;
        //col.a = 0.5f;
        //_shownOptions[_selection].GetComponent<Image>().color = col;
    }

    private void Select(GameObject obj)
    {
        Deselect();
        _selected = obj;
        Color col = obj.GetComponent<Image>().color;
        col.a = 1;
        obj.GetComponent<Image>().color = col;
    }
    public void Deselect()
    {
        if (_selected != null)
        {
            //Debug.Log("deselecting");
            Color col = _selected.GetComponent<Image>().color;
            col.a = 0.5f;
            _selected.GetComponent<Image>().color = col;

            col = _selectedOption.GetComponent<Image>().color;
            col.a = 0.5f;
            _selectedOption.GetComponent<Image>().color = col;
            HideOptions(_selectedOption);
        }
        else
        {
            Debug.Log("nothing was previously selected");
        }
        _horizontalSelect = false;
        _verticalSelect = false;
    }

    

}
