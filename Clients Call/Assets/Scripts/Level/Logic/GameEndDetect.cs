using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DLLLibrary;
using UnityEngine.SceneManagement;

public class GameEndDetect : MonoBehaviour {
    [SerializeField]
    private float _timeToEnd;
    [SerializeField]
    private float _timeToDestroy;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            StartCoroutine(Coroutines.CallVoidAfterSeconds(Utility.RestartLevel, _timeToEnd));
        }
        else if (collision.transform.tag == "BreakableTile")
        {
            StartCoroutine(Coroutines.CallVoidAfterSeconds(Destroy, collision.gameObject, _timeToDestroy));
        }
    }
    
}
