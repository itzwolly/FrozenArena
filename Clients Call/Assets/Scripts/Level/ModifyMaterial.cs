using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyMaterial : MonoBehaviour {
    [SerializeField] private Material _material;

    void Awake() {
        Material mat = new Material(_material);
        GetComponent<Renderer>().material = mat;
    }

}
