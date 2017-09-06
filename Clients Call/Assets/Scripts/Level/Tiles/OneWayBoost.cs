using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayBoost : MonoBehaviour {
    [SerializeField] private Direction _direction;
    [SerializeField] private float _speedBoost;

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
                    ActivateBoost(collision.gameObject, new Vector3(0.5f, 0, 0) * _speedBoost); // 1, 0, 0
                    break;
                case Direction.Down:
                    Debug.Log("Going down.");
                    ActivateBoost(collision.gameObject, new Vector3(0, 0, -0.5f) * _speedBoost); // 0, 0, -1
                    break;
                case Direction.Left:
                    ActivateBoost(collision.gameObject, new Vector3(-0.5f, 0, 0) * _speedBoost); // -1, 0 ,0
                    break;
                case Direction.Up:
                    ActivateBoost(collision.gameObject, new Vector3(0, 0, 0.5f) * _speedBoost); // 0, 0, 1
                    break;
                default:
                    break;
            }
        }
    }

    private void ActivateBoost(GameObject pGameObject, Vector3 pMultiplier) {
        pGameObject.GetComponent<Rigidbody>().AddForce(pMultiplier, ForceMode.Impulse);
    }
}
