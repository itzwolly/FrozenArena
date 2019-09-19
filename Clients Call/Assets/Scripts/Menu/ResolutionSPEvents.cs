using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResolutionSPEvents : MonoBehaviour {

	public void OnMainMenuClick() {
        SceneManager.LoadScene("Main Menu");
    }
}
