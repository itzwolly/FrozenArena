using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DLLLibrary;

public class BombTile : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag=="Player")
        {
            StartCoroutine(Coroutines.CallVoidAfterSeconds(Destroy,gameObject,4));
        }
    }
}
