using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoredInfo : MonoBehaviour {

    public List<GameObject> MovableCubes = new List<GameObject>();

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
}
