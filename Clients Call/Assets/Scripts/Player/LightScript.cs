using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour {
    [SerializeField]
    private GameObject Player;
	// Use this for initialization

	void Update () {
        transform.position = Player.transform.position;
	}
}
