using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour {
    [SerializeField] private ParticleSystem _psystem;
    [SerializeField] private AudioClip Pickup;
    [SerializeField] private AudioSource _audioSource;
    private void OnCollisionEnter(Collision collision) {
        //Debug.Log(collision.transform.tag);
        if (collision.transform.tag == "Player") {
            if (gameObject != null) {

                _psystem.Play();
                _audioSource.PlayOneShot(Pickup);
                PlayerStatsHandler.Instance.PlayerData[collision.transform.name].ItemsPickedUp++;
                Debug.Log(PlayerStatsHandler.Instance.PlayerData[collision.transform.name].ItemsPickedUp);
                Destroy(gameObject);

            }
        }
    }
}
