using UnityEngine;
using System;

[Serializable]
public class SurvivalProperties : Properties {
    [Space(-8)]
    [Header("Amount of seconds it shows before dropping a tile")]
    [Space(5)]
    [SerializeField] private int _easy;
    [SerializeField] private int _normal;
    [SerializeField] private int _hard;

    public int Easy {
        get { return _easy; }
    }
    public int Normal {
        get { return _normal; }
    }
    public int Hard {
        get { return _hard; }
    }
}
