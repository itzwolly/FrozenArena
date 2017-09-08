using UnityEngine;
using System;

[Serializable]
public class SurvivalProperties : System.Object {
    [Header("Tile Wait Time Before Dropping")]
    [SerializeField] private int _slow;
    [SerializeField] private int _normal;
    [SerializeField] private int _fast;

    public int Slow {
        get { return _slow; }
    }
    public int Normal {
        get { return _normal; }
    }
    public int Fast {
        get { return _fast; }
    }
}
