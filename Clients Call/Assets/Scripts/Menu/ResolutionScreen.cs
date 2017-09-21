using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionScreen : MonoBehaviour {

    [SerializeField] private Text _winLeft;
    [SerializeField] private Text _winRight;
    [SerializeField] private Text _mTraveledLeft;
    [SerializeField] private Text _mTraveledRight;
    [SerializeField] private Text _airtimeLeft;
    [SerializeField] private Text _airtimeRight;
    [SerializeField] private Text _BouncesLeft;
    [SerializeField] private Text _BouncesRight;
    [SerializeField] private Text _maxSpeedLeft;
    [SerializeField] private Text _maxSpeedRight;

    [SerializeField] private Image _player1Header;
    [SerializeField] private Image _player1PreviewSkin;
    [SerializeField] private Image _player2Header;
    [SerializeField] private Image _player2PreviewSkin;

    [SerializeField] private Text _winnerIndex;

    private PlayerStats _player1Stats;
    private PlayerStats _player2Stats;

    // Use this for initialization
    private void Start() {
        _player1Stats = PlayerStatsHandler.Instance.PlayerData["Player_1"];
        _player2Stats = PlayerStatsHandler.Instance.PlayerData["Player_2"];

        _winLeft.text = _player1Stats.Score.ToString();
        _winRight.text = _player2Stats.Score.ToString();
        _mTraveledLeft.text = _player1Stats.TotalAmountOfMetersTravelled.ToString();
        _mTraveledRight.text = _player2Stats.TotalAmountOfMetersTravelled.ToString();
        _airtimeLeft.text = _player1Stats.AirTimeInSeconds.ToString();
        _airtimeRight.text = _player2Stats.AirTimeInSeconds.ToString();
        _BouncesLeft.text = _player1Stats.AmountOfTimeHitOpponent.ToString();
        _BouncesRight.text = _player2Stats.AmountOfTimeHitOpponent.ToString();
        _maxSpeedLeft.text = _player1Stats.HighestVelocity.ToString();
        _maxSpeedRight.text = _player2Stats.HighestVelocity.ToString();
        _winnerIndex.text = MenuDataHandler.Instance.WinnerIndex.ToString();

        if (MenuDataHandler.Instance.PlayersReady == 1) {
            _player1Header.sprite = MenuDataHandler.Instance.Player1HeaderImage;
            _player1PreviewSkin.sprite = MenuDataHandler.Instance.Player1PreviewSkin;
        } else {
            _player1Header.sprite = MenuDataHandler.Instance.Player1HeaderImage;
            _player2Header.sprite = MenuDataHandler.Instance.Player2HeaderImage;

            _player1PreviewSkin.sprite = MenuDataHandler.Instance.Player1PreviewSkin;
            _player2PreviewSkin.sprite = MenuDataHandler.Instance.Player2PreviewSkin;
        }
    }
}