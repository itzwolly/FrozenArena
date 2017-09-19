using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuEvents : MonoBehaviour {

    public void OnPlayClick() {
        // Load play scene
        SceneManager.LoadScene("Team Select");
    }

    public void OnStatisticsClick() {
        // Load stats scene

    }

    public void OnMapBuilderClick() {
        // Load map builder scene

    }

    public void OnSettingsClick() {
        // Load settings scene

    }
}
