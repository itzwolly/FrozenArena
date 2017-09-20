using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeamSelectEvents : MonoBehaviour {
    [SerializeField] private KeyCode _interactionKey;

    public void OnBothSelectedSkin(KeyCode pKeyCode) {
        if (Input.GetKeyUp(pKeyCode)) {
            SceneManager.LoadScene("Arena Selection");
        }
    }
}
