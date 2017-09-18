using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkinSelectionHandler : MonoBehaviour {

    [SerializeField] private GameObject _singlePlayerLayout;
    [SerializeField] private GameObject _multiPlayerLayout;
    
	// Use this for initialization
	void Start () {
        Debug.Log(MenuDataHandler.Instance.PlayersReady + " | " + MenuDataHandler.Instance.IsPlayer1Purple);

        if (MenuDataHandler.Instance.PlayersReady == 1) {
            _singlePlayerLayout.SetActive(true);
            _multiPlayerLayout.SetActive(false);

            GameObject headerObject = GameObject.FindGameObjectWithTag("HeaderSP");
            if (MenuDataHandler.Instance.IsPlayer1Purple) {
                headerObject.transform.GetChild(0).gameObject.SetActive(true);
                headerObject.transform.GetChild(1).gameObject.SetActive(false);
            } else {
                headerObject.transform.GetChild(0).gameObject.SetActive(false);
                headerObject.transform.GetChild(1).gameObject.SetActive(true);
            }
        } else {
            _singlePlayerLayout.SetActive(false);
            _multiPlayerLayout.SetActive(true);

            GameObject headerObject = GameObject.FindGameObjectWithTag("HeaderMP");
            if (MenuDataHandler.Instance.IsPlayer1Purple) {
                headerObject.transform.GetChild(0).gameObject.SetActive(true);
                headerObject.transform.GetChild(1).gameObject.SetActive(false);

                headerObject.transform.GetChild(2).gameObject.SetActive(false);
                headerObject.transform.GetChild(3).gameObject.SetActive(true);
            } else {
                headerObject.transform.GetChild(0).gameObject.SetActive(false);
                headerObject.transform.GetChild(1).gameObject.SetActive(true);

                headerObject.transform.GetChild(2).gameObject.SetActive(true);
                headerObject.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.Backspace)) {
            SceneManager.LoadScene("Play");
        }
	}
}
