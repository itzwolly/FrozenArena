using UnityEngine;
using System;

[Serializable]
public class WoodToHoleProperties : Properties {
    [Space(-8)]
    [Header("Amount of holes that will light up (at once).")]
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
