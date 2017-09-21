using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMapSelect : MonoBehaviour {
    private CustomMaps _parent;
    [SerializeField]private MapData _data;
    public CustomMaps Parent
    {
        get { return _parent; }
        set { _parent = value; }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
