using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayBoost : MonoBehaviour {
    [SerializeField] private DirectionValue _direction;
    [SerializeField] private float _speedBoost;
    [SerializeField] private AudioClip SpeedingPlayer;
    [SerializeField] private ParticleSystem _psystem;

    private DirectionValue _prevDirection;

    public float SpeedBoost
    {
        get { return _speedBoost; }
        set { _speedBoost = value; }
    }

    public DirectionValue Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    private void Start()
    {
        _prevDirection = _direction;
    }

    private void Update()
    {
        if(_prevDirection!=_direction)
        {
            LevelEditorValidate();
            _prevDirection = _direction;
        }
    }

    public enum DirectionValue {
        Right,
        Down,
        Left,
        Up
    }
    
    private void LevelEditorValidate()
    {
        switch (_direction)
        {
            case DirectionValue.Right:
                transform.rotation = Quaternion.Euler(0, -90, 0);
                break;
            case DirectionValue.Down:
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case DirectionValue.Left:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            case DirectionValue.Up:
                transform.rotation = Quaternion.Euler(0, 270, 0);
                break;
            default:
                break;
        }
    }

    private void OnValidate() {
        switch (_direction) {
            case DirectionValue.Right:
                transform.rotation = Quaternion.Euler(0, -45, 0);
                break;
            case DirectionValue.Down:
                transform.rotation = Quaternion.Euler(0, 45, 0);
                break;
            case DirectionValue.Left:
                transform.rotation = Quaternion.Euler(0, 135, 0);
                break;
            case DirectionValue.Up:
                transform.rotation = Quaternion.Euler(0, 225, 0);
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("Boosting player: " + collision.transform.name);
        _psystem.Play();
        if (collision.transform.tag == "Player") {
            Debug.Log("Tag equals player");
            switch (_direction) {
                case DirectionValue.Right:
                    ActivateBoost(collision.gameObject, Vector3.right * _speedBoost);
                    break;
                case DirectionValue.Down:
                    Debug.Log("Going down.");
                    ActivateBoost(collision.gameObject, Vector3.back * _speedBoost);
                    break;
                case DirectionValue.Left:
                    ActivateBoost(collision.gameObject, Vector3.left * _speedBoost);
                    break;
                case DirectionValue.Up:
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
    }
}
