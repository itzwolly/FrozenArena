using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] string _key;
    public string Letter
    {
        get { return _key; }
    }
    [SerializeField] GameObject _up;
    public GameObject Up
    {
        get { return _up; }
    }
    [SerializeField] GameObject _down;
    public GameObject Down
    {
        get { return _down; }
    }
    [SerializeField] GameObject _left;
    public GameObject Left
    {
        get { return _left; }
    }
    [SerializeField] GameObject _right;
    public GameObject Right
    {
        get { return _right; }
    }
}
