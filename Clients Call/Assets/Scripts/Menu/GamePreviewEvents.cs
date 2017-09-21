using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class GamePreviewEvents : MonoBehaviour {
    private AsyncOperation asyncLoadLevel;

    public void OnPlayClick() {
        string sceneName = MenuDataHandler.Instance.QueuedMaps.First().SceneName;
        MenuDataHandler.Instance.QueuedMaps.Remove(MenuDataHandler.Instance.QueuedMaps.First());

        StartCoroutine(LoadLevel(sceneName));
    }

    private IEnumerator LoadLevel(string pName) {
        asyncLoadLevel = SceneManager.LoadSceneAsync(pName, LoadSceneMode.Single);

        while (!asyncLoadLevel.isDone) {
            yield return null;
        }
    }
}
