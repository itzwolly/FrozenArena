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
            string words= "Score|0"+Environment.NewLine+ "ItemsPickedUp|0" + Environment.NewLine +
                "TotalAmountBoosted|0" + Environment.NewLine + "AmountBoostedOneWay|0" + Environment.NewLine +
                "AmountBoostedMulti|0" + Environment.NewLine + "AmoutOfTimeHitOponent|0" + Environment.NewLine +
                "AirTimeInSeconds|0" + Environment.NewLine + "HighestVelocity|0" + Environment.NewLine +
                "TotalAmoutOfMetersTraveled|0";
            string newPath;
            newPath = path + "\\Team1.txt";
            if (!File.Exists(newPath))
            {
                //File.Create(newPath);
                //File.Create(newPath).Dispose();
                Debug.Log("added file for team1");
                Utility.WriteToFile(newPath, words);
            }
            newPath = path + "\\Team2.txt";
            if (!File.Exists(newPath))
            {
                //File.Create(newPath);
                //File.Create(newPath).Dispose();
                Debug.Log("added file for team2");
                Utility.WriteToFile(newPath, words);
            }
        }
        //Debug.Log(path+"\n"+newPath);
        _player1Stats = PlayerStatsHandler.Instance.PlayerData["Player_1"];
        _player2Stats = PlayerStatsHandler.Instance.PlayerData["Player_2"];
        //score for each team
        //save highscores that Alex did
        _player1IsTeam1 = MenuDataHandler.Instance.IsPlayer1Purple;
    }

    public void SaveHighScoresForTeam(string team, PlayerStats stat)
    {
        string teamPath = path + "\\" + team + ".txt";
        float value;
        float actualValue;
        actualValue = stat.Score;
        value = Utility.GetValueAfterString(teamPath, "Score");
        if(actualValue>value)
        {
            Utility.SetValueAfterString(teamPath,"Score",actualValue);
        }
        actualValue = stat.ItemsPickedUp;
        value = Utility.GetValueAfterString(teamPath, "ItemsPickedUp");
        if (actualValue > value)
        {
            Utility.SetValueAfterString(teamPath, "ItemPickedUp", actualValue);
        }
        actualValue = stat.TotalAmountBoosted;
        value = Utility.GetValueAfterString(teamPath, "TotalAmountBoosted");
        if (actualValue > value)
        {
            Utility.SetValueAfterString(teamPath, "TotalAmountBoosted", actualValue);
        }
        actualValue = stat.AmountBoostedOneWay;
        value = Utility.GetValueAfterString(teamPath, "AmountBoostedOneWay");
        if (actualValue > value)
        {
            Utility.SetValueAfterString(teamPath, "AmountBoostedOneWay", actualValue);
        }
        actualValue = stat.AmountBoostedMulti;
        value = Utility.GetValueAfterString(teamPath, "AmountBoostedMulti");
        if (actualValue > value)
        {
            Utility.SetValueAfterString(teamPath, "AmountBoostedMulti", actualValue);
        }
        actualValue = stat.AmountOfTimeHitOpponent;
        value = Utility.GetValueAfterString(teamPath, "AmoutOfTimeHitOponent");
        if (actualValue > value)
        {
            Utility.SetValueAfterString(teamPath, "AmoutOfTimeHitOponent", actualValue);
        }
        actualValue = stat.AirTimeInSeconds;
        value = Utility.GetValueAfterString(teamPath, "AirTimeInSeconds");
        if (actualValue > value)
        {
            Utility.SetValueAfterString(teamPath, "AirTimeInSeconds", actualValue);
        }
        actualValue = stat.HighestVelocity;
        value = Utility.GetValueAfterString(teamPath, "HighestVelocity");
        if (actualValue > value)
        {
            Utility.SetValueAfterString(teamPath, "HighestVelocity", actualValue);
        }
        actualValue = stat.TotalAmountOfMetersTravelled;
        value = Utility.GetValueAfterString(teamPath, "TotalAmoutOfMetersTraveled");
        if (actualValue > value)
        {
            Utility.SetValueAfterString(teamPath, "TotalAmoutOfMetersTraveled", actualValue);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            if (_player1IsTeam1)
            {
                SaveHighScoresForTeam("Team1", _player1Stats);
                SaveHighScoresForTeam("Team2", _player2Stats);
            }
            else
            {
                SaveHighScoresForTeam("Team2", _player1Stats);
                SaveHighScoresForTeam("Team1", _player2Stats);
            }
        }
    }

}
