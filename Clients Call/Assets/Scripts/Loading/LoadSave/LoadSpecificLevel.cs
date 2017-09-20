using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSpecificLevel : MonoBehaviour {
    [SerializeField] CreateSceneButton _controller;
    [SerializeField] string _sceneToLoad;
    
	// Use this for initialization
	void Start () {
	}

    public void ChangeSceneToLoad(string s)
    {
        _sceneToLoad = s;
    }
	
    public void LoadALevel()
    {
        _controller.SetNewScene(_sceneToLoad);
        _controller.EndLoadLevel();
    }
}
