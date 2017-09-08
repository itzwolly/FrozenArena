using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private KeyCode _forward;
    [SerializeField] private KeyCode _back;
    [SerializeField] private KeyCode _left;
    [SerializeField] private KeyCode _right;
    [SerializeField] private KeyCode _ability1;
    [SerializeField] private KeyCode _ability2;
    [SerializeField] private float _speed;
    [SerializeField] private AudioClip HitBreakable;
    [SerializeField] private AudioClip HitOtherPlayer;

    private Dictionary<KeyCode, Action> ButtonActions = new Dictionary<KeyCode, Action>();
    private float _currentSpeed;
    private float _distToGround;
    private float _airTimeTimer;


    public float Speed {
        get { return _speed; }
    }
    public float CurrentSpeed {
        get { return _currentSpeed; }
        set { _currentSpeed = value; }
    }

    private void Awake() {
        _currentSpeed = _speed;
    }

    private void Start() {
        FillButtons();
        _distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag == "Player") {
            GetComponent<AudioSource>().PlayOneShot(HitOtherPlayer);
            // TODO: Increase player hit amount
        } else if (collision.transform.tag == "BreakableTile") {
            GetComponent<AudioSource>().PlayOneShot(HitBreakable);
        }
    }

    private void FillButtons()
    {
        ButtonActions.Add(_forward, ForwardAction);
        ButtonActions.Add(_back, BackAction);
        ButtonActions.Add(_left, LeftAction);
        ButtonActions.Add(_right, RightAction);
        ButtonActions.Add(_ability1, Ability1Action);
        ButtonActions.Add(_ability2, Ability2Action);
    }

    private void ForwardAction()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * _currentSpeed);
    }
    private void BackAction()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * _currentSpeed);
    }
    private void LeftAction()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left * _currentSpeed);
    }
    private void RightAction()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * _currentSpeed);
    }

    private void Ability1Action()
    {

    }
    private void Ability2Action()
    {

    }

    // Update is called once per frame
    void Update () {
        if (!IsGrounded()) {
            _airTimeTimer += Time.deltaTime;
            if (_airTimeTimer >= 1f) { // 1f represents a second.
                // TODO: Add airTimeInSeconds += _airTimeTimer
                _airTimeTimer = 0f;
            }
        }

        foreach (KeyCode k in ButtonActions.Keys) {
            if (Input.GetKey(k))
                ButtonActions[k]();
        }
    }

    public bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, _distToGround + 0.1f);
    }
}
