using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DLLLibrary;

public class StoredInfo : MonoBehaviour {

    public List<GameObject> MovableCubes = new List<GameObject>();
    public List<GameObject> Players = new List<GameObject>();

    private void Start()
    {
        foreach(Transform t in transform)
        {
            if (t.tag == "Ground")
            {
                MovableCubes.Add(t.gameObject);
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Utility.RestartLevel();
        }
    }
}
