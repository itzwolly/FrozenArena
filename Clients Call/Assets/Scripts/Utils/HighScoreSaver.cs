using System;
using System.Collections;
using System.Collections.Generic;
using DLLLibrary;
using System.IO;
using UnityEngine;

public class HighScoreSaver : MonoBehaviour {

    string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\FrozenArena";
    private PlayerStats _player1Stats;
    private PlayerStats _player2Stats;
    bool _player1IsTeam1;
    // Use this for initialization
    void Start () {
        if(!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        path += "\\HighScores";
        if(!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        _player1Stats = PlayerStatsHandler.Instance.PlayerData["Player_1"];
        _player2Stats = PlayerStatsHandler.Instance.PlayerData["Player_2"];
        //score for each team
        //save highscores that Alex did
        _player1IsTeam1 = MenuDataHandler.Instance.IsPlayer1Purple;
    }

    public void SaveHighScoresForTeam(string team)
    {
        string teamPath = path + "\\" + team + ".txt";
        float value;
        if (!File.Exists(teamPath))
            File.Create(teamPath);
        value = Utility.GetValueAfterString(teamPath, "Score");
        value = Utility.GetValueAfterString(teamPath, "ItemsPickedUp");
        value = Utility.GetValueAfterString(teamPath, "TotalAmountBoosted");
        value = Utility.GetValueAfterString(teamPath, "AmountBoostedOneWay");
        value = Utility.GetValueAfterString(teamPath, "AmountBoostedMulti");
        value = Utility.GetValueAfterString(teamPath, "AmoutOfTimeHitOponent");
        value = Utility.GetValueAfterString(teamPath, "AirTimeInSeconds");
        value = Utility.GetValueAfterString(teamPath, "HighestVelocity");
        value = Utility.GetValueAfterString(teamPath, "TotalAmoutOfMetersTraveled");
    }

}
