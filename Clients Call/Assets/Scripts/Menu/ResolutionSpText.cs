using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSpText : MonoBehaviour {

    [SerializeField] private Text _pointsText;
    [SerializeField] private Text _airtimeInSeconds;
    [SerializeField] private Text _travelledInMeters;
    [SerializeField] private Text _maxSpeed;

    //[SerializeField] private Image _player1Header;
    [SerializeField] private Image _player1PreviewSkin;

	// Use this for initialization
	void Start () {
        _pointsText.text = PlayerStatsHandler.Instance.PlayerData["Player_1"].ItemsPickedUp.ToString();
        _airtimeInSeconds.text = PlayerStatsHandler.Instance.PlayerData["Player_1"].AirTimeInSeconds.ToString();
        _travelledInMeters.text = PlayerStatsHandler.Instance.PlayerData["Player_1"].TotalAmountOfMetersTravelled.ToString();
        _maxSpeed.text = PlayerStatsHandler.Instance.PlayerData["Player_1"].HighestVelocity.ToString();

        //_player1Header.sprite = MenuDataHandler.Instance.Player1HeaderImage;
        _player1PreviewSkin.sprite = MenuDataHandler.Instance.Player1PreviewSkin;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
