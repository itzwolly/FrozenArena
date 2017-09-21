using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DLLLibrary;
using UnityEngine.UI;
using UnityEditor;

public class CustomMaps : MonoBehaviour {
    private MenuDataHandler _handler;
    [SerializeField] GameObject _selection;
    // Use this for initialization
    private List<GameObject> _arenas = new List<GameObject>();
	void Start () {
        _handler = MenuDataHandler.Instance;
        string[] fileNames = Utility.AllFilesInPath("Assets\\Saves", "*.txt");
        _selection.GetComponent<MapData>().Name = fileNames[0];
        _arenas.Add(_selection);
        Vector3 initialPos = _selection.transform.position;
        Navigation nav;
        for (int i = 1; i < fileNames.Length; i++)
        {
            Debug.Log(fileNames[i]);
            GameObject obj = Instantiate(_selection);
            obj.transform.SetParent(transform);
            obj.transform.position = initialPos;
            obj.transform.Translate(i*31,0,0);
            obj.GetComponent<MapData>().Name = fileNames[i];
            nav = obj.GetComponent<Button>().navigation;
            nav.selectOnLeft = _arenas[i - 1].GetComponent<Button>();
            obj.GetComponent<Button>().navigation = nav;
            nav = _arenas[i-1].GetComponent<Button>().navigation;
            nav.selectOnRight = obj.GetComponent<Button>(); 
            _arenas[i - 1].GetComponent<Button>().navigation = nav;
            obj.transform.localScale = new Vector3(1,1,1);
            _arenas.Add(obj);
        }
        nav = _arenas[0].GetComponent<Button>().navigation;
        nav.selectOnLeft = _arenas[_arenas.Count - 1].GetComponent<Button>();
        _arenas[0].GetComponent<Button>().navigation = nav;

        nav = _arenas[_arenas.Count - 1].GetComponent<Button>().navigation;
        nav.selectOnRight = _arenas[0].GetComponent<Button>();
        _arenas[_arenas.Count - 1].GetComponent<Button>().navigation = nav;
    }
	
	// Update is called once per frame
	void Update () {
		if(Selection.activeGameObject.GetComponent<MapData>().Name!=_handler.NewLevelName)
        {
            _handler.NewLevelName = Selection.activeGameObject.GetComponent<MapData>().Name;
        }
	}
}
