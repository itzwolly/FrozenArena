using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DLLLibrary;
using UnityEngine.SceneManagement;

public class GameEndDetect : MonoBehaviour {
    [SerializeField] private float _timeToEnd;
    [SerializeField] private float _timeToDestroy;

    private void OnTriggerEnter(Collider collision) {
        if (collision.transform.tag == "Player") {
            StartCoroutine(Coroutines.CallVoidAfterSeconds(Utility.RestartLevel, _timeToEnd));
            GameObject scoringPlayer = (GameObject.FindGameObjectsWithTag("Player")[0] == collision.gameObject) ? GameObject.FindGameObjectsWithTag("Player")[0] : GameObject.FindGameObjectsWithTag("Player")[1];
            scoringPlayer.GetComponent<PlayerStats>().Score++;

            Debug.Log("Player-1 (" + GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerStats>().Score + " - " + GameObject.FindGameObjectsWithTag("Player")[1].GetComponent<PlayerStats>().Score + ") Player-2");
        } else if (collision.transform.tag == "BreakableTile") {
            StartCoroutine(Coroutines.CallVoidAfterSeconds(Destroy, collision.gameObject, _timeToDestroy));
        }
    }
}
