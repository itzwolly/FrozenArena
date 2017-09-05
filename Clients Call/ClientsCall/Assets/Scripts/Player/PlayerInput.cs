using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    [SerializeField] private KeyCode Forward;
    [SerializeField] private KeyCode Back;
    [SerializeField] private KeyCode Left;
    [SerializeField] private KeyCode Right;
    [SerializeField] private KeyCode Ability1;
    [SerializeField] private KeyCode Ability2;

    private Dictionary<KeyCode,Action> ButtonActions = new Dictionary<KeyCode, Action>();

    // Use this for initialization
    private void Start ()
    {
        FillButtons();
    }

    private void FillButtons()
    {
        ButtonActions.Add(Forward, ForwardAction);
        ButtonActions.Add(Back, BackAction);
        ButtonActions.Add(Left, LeftAction);
        ButtonActions.Add(Right, RightAction);
        ButtonActions.Add(Ability1, Ability1Action);
        ButtonActions.Add(Ability2, Ability2Action);
    }

    private void ForwardAction()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward*10);
    }

    private void BackAction()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * 10);
    }

    private void LeftAction()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left * 10);
    }

    private void RightAction()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * 10);
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
	}
}
