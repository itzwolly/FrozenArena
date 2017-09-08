using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempFileHandler : MonoBehaviour {

    private void Start() {
        //FileHandler.CreateTempFolder();
        //FileHandler.CreateTempPlayerFile(/*PlayerNumber*/);
    }

    private void OnApplicationQuit() {
        FileHandler.DeleteTempFolder();
    }
}
