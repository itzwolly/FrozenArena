using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : MonoBehaviour {
    [SerializeField] private float _slowDownMultiplier;
    [SerializeField] private float _slowedDownSpeed;
    [SerializeField] private AudioClip SlowingPlayer;

    public float SlowDownMultiplier
    {
        get { return _slowDownMultiplier; }
        set { _slowDownMultiplier = value; }
    }
    public float SlowedDownSpeed
    {
        get { return _slowedDownSpeed; }
        set { _slowedDownSpeed = value; }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag == "Player") {
            SlowDownObject(collision.gameObject, _slowDownMultiplier);
            SetObjectSpeed(collision.gameObject, _slowedDownSpeed);
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.transform.tag == "Player") {
            // empty
            SetObjectSpeed(collision.gameObject, collision.gameObject.GetComponent<PlayerMovement>().Speed);
        }
    }

    private void SlowDownObject(GameObject pObject, float pAmount) {
        pObject.GetComponent<Rigidbody>().velocity *= pAmount;
        GetComponent<AudioSource>().PlayOneShot(SlowingPlayer);
    }

    private void SetObjectSpeed(GameObject pObject, float pAmount) {
        pObject.GetComponent<PlayerMovement>().CurrentSpeed = pAmount;
    }
}
