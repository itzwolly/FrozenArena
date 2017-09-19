using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class TeamSelection : MonoBehaviour {
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _purpleImage;
    [SerializeField] private Sprite _yellowImage;
    [SerializeField] private RectTransform _transform;
    [SerializeField] private GameObject _return;
    [SerializeField] private PlayerNumber _playerNumber;
    
    [SerializeField] private KeyCode _upKey;
    [SerializeField] private KeyCode _downKey;
    [SerializeField] private KeyCode _leftKey;
    [SerializeField] private KeyCode _rightKey;
    [SerializeField] private KeyCode _interactionKey;

    private GameObject _otherPlayer;
    private KeyCode[] _keys;

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
    public KeyCode InteractionKey {
        get { return _interactionKey; }
    }
    public KeyCode[] Keys {
        get { return _keys; }
    }

    private Vector3 _currentPos;
    private Vector3 _purplePos;
    private Vector3 _yellowPos;
    private Sprite _currentSprite;

    private TeamState _state;
    private TeamState _prevState;
    private bool _ready;
    private int _selectedIndex = 0;

    private void Start() {
        _state = TeamState.NoTeam;
        _currentPos = _transform.anchoredPosition;
        _currentSprite = GetComponent<Image>().sprite;

        _otherPlayer = GameObject.FindGameObjectsWithTag("Player").First(o => o.GetComponent<TeamSelection>().GetPlayerNumber != _playerNumber);
        _keys = new KeyCode[] { _upKey, _downKey, _leftKey, _rightKey, _interactionKey };

        _purplePos = new Vector3(-408.2f, (_playerNumber == PlayerNumber.Player_One) ? -134 : -403, 0);
        _yellowPos = new Vector3(419, (_playerNumber == PlayerNumber.Player_One) ? -134 : -403, 0);
    }
    
    private void Update() {
        NavigateOptions(_upKey, _downKey);
        if (_selectedIndex == 0) {
            MovePlayer(_leftKey, _rightKey, _otherPlayer);
        }
        ConfirmReady(_interactionKey);
    }

    private void HighlightButton(int pIndex) {
        if (pIndex == 0) {
            Image returnImage = _return.GetComponent<Image>();
            Image playerImage = _image;

            Color returnColor = returnImage.color;
            Color playerColor = playerImage.color;

            returnColor.a = 0.5f;
            playerColor.a = 1f;

            returnImage.color = returnColor;
            playerImage.color = playerColor;
        } else {
            Image returnImage = _return.GetComponent<Image>();
            Image playerImage = _image;

            Color returnColor = returnImage.color;
            Color playerColor = playerImage.color;

            returnColor.a = 1f;
            playerColor.a = 0.5f;

            returnImage.color = returnColor;
            playerImage.color = playerColor;
        }
    }

    private void NavigateOptions(KeyCode pUpKey, KeyCode pDownKey) {
        if (_playerNumber == PlayerNumber.Player_One) {
            if (!_ready) {
                if (Input.GetKeyUp(pUpKey)) {
                    _selectedIndex = (_selectedIndex == 0) ? 1 : 0;
                    HighlightButton(_selectedIndex);
                } else if (Input.GetKeyUp(pDownKey)) {
                    _selectedIndex = (_selectedIndex == 1) ? 0 : 1;
                    HighlightButton(_selectedIndex);
                }
            }
        }
    }

    private void ConfirmReady(KeyCode pInteractionKey) {
        if (Input.GetKeyUp(pInteractionKey)) {
            if (_selectedIndex == 0) {
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
            } else {
                SceneManager.LoadScene("Main Menu");
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
