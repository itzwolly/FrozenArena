using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeamSelectEnd : MonoBehaviour {
    [SerializeField] private GameObject _player1;
    [SerializeField] private GameObject _player2;

    [SerializeField] private float _timeToWaitAfterReady;

    private TeamSelection p1Selection;
    private TeamSelection p2Selection;

    private void Start() {
        p1Selection = _player1.GetComponent<TeamSelection>();
        p2Selection = _player2.GetComponent<TeamSelection>();
    }

    // Update is called once per frame
    void Update () {
        if ((p1Selection.Ready && p2Selection.State == TeamSelection.TeamState.NoTeam) || (p1Selection.Ready && p2Selection.Ready)) {
            

            // load next scene
            Timer.Register(_timeToWaitAfterReady, LoadNextScene);
        }
	}

    private void LoadNextScene() {
        SavePlayersReadyData();
        SceneManager.LoadScene("Skin Selection");
    }

    private void SavePlayersReadyData() {
        // Set amount of players who are ready, to use in the following flow.
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++) {
            GameObject player = GameObject.FindGameObjectsWithTag("Player")[i];
            TeamSelection selection = player.GetComponent<TeamSelection>();
            if (selection.Ready) {
                MenuDataHandler.Instance.PlayersReady++;
                
                if (selection.State == TeamSelection.TeamState.Purple) {
                    MenuDataHandler.Instance.IsPlayer1Purple = true;
                } else {
                    MenuDataHandler.Instance.IsPlayer1Purple = false;
                }
            }
        }
    }
}
