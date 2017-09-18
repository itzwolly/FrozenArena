using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSlipperyTile : MonoBehaviour {
    
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
