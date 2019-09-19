using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuilderMenuHandler : MonoBehaviour {
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private KeyCode _interactionKey;

    [SerializeField] private GameObject _on;
    [SerializeField] private GameObject _off;

    [SerializeField] private Sprite _newOn;
    [SerializeField] private Sprite _newOff;

    Sprite _prevSpriteOn;
    Sprite _prevSpriteOff;

    // Use this for initialization
    private void Start () {
        _prevSpriteOn = _on.GetComponent<Image>().sprite;
        _prevSpriteOff = _off.GetComponent<Image>().sprite;
    }

    // Update is called once per frame
    private void Update () {
        if (_eventSystem.currentSelectedGameObject.name == "on")
        {
            if (Input.GetKeyUp(_interactionKey))
            {
                _on.GetComponent<Image>().sprite = _newOn;
                _off.GetComponent<Image>().sprite = _prevSpriteOff;
            }
        }
        else if (_eventSystem.currentSelectedGameObject.name == "off")
        {
            if (Input.GetKeyUp(_interactionKey))
            {
                _on.GetComponent<Image>().sprite = _prevSpriteOn;
                _off.GetComponent<Image>().sprite = _newOff;
            }
        }
        else
        {
            _on.GetComponent<Image>().sprite = _prevSpriteOn;
            _off.GetComponent<Image>().sprite = _prevSpriteOff;
        }
    }
}
