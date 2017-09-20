using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaSelectEvents : MonoBehaviour {

	public void OnStartClick() {
        // go to preview screen of first map in queued maps
        if (MenuDataHandler.Instance.QueuedMaps.Count > 0) {
            if (MenuDataHandler.Instance.PlayersReady == 1) {
                SceneManager.LoadScene("GamePreviewSP");
            } else {
                SceneManager.LoadScene("GamePreviewMP");
            }
        }
    }

    public void OnReturnClick() {
        // go back to 
        SceneManager.LoadScene("Skin Selection");
    }
}
