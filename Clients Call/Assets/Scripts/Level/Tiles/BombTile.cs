using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DLLLibrary;

public class BombTile : MonoBehaviour {

    [SerializeField]
    private List<GameObject> _players;
    [SerializeField]
    private float _powerMultiplier;
    [SerializeField]
    private float _maxDistance;
    [SerializeField]
    private float _explodeTimer;
    [SerializeField]
    private float _resetTime;
    [SerializeField]
    private AudioClip Exploding;

    Material _mat;
    Color _initialColor;
    bool _exploding;

    private void Start()
    {
        _mat = gameObject.GetComponent<Renderer>().material;
        _initialColor = _mat.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Color col = _initialColor;
        col.a /= 2;
        gameObject.GetComponent<Renderer>().material.color = col;
        if (collision.transform.tag=="Player" && !_exploding)
        {
            _exploding = true;
            StartCoroutine(Coroutines.CallVoidAfterSeconds(Explode,_explodeTimer));
        }
    }


    private void Explode()
    {
        GetComponent<AudioSource>().PlayOneShot(Exploding);
        foreach (GameObject obj in _players)
        {
            Vector3 direction = obj.transform.position - gameObject.transform.position;
            if (direction.magnitude > _maxDistance)
                continue;
            float power = (_maxDistance/direction.magnitude)*_powerMultiplier;
            direction.Normalize();
            //Debug.Log(power);
            obj.GetComponent<Rigidbody>().AddForce(direction*power,ForceMode.Impulse);
        }
        //boom
        StartCoroutine(Coroutines.CallVoidAfterSeconds(Reset, _resetTime));
    }

    private void Reset()
    {
        gameObject.GetComponent<Renderer>().material.color = _initialColor;
        _exploding = false;

    }
}
