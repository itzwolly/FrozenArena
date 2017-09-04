using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public KeyCode Forward;
    public KeyCode Back;
    public KeyCode Left;
    public KeyCode Right;
    public KeyCode Ability1;
    public KeyCode Ability2;
    public PlayerHandler Handler;

    private Dictionary<KeyCode,Action> ButtonActions = new Dictionary<KeyCode, Action>();

    void OnCollisionEnter(Collision c)
    {
        // force is how forcefully we will push the player away from the enemy.

        // If the object we hit is the enemy
        if (c.gameObject.tag == "Player")
        {
            Handler.CollidedWithPlayer(gameObject);
        }
    }

        // Use this for initialization
    void Start ()
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
