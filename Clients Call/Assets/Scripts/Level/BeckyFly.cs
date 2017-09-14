using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeckyFly : MonoBehaviour {
    private Vector3 direction;
    [SerializeField] private float _speed;

    // Use this for initialization
    void Start () {
        direction = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += direction.normalized * _speed * Time.deltaTime;
        //transform.rotation = Quaternion.Euler(direction.normalized);
        transform.rotation = Quaternion.Euler(direction);
    }
}
