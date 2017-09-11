using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputInfoScript : MonoBehaviour {
    [SerializeField] private Button StartAccept;
    // Update is called once per frame
    private bool _Started;
    private void Start()
    {
        _Started = false;
    }
    void Update () {
		if(Input.GetKeyUp(KeyCode.Space))
        {
            StartAccept.onClick.Invoke();
        }
	}
}
