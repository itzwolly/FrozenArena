using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkinSelectionHandler : MonoBehaviour {

    [SerializeField] private GameObject _singlePlayerLayout;
    [SerializeField] private GameObject _multiPlayerLayout;

    [SerializeField] private GameObject _player1Indicator;
    [SerializeField] private GameObject _player2Indicator;

    [SerializeField] private GameObject _eventSystem;
    [SerializeField] private GameObject _returnButton;

    [Space(5)]
    [Header("Make sure that purple is the first and yellow second.")]
    [SerializeField] private Sprite[] _player1IndicatorSprites;
    [SerializeField] private Sprite[] _player2IndicatorSprites;

    [Space(5)]
    [Header("Make sure that stone is the first and gold second.")]
    [SerializeField] private Sprite[] _player1BallSprites;
    [SerializeField] private Sprite[] _player2BallSprites;

    private int _currentlySelectedIndexP1 = 0;
    private int _currentlySelectedIndexP2 = 0;

    private KeyCode[] _player1Keys;
    private KeyCode[] _player2Keys;

    private GameObject _player1PreviewSkin;
    private GameObject _player2PreviewSkin;

    private Sprite _player1Sprite;
    private Sprite _player2Sprite;

    private RectTransform _p1RectTransform;
    private RectTransform _p2RectTransform;

    private bool _p1Ready = false;
    private bool _p2Ready = false;

    private int _selectedIndex = 0;

    public bool P1Ready {
        get { return _p1Ready; }
    }
    public bool P2Ready {
        get { return _p2Ready; }
    }

    // Use this for initialization
    void Start () {
        Debug.Log(MenuDataHandler.Instance.PlayersReady + " | " + MenuDataHandler.Instance.IsPlayer1Purple);
        
        if (MenuDataHandler.Instance.PlayersReady == 1) {
            _singlePlayerLayout.SetActive(true);
            _multiPlayerLayout.SetActive(false);
            _player2Indicator.SetActive(false);

            _player1Keys = MenuDataHandler.Instance.Player1Keys;

            GameObject headerObject = GameObject.FindGameObjectWithTag("HeaderSP");
            _player1PreviewSkin = headerObject.transform.parent.GetChild(1).gameObject;
            if (MenuDataHandler.Instance.IsPlayer1Purple) {
                _player1Sprite = _player1IndicatorSprites[0];
                _player1PreviewSkin.GetComponent<Image>().sprite = _player1BallSprites[0];

                headerObject.transform.GetChild(0).gameObject.SetActive(true);
                headerObject.transform.GetChild(1).gameObject.SetActive(false);
            } else {
                _player1Sprite = _player1IndicatorSprites[1];
                _player1PreviewSkin.GetComponent<Image>().sprite = _player1BallSprites[2];

                headerObject.transform.GetChild(0).gameObject.SetActive(false);
                headerObject.transform.GetChild(1).gameObject.SetActive(true);
            }

            _player1Indicator.GetComponent<Image>().sprite = _player1Sprite;
        } else {
            _singlePlayerLayout.SetActive(false);
            _multiPlayerLayout.SetActive(true);

            _player1Keys = MenuDataHandler.Instance.Player1Keys;
            _player2Keys = MenuDataHandler.Instance.Player2Keys;

            GameObject headerObject = GameObject.FindGameObjectWithTag("HeaderMP");
            _player1PreviewSkin = headerObject.transform.parent.GetChild(1).gameObject;
            _player2PreviewSkin = headerObject.transform.parent.GetChild(2).gameObject;
            if (MenuDataHandler.Instance.IsPlayer1Purple) {
                _player1Sprite = _player1IndicatorSprites[0];
                _player2Sprite = _player2IndicatorSprites[1];

                _player1PreviewSkin.GetComponent<Image>().sprite = _player1BallSprites[0];
                _player2PreviewSkin.GetComponent<Image>().sprite = _player2BallSprites[0];

                headerObject.transform.GetChild(0).gameObject.SetActive(true);
                headerObject.transform.GetChild(1).gameObject.SetActive(false);

                headerObject.transform.GetChild(2).gameObject.SetActive(false);
                headerObject.transform.GetChild(3).gameObject.SetActive(true);
            } else {
                _player1Sprite = _player1IndicatorSprites[1];
                _player2Sprite = _player2IndicatorSprites[0];

                _player1PreviewSkin.GetComponent<Image>().sprite = _player1BallSprites[2];
                _player2PreviewSkin.GetComponent<Image>().sprite = _player2BallSprites[2];

                headerObject.transform.GetChild(0).gameObject.SetActive(false);
                headerObject.transform.GetChild(1).gameObject.SetActive(true);

                headerObject.transform.GetChild(2).gameObject.SetActive(true);
                headerObject.transform.GetChild(3).gameObject.SetActive(false);
            }

            _player1Indicator.GetComponent<Image>().sprite = _player1Sprite;
            _player2Indicator.GetComponent<Image>().sprite = _player2Sprite;
        }

        _p1RectTransform = _player1Indicator.GetComponent<RectTransform>();
        _p2RectTransform = _player2Indicator.GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	private void Update () {
        if (MenuDataHandler.Instance.PlayersReady == 1) {
            CheckPlayer1Controls();
        } else {
            CheckPlayer1Controls();
            CheckPlayer2Controls();
        }
    }

    private void Player1InteractionClick() {
        Image image = _player1PreviewSkin.GetComponent<Image>();
        Color c = image.color;

        if (_p1Ready) {
            c.a = 0.49f;
            image.color = c;

            _player1PreviewSkin.transform.GetChild(0).gameObject.SetActive(true);
        } else {
            c.a = 1f;
            image.color = c;

            _player1PreviewSkin.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void Player2InteractionClick() {
        Image image = _player2PreviewSkin.GetComponent<Image>();
        Color c = image.color;

        if (_p2Ready) {
            c.a = 0.49f;
            image.color = c;

            _player2PreviewSkin.transform.GetChild(0).gameObject.SetActive(true);
        } else {
            c.a = 1f;
            image.color = c;

            _player2PreviewSkin.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void CheckPlayer1Controls() {
        // order of array; up, down, left, right, interaction
        if (!_p1Ready) {
            if (Input.GetKeyUp(_player1Keys[0])) {
                //up
                _selectedIndex = (_selectedIndex == 0) ? 1 : 0;
                HighlightButton(_selectedIndex);
            } else if (Input.GetKeyUp(_player1Keys[1])) {
                // down
                _selectedIndex = (_selectedIndex == 1) ? 0 : 1;
                HighlightButton(_selectedIndex);
            } else if (Input.GetKeyUp(_player1Keys[2])) {
                // left
                _currentlySelectedIndexP1 = (_currentlySelectedIndexP1 == 0) ? 1 : 0;
                _p1RectTransform.anchoredPosition = (_currentlySelectedIndexP1 == 0) ? new Vector3(-94, -87.5f, 0) : new Vector3(12.7f, -87.5f);
                SetPlayer1Skin();
            } else if (Input.GetKeyUp(_player1Keys[3])) {
                // right
                _currentlySelectedIndexP1 = (_currentlySelectedIndexP1 == 0) ? 1 : 0;
                _p1RectTransform.anchoredPosition = (_currentlySelectedIndexP1 == 0) ? new Vector3(-94, -87.5f) : new Vector3(12.7f, -87.5f);
                SetPlayer1Skin();
            }
        }

        if (Input.GetKeyUp(_player1Keys[4])) {
            // interaction
            if (_selectedIndex == 0) {
                _p1Ready = !_p1Ready;
                Player1InteractionClick();
            } else {
                SceneManager.LoadScene("Team Select");
            }
        }
    }

    private void HighlightButton(int pIndex) {
        if (pIndex == 0) {
            Image returnImage = _returnButton.GetComponent<Image>();
            Image playerImage = _player1Indicator.GetComponent<Image>();

            Color returnColor = returnImage.color;
            Color playerColor = playerImage.color;

            returnColor.a = 0.5f;
            playerColor.a = 1f;

            returnImage.color = returnColor;
            playerImage.color = playerColor;
        } else {
            Image returnImage = _returnButton.GetComponent<Image>();
            Image playerImage = _player1Indicator.GetComponent<Image>();

            Color returnColor = returnImage.color;
            Color playerColor = playerImage.color;

            returnColor.a = 1f;
            playerColor.a = 0.5f;

            returnImage.color = returnColor;
            playerImage.color = playerColor;
        }
    }

    private void CheckPlayer2Controls() {
        // order of array; up, down, left, right, interaction
        if (!_p2Ready) {
            if (Input.GetKeyUp(_player2Keys[1])) {
                // down
                //_eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(_returnButton);
            } else if (Input.GetKeyUp(_player2Keys[2])) {
                // left
                _currentlySelectedIndexP2 = (_currentlySelectedIndexP2 == 0) ? 1 : 0;
                _p2RectTransform.anchoredPosition = (_currentlySelectedIndexP2 == 0) ? new Vector3(-20.2f, -87.5f) : new Vector3(86.9f, -87.5f);
                SetPlayer2Skin();
            } else if (Input.GetKeyUp(_player2Keys[3])) {
                // right
                _currentlySelectedIndexP2 = (_currentlySelectedIndexP2 == 0) ? 1 : 0;
                _p2RectTransform.anchoredPosition = (_currentlySelectedIndexP2 == 0) ? new Vector3(-20.2f, -87.5f) : new Vector3(86.9f, -87.5f);
                SetPlayer2Skin();
            }
        }

        if (Input.GetKeyUp(_player2Keys[4])) {
            // interaction
            _p2Ready = !_p2Ready;
            Player2InteractionClick();
        }
    }

    private void SetPlayer1Skin() {
        if (_currentlySelectedIndexP1 == 0) {
            // set image to stone
            if (MenuDataHandler.Instance.IsPlayer1Purple) {
                _player1PreviewSkin.GetComponent<Image>().sprite = _player1BallSprites[0];
            } else {
                _player1PreviewSkin.GetComponent<Image>().sprite = _player1BallSprites[2];
            }
        } else if (_currentlySelectedIndexP1 == 1) {
            // set image to gold
            if (MenuDataHandler.Instance.IsPlayer1Purple) {
                _player1PreviewSkin.GetComponent<Image>().sprite = _player1BallSprites[1];
            } else {
                _player1PreviewSkin.GetComponent<Image>().sprite = _player1BallSprites[3];
            }
        }
    }
    
    private void SetPlayer2Skin() {
        if (_currentlySelectedIndexP2 == 0) {
            // set image to stone
            if (MenuDataHandler.Instance.IsPlayer1Purple) {
                _player2PreviewSkin.GetComponent<Image>().sprite = _player2BallSprites[0];
            } else {
                _player2PreviewSkin.GetComponent<Image>().sprite = _player2BallSprites[2];
            }
        } else if (_currentlySelectedIndexP2 == 1) {
            // set image to gold
            if (MenuDataHandler.Instance.IsPlayer1Purple) {
                _player2PreviewSkin.GetComponent<Image>().sprite = _player2BallSprites[1];
            } else {
                _player2PreviewSkin.GetComponent<Image>().sprite = _player2BallSprites[3];
            }
        }
    }
}
