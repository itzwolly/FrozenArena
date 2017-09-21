using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkinSelectionEnd : MonoBehaviour {
    [SerializeField] private float _timeToWaitAfterReady;

    private SkinSelectionHandler _skinSelection;
    private Timer _timer;

    private void Start() {
        _skinSelection = GetComponent<SkinSelectionHandler>();
    }

    // Update is called once per frame
    void Update () {
        if (MenuDataHandler.Instance.PlayersReady == 1) {
            if (_skinSelection.P1Ready) {
                CreateTimer();
            } else {
                CancelTimer();
            }
        } else {
            if (_skinSelection.P1Ready && _skinSelection.P2Ready) {
                CreateTimer();
            } else {
                CancelTimer();
            }
        }
    }

    private void CreateTimer() {
        if (_timer == null) {
            Debug.Log("Registering timer");
            _timer = Timer.Register(_timeToWaitAfterReady, LoadNextScene);
        }
    }

    private void CancelTimer() {
        if (_timer != null) {
            Debug.Log("Canceling Timer");
            Timer.CancelAllRegisteredTimers();
            _timer = null;
        }
    }
    private void LoadNextScene() {
        //SavePlayersReadyData();
        if (MenuDataHandler.Instance.PlayersReady == 1) {
            StartCoroutine(LoadLevel("Arena Selection SP"));
        } else {
            StartCoroutine(LoadLevel("Arena Selection MP"));
        }
    }

    private AsyncOperation asyncLoadLevel;

    private IEnumerator LoadLevel(string pName) {
        asyncLoadLevel = SceneManager.LoadSceneAsync(pName, LoadSceneMode.Single);

        while (!asyncLoadLevel.isDone) {
            yield return null;
        }
    }

    //private void SavePlayersReadyData() {
    //    // Set amount of players who are ready, to use in the following flow.
    //    for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++) {
    //        GameObject player = GameObject.FindGameObjectsWithTag("Player")[i];
    //        TeamSelection selection = player.GetComponent<TeamSelection>();

    //        if (i == 0) {
    //            MenuDataHandler.Instance.Player2Keys = selection.Keys;
    //        } else {
    //            MenuDataHandler.Instance.Player1Keys = selection.Keys;
    //        }

    //        SetDataForTeamSelection(selection);
    //    }
    //}

    //private void SetDataForTeamSelection(TeamSelection pSelection) {
    //    if (pSelection.Ready) {
    //        MenuDataHandler.Instance.PlayersReady++;

    //        if (pSelection.State == TeamSelection.TeamState.Purple) {
    //            MenuDataHandler.Instance.IsPlayer1Purple = true;
    //        } else {
    //            MenuDataHandler.Instance.IsPlayer1Purple = false;
    //        }
    //    }
    //}
}
