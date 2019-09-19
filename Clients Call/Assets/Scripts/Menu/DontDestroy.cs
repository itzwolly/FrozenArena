using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour {
    private StudioEventEmitter _emitter;
    
    // Use this for initialization
    private void Start () {
        if (MenuDataHandler.Instance.HasCreatedEmitter == false) {
            DontDestroyOnLoad(transform.gameObject);
            MenuDataHandler.Instance.HasCreatedEmitter = true;
        }

        _emitter = GameObject.FindGameObjectWithTag("FMODEventEmitter").GetComponent<StudioEventEmitter>();
        _emitter.Play();
    }

    private void Update() {
        if (SceneManager.GetActiveScene().name.Contains("level")) {
            _emitter.SetParameter("AdaptiveAudio", 3);
        } else if (SceneManager.GetActiveScene().name.Contains("Resolution")) {
            _emitter.SetParameter("AdaptiveAudio", 5);
        } else if (SceneManager.GetActiveScene().name.Contains("Main Menu")) {
            _emitter.SetParameter("AdaptiveAudio", 0);
        } else if (SceneManager.GetActiveScene().name.Contains("Team") || SceneManager.GetActiveScene().name.Contains("Skin") || SceneManager.GetActiveScene().name.Contains("Arena")) {
            _emitter.SetParameter("AdaptiveAudio", 1);
        } else if (SceneManager.GetActiveScene().name.Contains("Preview")) {
            _emitter.SetParameter("AdaptiveAudio", 2);
        } /*else {
            _emitter.SetParameter("AdaptiveAudio", 0);
        }*/
    }

}
