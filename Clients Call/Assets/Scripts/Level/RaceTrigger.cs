using DLLLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceTrigger : MonoBehaviour {
    [SerializeField] private int _checkpointsPassed = 0;
    [SerializeField] private int _timeToEnd = 1;
    

    // Use this for initialization
    void Start () {
		
	}


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (gameObject.tag == "Finish")
            {
                //finish
                //StartCoroutine(Coroutines.CallVoidAfterSeconds(Utility.RestartLevel, _timeToEnd));
                SceneManager.LoadScene("ResolutionSP");
            }
            if (gameObject.tag == "Checkpoint")
            {
                //add to the checkpoints passed number
                _checkpointsPassed++;
            }
        }
    }
   
}
