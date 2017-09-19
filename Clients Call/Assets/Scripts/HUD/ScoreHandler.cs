using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour {

    [SerializeField] private GameObject _p1ImageContainer;
    [SerializeField] private GameObject _p2ImageContainer;

    [SerializeField] private Sprite[] _p1ScoreImages;
    [SerializeField] private Sprite[] _p2ScoreImages;

    private PlayerStatsHandler _handler;
    private PlayerStats _p1Stats;
    private PlayerStats _p2Stats;

    private Image _p1Image;
    private Image _p2Image;

	// Use this for initialization
	private void Start () {
        DontDestroyOnLoad(transform.root.transform);

        _handler = PlayerStatsHandler.Instance;

        _p1Stats = _handler.PlayerData["Player_1"];
        _p2Stats = _handler.PlayerData["Player_2"];

        _p1Image = _p1ImageContainer.GetComponent<Image>();
        _p2Image = _p2ImageContainer.GetComponent<Image>();


        if (_p1Stats.Score <= 0) {
            _p1Image.color = Color.clear;
        } else {
            _p1Image.color = Color.white;
            _p1Image.sprite = _p1ScoreImages[(_p1Stats.Score - 1)];
        }

        if (_p2Stats.Score <= 0) {
            _p2Image.color = Color.clear;
        } else {
            _p2Image.color = Color.white;
            _p2Image.sprite = _p2ScoreImages[(_p2Stats.Score - 1)];
        }
    }
}
