using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiDirectionalBoost : MonoBehaviour {
    [SerializeField] private float _speedBoost;
    [SerializeField] private AudioClip SpeedingUpPlayer;

    public float SpeedBoost
    {
        get { return _speedBoost; }
        set { _speedBoost = value; }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag == "Player") {
            Debug.Log("Applying boost to: " + collision.transform.name);
            ActivateBoost(collision.gameObject, collision.rigidbody.velocity * _speedBoost);
        }
    }

    private void ActivateBoost(GameObject pGameObject, Vector3 pMultiplier) {
        GetComponent<AudioSource>().PlayOneShot(SpeedingUpPlayer);
        pGameObject.GetComponent<Rigidbody>().AddForce(pMultiplier, ForceMode.Impulse);
    }
}
