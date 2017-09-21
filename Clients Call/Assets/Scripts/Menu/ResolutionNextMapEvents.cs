using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResolutionNextMapEvents : MonoBehaviour {
    AsyncOperation _asyncLoadLevel;

    public void OnNextClick() {
        string sceneName = MenuDataHandler.Instance.QueuedMaps.First().SceneName;
        MenuDataHandler.Instance.QueuedMaps.Remove(MenuDataHandler.Instance.QueuedMaps.First());

        PlayerStatsHandler.Instance.PlayerData["Player_1"].Score = 0;
        PlayerStatsHandler.Instance.PlayerData["Player_2"].Score = 0;

        PlayerStatsHandler.Instance.PlayerData["Player_1"].HasWon = false;
        PlayerStatsHandler.Instance.PlayerData["Player_2"].HasWon = false;

        StartCoroutine(LoadLevel(sceneName));
    }

    private IEnumerator LoadLevel(string pName) {
        _asyncLoadLevel = SceneManager.LoadSceneAsync(pName, LoadSceneMode.Single);

        while (!_asyncLoadLevel.isDone) {
            print("Loading the Scene");
            yield return null;
        }
    }
}
