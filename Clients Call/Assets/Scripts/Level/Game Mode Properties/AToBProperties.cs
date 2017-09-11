using UnityEngine;
using System;

[Serializable]
public class AToBProperties : Properties {
    [Space(-8)]
    [Header("Amount of seconds before all but one tiles drop.")]
    [Space(5)]
    [SerializeField] private float _easy;
    [SerializeField] private float _normal;
    [SerializeField] private float _hard;

    public float Easy {
        get { return _easy; }
    }
    public float Normal {
        get { return _normal; }
    }
    public float Hard {
        get { return _hard; }
    }
}
