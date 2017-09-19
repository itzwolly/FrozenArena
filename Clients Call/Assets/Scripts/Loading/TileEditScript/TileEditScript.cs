using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class TileEditScript : MonoBehaviour {

    private int _selection;
    public int Selection
    {
        get { return _selection; }
        set { _selection = value; }
    }
    private int _counter;
    public int Counter
    {
        get { return _counter; }
        set { _counter = value; }
    }

    private bool _pressedLeft;
    public bool PressedLeft
    {
        get { return _pressedLeft; }
        set { _pressedLeft = value; }
    }
    private bool _pressedRight;
    public bool PressedRight
    {
        get { return _pressedRight; }
        set { _pressedRight = value; }
    }

    public void Select(Image obj)
    {
        Color col = obj.color;
        col.a = 1f;
        obj.color = col;
    }
    public void Deselect(Image obj)
    {
        Color col = obj.color;
        col.a = 0.5f;
        obj.color = col;
    }

    public abstract void EditTile(GameObject obj);
    public abstract void ChangeSelection(int value);
    public abstract void UpdateSelected(int value);
}
