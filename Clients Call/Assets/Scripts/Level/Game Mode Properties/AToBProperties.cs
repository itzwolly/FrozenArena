using UnityEngine;
using System;

[Serializable]
public class AToBProperties : Properties {
    [Space(-8)]
    [Header("Amount of seconds before all but one tiles drop.")]
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
