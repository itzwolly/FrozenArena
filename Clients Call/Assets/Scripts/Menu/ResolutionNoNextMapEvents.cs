using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResolutionNoNextMapEvents : MonoBehaviour {

    public void OnReplayClick() {
        // copy previously selected queued maps to new queued maps. load the first one delete the first one
        MenuDataHandler.Instance.QueuedMaps = MenuDataHandler.Instance.CopyQueuedMaps;

        string sceneName = MenuDataHandler.Instance.QueuedMaps.First().SceneName;
        MenuDataHandler.Instance.QueuedMaps.Remove(MenuDataHandler.Instance.QueuedMaps.First());

        PlayerStatsHandler.Instance.PlayerData["Player_1"].Score = 0;
        PlayerStatsHandler.Instance.PlayerData["Player_2"].Score = 0;

        PlayerStatsHandler.Instance.PlayerData["Player_1"].HasWon = false;
        PlayerStatsHandler.Instance.PlayerData["Player_2"].HasWon = false;

        StartCoroutine(LoadLevel(sceneName));
    }

    public void OnModifyGameClick() {
        if (MenuDataHandler.Instance.PlayersReady == 1) {
            SceneManager.LoadScene("Arena Selection SP");
        } else {
            SceneManager.LoadScene("Arena Selection MP");
        }
    }

    public void OnMainMenuClick() {
        PlayerStatsHandler.Instance.PlayerData["Player_1"].Score = 0;
        PlayerStatsHandler.Instance.PlayerData["Player_2"].Score = 0;

        PlayerStatsHandler.Instance.PlayerData["Player_1"].HasWon = false;
        PlayerStatsHandler.Instance.PlayerData["Player_2"].HasWon = false;

        SceneManager.LoadScene("Main Menu");
    }

    AsyncOperation _asyncLoadLevel;
    private IEnumerator LoadLevel(string pName) {
        _asyncLoadLevel = SceneManager.LoadSceneAsync(pName, LoadSceneMode.Single);

        while (!_asyncLoadLevel.isDone) {
            yield return null;
        }
    }
}
