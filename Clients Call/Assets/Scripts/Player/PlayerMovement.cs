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
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private ParticleSystem _psystem;
    [SerializeField] private ParticleSystem _psystemBoost;


    private Dictionary<KeyCode, Action> ButtonActions = new Dictionary<KeyCode, Action>();
    private float _currentSpeed;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag=="Player")
        {
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

    private void Ability1Action()
    {

    }
    private void Ability2Action()
    {

    }

    // Update is called once per frame
    void Update () {
        foreach (KeyCode k in ButtonActions.Keys)
        {
            if (Input.GetKey(k))
                ButtonActions[k]();
        }
        _audioSource.pitch = gameObject.GetComponent<Rigidbody>().velocity.magnitude-1;
}
}
