using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersReady : MonoBehaviour {
    private int _amountOfPlayersReady;

	// Use this for initialization
	private void Start () {
        DontDestroyOnLoad(gameObject.transform);
	}

    private void OnDestroy() {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++) {
            GameObject player = GameObject.FindGameObjectsWithTag("Player")[i];
            if (player.GetComponent<TeamSelection>().Ready) {
                _amountOfPlayersReady++;
            }
        }
    }
}
