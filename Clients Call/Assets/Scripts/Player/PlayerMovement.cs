using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private int _code;
    [SerializeField] private KeyCode _forward;
    [SerializeField] private KeyCode _back;
    [SerializeField] private KeyCode _left;
    [SerializeField] private KeyCode _right;
    [SerializeField] private float _speed;
    [SerializeField] private AudioClip HitBreakable;
    [SerializeField] private AudioClip HitOtherPlayer;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private ParticleSystem _psystem;
    [SerializeField] private ParticleSystem _psystemBoost;
    
    private Dictionary<KeyCode, Action> ButtonActions = new Dictionary<KeyCode, Action>();
    private float _currentSpeed;
    private float _distToGround;
    private Vector3 _lastPosition;

    public int Code
    {
        get { return _code; }
    }

    public float Speed
    {
        get { return _speed; }
    }

    public float CurrentSpeed
    {
        get { return _currentSpeed; }
        set { _currentSpeed = value; }
    }

    private void Awake() {
        _currentSpeed = _speed;
        _lastPosition = transform.position;
    }

    private void Start() {
        _distToGround = GetComponent<Collider>().bounds.extents.y;
        FillButtons();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag=="Player")
        {
            PlayerStatsHandler.Instance.PlayerData[name].AmountOfTimeHitOpponent++;
            GetComponent<AudioSource>().PlayOneShot(HitOtherPlayer);
            _psystem.Play();
        }
        else if(collision.transform.tag == "BreakableTile")
        {
            GetComponent<AudioSource>().PlayOneShot(HitBreakable);
            _psystem.Play();
        }
        else if (collision.transform.tag == "Booster")
        {
            _psystemBoost.Play();
        }
    }

    private void FillButtons()
    {
        ButtonActions.Add(_forward, ForwardAction);
        ButtonActions.Add(_back, BackAction);
        ButtonActions.Add(_left, LeftAction);
        ButtonActions.Add(_right, RightAction);
    }

    private void ForwardAction()
    {
        gameObject.GetComponent<Rigidbody>().AddForce((Vector3.forward * _currentSpeed) * Time.deltaTime);
    }
    private void BackAction()
    {
        gameObject.GetComponent<Rigidbody>().AddForce((Vector3.back * _currentSpeed) * Time.deltaTime);
    }
    private void LeftAction()
    {
        gameObject.GetComponent<Rigidbody>().AddForce((Vector3.left * _currentSpeed) * Time.deltaTime);
    }
    private void RightAction()
    {
        gameObject.GetComponent<Rigidbody>().AddForce((Vector3.right * _currentSpeed) * Time.deltaTime);
    }

    // Update is called once per frame
    void Update () {
        foreach (KeyCode k in ButtonActions.Keys)
        {
            if (Input.GetKey(k))
                ButtonActions[k]();
        }

        if (!IsGrounded() && transform.position.y > 1.1f) {
            PlayerStatsHandler.Instance.PlayerData[name].AirTimeInSeconds += Time.deltaTime;
        }

        if (_lastPosition != transform.position && transform.position.y > 0.99f) {
            PlayerStatsHandler.Instance.PlayerData[name].TotalAmountOfMetersTravelled += Vector3.Distance(transform.position, _lastPosition);
            _lastPosition = transform.position;

            if (GetComponent<Rigidbody>().velocity.magnitude > PlayerStatsHandler.Instance.PlayerData[name].HighestVelocity) {
                PlayerStatsHandler.Instance.PlayerData[name].HighestVelocity = GetComponent<Rigidbody>().velocity.magnitude;
            }
        }

        _audioSource.pitch = gameObject.GetComponent<Rigidbody>().velocity.magnitude - 1;
    }

    private bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, _distToGround + 0.1f);
    }
}
