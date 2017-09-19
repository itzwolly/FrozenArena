using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour {
    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag == "Pickup") {
            if (collision.gameObject != null) {

                PlayerStatsHandler.Instance.PlayerData["Player_1"].ItemsPickedUp++;
                Destroy(collision.gameObject);

                Debug.Log("Points: " + PlayerStatsHandler.Instance.PlayerData["Player_1"].ItemsPickedUp);
            }
        }
    }
}
