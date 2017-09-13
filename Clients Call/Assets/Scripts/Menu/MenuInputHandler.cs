using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInputHandler : MonoBehaviour {
    [SerializeField] private KeyCode _keyUp;
    [SerializeField] private KeyCode _keyRight;
    [SerializeField] private KeyCode _keyDown;
    [SerializeField] private KeyCode _keyLeft;

    [SerializeField] private KeyCode _interactionKey;
    [SerializeField] private GameObject _selectedOption;
    [SerializeField] private List<GameObject> _possibleOptionsInOrder;

    private Button _selectedButton;

    private int _currentIndex = 0;

    private void Start() {
        _selectedButton = _selectedOption.GetComponent<Button>();

        // Add listeners
        _selectedButton.onClick.AddListener(OnPlayClick);
    }

    public void OnPlayClick() {
        Debug.Log("Clicked on play");
    }

    private void Update() {
        if (Input.GetKeyUp(_interactionKey)) {
            // Call onClick for selected button
            _selectedButton.onClick.Invoke();
        }

        if (Input.GetKeyUp(_keyUp)) {
            // -2
            _currentIndex -= 2;
        } else if (Input.GetKeyUp(_keyRight)) {
            // +1
            _currentIndex += 1;
        } else if (Input.GetKeyUp(_keyDown)) {
            // +2
            _currentIndex += 2;
        } else if (Input.GetKeyUp(_keyLeft)) {
            // -1
            _currentIndex -= 1;
        }
    }
}
