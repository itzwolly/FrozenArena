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
        if (collision.transform.tag == "Player") {
            switch (_direction) {
                case Direction.Right:
                    ActivateBoost(collision.gameObject, Vector3.right * _speedBoost);
                    break;
                case Direction.Down:
                    ActivateBoost(collision.gameObject, Vector3.back * _speedBoost);
                    break;
                case Direction.Left:
                    ActivateBoost(collision.gameObject, Vector3.left * _speedBoost);
                    break;
                case Direction.Up:
                    ActivateBoost(collision.gameObject, Vector3.forward * _speedBoost);
                    break;
                default:
                    break;
            }
        }
    }

    private void ActivateBoost(GameObject pGameObject, Vector3 pMultiplier) {
        GetComponent<AudioSource>().PlayOneShot(SpeedingPlayer);
        pGameObject.GetComponent<Rigidbody>().AddForce(pMultiplier, ForceMode.Impulse);

        PlayerStatsHandler.Instance.PlayerData[pGameObject.name].TotalAmountBoosted++;
        PlayerStatsHandler.Instance.PlayerData[pGameObject.name].AmountBoostedOneWay++;
    }
}
