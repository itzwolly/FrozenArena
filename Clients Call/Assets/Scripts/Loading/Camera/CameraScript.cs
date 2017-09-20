using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(0, 0, 0.5f);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(0, 0, -0.5f);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(0.5f, 0, 0);
        if (Input.GetKey(KeyCode.A))
            transform.Translate(-0.5f, 0, 0);
        if (Input.GetKey(KeyCode.E))
            transform.Translate(0, 0.5f, 0);
        if (Input.GetKey(KeyCode.Q))
            transform.Translate(0, -0.5f, 0);
        if (Input.GetKey(KeyCode.UpArrow))
            transform.Rotate(-1, 0, 0);
        if (Input.GetKey(KeyCode.DownArrow))
            transform.Rotate(1, 0, 0);
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(0, -1, 0);
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(0, 1, 0);

    }
}
