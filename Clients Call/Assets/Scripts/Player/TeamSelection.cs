using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TeamSelection : MonoBehaviour {
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _purpleImage;
    [SerializeField] private Sprite _yellowImage;
    [SerializeField] private RectTransform _transform;
    [SerializeField] private PlayerNumber _playerNumber;

    [SerializeField] private KeyCode _leftKey;
    [SerializeField] private KeyCode _rightKey;
    [SerializeField] private KeyCode _interactionKey;

    private GameObject _otherPlayer;

    public enum PlayerNumber {
        Player_One,
        Player_Two
    }
    public enum TeamState {
        NoTeam,
        Purple,
        Yellow
    }

    public PlayerNumber GetPlayerNumber {
        get { return _playerNumber; }
    }
    public bool Ready {
        get { return _ready; }
    }
    public TeamState State {
        get { return _state; }
    }
    
    private Vector3 _currentPos;
    private Vector3 _purplePos;
    private Vector3 _yellowPos;
    private Sprite _currentSprite;

    private TeamState _state;
    private TeamState _prevState;
    private bool _ready;

    private void Start() {
        _state = TeamState.NoTeam;
        _currentPos = _transform.anchoredPosition;
        _currentSprite = GetComponent<Image>().sprite;

        _otherPlayer = GameObject.FindGameObjectsWithTag("Player").First(o => o.GetComponent<TeamSelection>().GetPlayerNumber != _playerNumber);

        _purplePos = new Vector3(-408.2f, (_playerNumber == PlayerNumber.Player_One) ? -134 : -403, 0);
        _yellowPos = new Vector3(419, (_playerNumber == PlayerNumber.Player_One) ? -134 : -403, 0);
    }
    
    private void Update() {
        MovePlayer(_leftKey, _rightKey, _otherPlayer);
        ConfirmReady(_interactionKey);

        Debug.Log(_state);
    }

    private void ConfirmReady(KeyCode pInteractionKey) {
        if (Input.GetKeyUp(pInteractionKey)) {
            if (!_ready) {
                if (_state == TeamState.Purple) {
                    _image.sprite = _purpleImage;
                    _ready = true;
                } else if (_state == TeamState.Yellow) {
                    _image.sprite = _yellowImage;
                    _ready = true;
                }
            } else {
                _ready = false;
                _image.sprite = _currentSprite;
            }
        }
    }

    private void MovePlayer(KeyCode pKeyLeft, KeyCode pKeyRight, GameObject pOtherPlayer) {
        if (Input.GetKeyUp(pKeyLeft) && !_ready) {
            TeamState otherPlayerState = pOtherPlayer.GetComponent<TeamSelection>().State;
            if (_state == TeamState.NoTeam) {
                if (otherPlayerState == TeamState.Purple) {
                    return;
                }
                _state = TeamState.Purple;
            } else if (_state == TeamState.Yellow) {
                _state = TeamState.NoTeam;
            }

            SetImagePosition();
        } else if (Input.GetKeyUp(pKeyRight) && !_ready) {
            TeamState otherPlayerState = pOtherPlayer.GetComponent<TeamSelection>().State;
            if (_state == TeamState.NoTeam) {
                if (otherPlayerState == TeamState.Yellow) {
                    return;
                }
                _state = TeamState.Yellow;
            } else if (_state == TeamState.Purple) {
                _state = TeamState.NoTeam;
            }

            SetImagePosition();
        }
    }

    private void SetImagePosition() {
        if (_state == TeamState.NoTeam) {
            _transform.anchoredPosition = _currentPos;
            _image.sprite = _currentSprite;
        } else if (_state == TeamState.Purple) {
            _transform.anchoredPosition = _purplePos;
        } else if (_state == TeamState.Yellow) {
            _transform.anchoredPosition = _yellowPos;
        }
    }
}
