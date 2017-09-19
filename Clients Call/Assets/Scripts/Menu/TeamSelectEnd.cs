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
        if (/*(p1Selection.Ready && p2Selection.State == TeamSelection.TeamState.NoTeam) ||*/ (p1Selection.Ready && p2Selection.Ready)) {
            // load next scene
            Timer.Register(_timeToWaitAfterReady, LoadNextScene);
        }
	}

    private void LoadNextScene() {
        SceneManager.LoadScene("Skin Selection");
    }
}
