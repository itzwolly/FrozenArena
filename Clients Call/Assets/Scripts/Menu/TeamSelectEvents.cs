using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeamSelectEvents : MonoBehaviour {
    [SerializeField] private KeyCode _interactionKey;


    private void Update() {
        OnInteractionKeyClick(_interactionKey);
    }

    public void OnInteractionKeyClick(KeyCode pKeyCode) {
        if (Input.GetKeyUp(pKeyCode)) {
            SceneManager.LoadScene("Arena Selection");
        }
    }
}
