using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnim : MonoBehaviour {
    [SerializeField] private Animator _camAnimator;
    // Use this for initialization
    void Start () {
        _camAnimator.SetTrigger("Awake");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
