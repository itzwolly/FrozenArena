using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSpecificLevel : MonoBehaviour {
    [SerializeField] CreateSceneButton _controller;
    [SerializeField] string _sceneToLoad;
    private MenuDataHandler _handler;
    
	// Use this for initialization
	void Start () {
        _handler = MenuDataHandler.Instance;
        _sceneToLoad = _handler.NewLevelName;
        LoadALevel();
	}

    public void ChangeSceneToLoad(string s)
    {
        _sceneToLoad = s;
    }
	
    public void LoadALevel()
    {
        Debug.Log(_sceneToLoad);
        _controller.SetNewLevel(_sceneToLoad);
        _controller.EndLoadLevel();
    }
}
