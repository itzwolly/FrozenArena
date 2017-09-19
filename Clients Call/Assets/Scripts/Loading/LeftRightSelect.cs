using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftRightSelect : MonoBehaviour
{

    [SerializeField] private Image Left;
    [SerializeField] private Image Right;
    private bool _leftSelected;
    private bool _selected;
    private bool _on;
    public bool On
    {
        get { return _on; }
    }
    // Use this for initialization
    void Start()
    {
        _leftSelected = true;
        _selected = false;

        Color col = Left.color;
        col.a = 0.6f;
        Left.color = col;

        col = Right.color;
        col.a = 0.2f;
        Right.color = col;
    }
    public void Swap()
    {
        if (_leftSelected)
        {
            _leftSelected = false;
            _on = true;
            Color col = Left.color;
            col.a -=0.4f;
            Left.color = col;

            col = Right.color;
            col.a +=0.4f;
            Right.color = col;
        }
        else
        {
            _leftSelected = true;
            _on = false;

            Color col = Left.color;
            col.a += 0.4f;
            Left.color = col;

            col = Right.color;
            col.a -= 0.4f;
            Right.color = col;
        }
    }

    public void Select()
    {
        if (_selected)
        {
            Color col = Left.color;
            col.a -= 0.4f;
            Left.color = col;

            col = Right.color;
            col.a -= 0.4f;
            Right.color = col;
            _selected = false;
        }
        else
        {
            Color col = Left.color;
            col.a += 0.4f;
            Left.color = col;

            col = Right.color;
            col.a += 0.4f;
            Right.color = col;
            _selected = true;
        }
    }
}