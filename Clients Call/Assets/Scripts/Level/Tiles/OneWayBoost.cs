using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayBoost : MonoBehaviour {
    [SerializeField] private Direction _direction;
    [SerializeField] private float _speedBoost;
    [SerializeField] private AudioClip SpeedingPlayer;

    private enum Direction {
        Right,
        Down,
        Left,
        Up
    }

    private void OnValidate() {
        switch (_direction) {
            case Direction.Right:
                transform.rotation = Quaternion.Euler(0, -45, 0);
                break;
            case Direction.Down:
                transform.rotation = Quaternion.Euler(0, 45, 0);
                break;
            case Direction.Left:
                transform.rotation = Quaternion.Euler(0, 135, 0);
                break;
            case Direction.Up:
                transform.rotation = Quaternion.Euler(0, 225, 0);
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("Boosting player: " + collision.transform.name);
        if (collision.transform.tag == "Player") {
            Debug.Log("Tag equals player");
            switch (_direction) {
                case Direction.Right:
                    ActivateBoost(collision.gameObject, (Quaternion.Euler(0, -45, 0) * Vector3.right) * _speedBoost); // (Quaternion.Euler(0, -45, 0) * Vector3.right)
                    break;
                case Direction.Down:
                    Debug.Log("Going down.");
                    ActivateBoost(collision.gameObject, (Quaternion.Euler(0, -45, 0) * Vector3.down) * _speedBoost);
                    break;
                case Direction.Left:
                    ActivateBoost(collision.gameObject, (Quaternion.Euler(0, -45, 0) * Vector3.left) * _speedBoost);
                    break;
                case Direction.Up:
                    ActivateBoost(collision.gameObject, (Quaternion.Euler(0, -45, 0) * Vector3.up) * _speedBoost);
                    break;
                default:
                    break;
            }
        }
    }

    private void ActivateBoost(GameObject pGameObject, Vector3 pMultiplier) {
        GetComponent<AudioSource>().PlayOneShot(SpeedingPlayer);
        pGameObject.GetComponent<Rigidbody>().AddForce(pMultiplier, ForceMode.Impulse);
    }
}
