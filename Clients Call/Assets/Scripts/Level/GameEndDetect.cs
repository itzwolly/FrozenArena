using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DLLLibrary;
using UnityEngine.SceneManagement;

public class GameEndDetect : MonoBehaviour {
    [SerializeField]
    private float _timeToEnd;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag=="Player")
        {
            StartCoroutine(Coroutines.CallVoidAfterSeconds(Utility.RestartLevel,_timeToEnd));
        }
        else if(collision.transform.tag == "BreakableTile")
        {
            Destroy(collision.gameObject);
        }
    }

}
