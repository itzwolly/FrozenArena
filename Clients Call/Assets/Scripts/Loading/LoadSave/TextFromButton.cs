using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFromButton : MonoBehaviour {
    [SerializeField] Text _textField;
    public Text TextField
    {
        get { return _textField; }
    }
}
