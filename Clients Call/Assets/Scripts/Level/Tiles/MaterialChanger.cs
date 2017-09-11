using UnityEngine;
using System.Collections;

public class MaterialChanger : MonoBehaviour {
    [SerializeField] private Material material1;
    [SerializeField] private Material material2;
    [SerializeField] private float duration = 2.0F;
    [SerializeField] private Renderer rend;

    private void Start() {
        rend = GetComponent<Renderer>();
        rend.material = material1;
    }

    private void Update() {
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        rend.material.Lerp(rend.material, material2, lerp);
    }
}