using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GamePreviewHandler : MonoBehaviour {
    [SerializeField] private Image _player1Header;
    [SerializeField] private Image _player2Header;

    [SerializeField] private Image _player1Skin;
    [SerializeField] private Image _player2Skin;

    [SerializeField] private Sprite[] _player1Headers;
    [SerializeField] private Sprite[] _player2Headers;

    [SerializeField] private Sprite[] _difficultySprites;
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private GameObject _difficultyContainer;

    [SerializeField] private KeyCode _leftKey;
    [SerializeField] private KeyCode _rightKey;

    private bool _isPlayer1Purple;
    private int _amountPlayersReady;
    private int _currentlySelected;
    
	// Use this for initialization
	private void Awake () {
        _isPlayer1Purple = MenuDataHandler.Instance.IsPlayer1Purple;
        _amountPlayersReady = MenuDataHandler.Instance.PlayersReady;
    }

    private void Start() {
        if (_amountPlayersReady == 1) {
            _player1Skin.sprite = MenuDataHandler.Instance.Player1PreviewSkin;

            if (_isPlayer1Purple) {
                _player1Header.sprite = _player1Headers[0];
            } else {
                _player1Header.sprite = _player1Headers[1];
            }
        } else {
            _player1Skin.sprite = MenuDataHandler.Instance.Player1PreviewSkin;
            _player2Skin.sprite = MenuDataHandler.Instance.Player2PreviewSkin;

            if (_isPlayer1Purple) {
                _player1Header.sprite = _player1Headers[0];
                _player2Header.sprite = _player2Headers[1];
            } else {
                _player1Header.sprite = _player1Headers[1];
                _player2Header.sprite = _player2Headers[0];
            }
        }
    }

    private void Update() {
        if (_eventSystem != null) {
            if (_eventSystem.currentSelectedGameObject != null) {
                if (_eventSystem.currentSelectedGameObject != _difficultyContainer) { return; }

                if (_eventSystem.currentSelectedGameObject == _difficultyContainer) {
                    if (Input.GetKeyUp(_leftKey)) {
                        if (_currentlySelected == 0) {
                            _currentlySelected = 2;
                        } else {
                            _currentlySelected--;
                        }

                        SetDifficultySprite(_currentlySelected);
                    } else if (Input.GetKeyUp(_rightKey)) {
                        if (_currentlySelected == 2) {
                            _currentlySelected = 0;
                        } else {
                            _currentlySelected++;
                        }
                        SetDifficultySprite(_currentlySelected);
                    }
                }
            }
        }
    }

    private void SetDifficultySprite(int pCurrentlySelected) {
        _difficultyContainer.GetComponent<Image>().sprite = _difficultySprites[pCurrentlySelected];
    }
}
