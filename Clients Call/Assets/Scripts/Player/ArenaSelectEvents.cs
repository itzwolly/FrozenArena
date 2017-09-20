using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaSelectEvents : MonoBehaviour {

	public void OnStartClick() {
        // go to preview screen of first map in queued maps

    }

    public void OnReturnClick() {
        // go back to 
        SceneManager.LoadScene("Skin Selection");
    }
}
