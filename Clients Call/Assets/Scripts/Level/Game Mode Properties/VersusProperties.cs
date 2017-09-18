using UnityEngine;
using System;

[Serializable]
public class VersusProperties : System.Object { // change to properties if u want easy, medium and hard aswell.
    [SerializeField] private bool _enableRaisingTiles;
    [SerializeField] private float _dropAndRaiseSpeed;


    public bool EnableRaisingTiles {
        get { return _enableRaisingTiles; }
    }
    public float DropAndRaiseSpeed {
        get { return _dropAndRaiseSpeed; }
    }
}
