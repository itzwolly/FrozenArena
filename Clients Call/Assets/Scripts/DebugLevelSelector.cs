using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugLevelSelector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        SceneManager.LoadScene("sp_level_1");

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SceneManager.LoadScene("sp_level_2");

        if (Input.GetKeyDown(KeyCode.Alpha3))
            SceneManager.LoadScene("sp_level_3");

        if (Input.GetKeyDown(KeyCode.Alpha4))
            SceneManager.LoadScene("sp_level_4");

        if (Input.GetKeyDown(KeyCode.Alpha5))
            SceneManager.LoadScene("sp_level_5");

        if (Input.GetKeyDown(KeyCode.Alpha6))
            SceneManager.LoadScene("sp_level_6");

        if (Input.GetKeyDown(KeyCode.Alpha7))
            SceneManager.LoadScene("sp_level_7");

        if (Input.GetKeyDown(KeyCode.F1))
            SceneManager.LoadScene("mp_level_1");

        if (Input.GetKeyDown(KeyCode.F2))
            SceneManager.LoadScene("mp_level_2");

        if (Input.GetKeyDown(KeyCode.F3))
            SceneManager.LoadScene("mp_level_3");

        if (Input.GetKeyDown(KeyCode.F4))
            SceneManager.LoadScene("mp_level_4");

        if (Input.GetKeyDown(KeyCode.F5))
            SceneManager.LoadScene("mp_level_5");

        if (Input.GetKeyDown(KeyCode.F6))
            SceneManager.LoadScene("mp_level_6");

        if (Input.GetKeyDown(KeyCode.F7))
            SceneManager.LoadScene("mp_level_7");

        if (Input.GetKeyDown(KeyCode.F8))
            SceneManager.LoadScene("mp_level_8");
    }
}
