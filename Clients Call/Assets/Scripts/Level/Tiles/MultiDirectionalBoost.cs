using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiDirectionalBoost : MonoBehaviour {
    [SerializeField] private float _speedBoost;
    [SerializeField] private AudioClip SpeedingUpPlayer;

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag == "Player") {
            ActivateBoost(collision.gameObject, collision.rigidbody.velocity * _speedBoost);
        }
    }

    private void ActivateBoost(GameObject pGameObject, Vector3 pMultiplier) {
        GetComponent<AudioSource>().PlayOneShot(SpeedingUpPlayer);
        pGameObject.GetComponent<Rigidbody>().AddForce(pMultiplier, ForceMode.Impulse);

        PlayerStatsHandler.Instance.PlayerData[pGameObject.name].TotalAmountBoosted++;
        PlayerStatsHandler.Instance.PlayerData[pGameObject.name].AmountBoostedMulti++;
    }
}
