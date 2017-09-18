using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DLLLibrary;
using UnityEngine.UI;

public class SelectedLevelName : MonoBehaviour {
    [SerializeField] private GameObject _LevelControl;
    [SerializeField] private Scrollbar _scrollBar;
    [SerializeField] private GameObject _options;
    [SerializeField] private Button _buttonBrush;

    private List<Button> _buttons = new List<Button>();

    private int _selection;
    // Use this for initialization
    void Start() {
        //CreateOptions();
    }

    public void CreateOptions()
    {
        _selection = 0;
        string[] fileNames = Utility.AllFilesInPath("Assets\\Saves","*.txt");
        if (_buttons.Count < fileNames.Length)
        {
            for (int i = _buttons.Count; i < fileNames.Length; i++)
            {
                Debug.Log(fileNames[i]);
                Button obj = Instantiate(_buttonBrush);
                obj.GetComponent<TextFromButton>().TextField.text = fileNames[i];
                obj.transform.SetParent(_options.transform);
                _buttons.Add(obj);
            }
        }
        Select(_buttons[_selection].GetComponent<Image>());
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            ChangeSelection(1);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            ChangeSelection(-1);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Deselect(_buttons[_selection].GetComponent<Image>());
            _LevelControl.GetComponent<CreateSceneButton>().EndLoadLevel();
        }
    }
    private void ChangeSelection(int i)
    {
        Deselect(_buttons[_selection].GetComponent<Image>());
        _selection += i;
        if (_selection < 0)
            _selection = _buttons.Count - 1;
        if (_selection > _buttons.Count-1)
            _selection = 0;
        _LevelControl.GetComponent<CreateSceneButton>().ChangeSceneName(_buttons[_selection].GetComponent<TextFromButton>().TextField.text);
        Select(_buttons[_selection].GetComponent<Image>());
    }
    public void Select(Image obj)
    {
        Color col = obj.color;
        col.a = 1f;
        obj.color = col;
    }
    public void Deselect(Image obj)
    {
        Color col = obj.color;
        col.a = 0.5f;
        obj.color = col;
    }
}
