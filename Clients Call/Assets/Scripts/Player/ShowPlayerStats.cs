using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerStats : MonoBehaviour {

    private Text _text;
    private PlayerStats _playerStats;

	// Use this for initialization
	void Start () {
        _text = GetComponent<Text>();
        _playerStats = PlayerStatsHandler.Instance.PlayerData[name];

    }
	
	// Update is called once per frame
	void Update () {
        _text.text = ("Airtime in sec: " + _playerStats.AirTimeInSeconds + "\n" +
                      "Total amount boosted: " + _playerStats.TotalAmountBoosted + "\n" +
                      "\t- amount boosted multi: " + _playerStats.AmountBoostedMulti + "\n" +
                      "\t- amount boosted one-way: " + _playerStats.AmountBoostedOneWay + "\n" +
                      "Amount of time hit opponent: " + _playerStats.AmountOfTimeHitOpponent + "\n" +
                      "Total meters travelled: " + _playerStats.TotalAmountOfMetersTravelled + "\n" + 
                      "Highest achieved velocity: " + _playerStats.HighestVelocity).ToString();
    }
}
