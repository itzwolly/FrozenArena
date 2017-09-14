using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DLLLibrary;
using UnityEngine;

public class LoadSaveLevelScript : MonoBehaviour
{
    [SerializeField] CreateSceneButton _LevelInfo;
    [SerializeField] GameObject LoadScroll;
    public void SaveLevel()
    {
        string path = "Assets\\Saves\\"+_LevelInfo.GetSceneName+".txt";
        if (File.Exists(path))
        {
            Debug.Log("Level Already Exists, Overwriting");
        }
        else
        {
            Debug.Log("Level will be saved");
            File.Create(path);
        }
        File.WriteAllText(path, String.Empty);
        Utility.WriteToFile(path, "PlayerMass " + "|" + _LevelInfo.GetPlayerMass.ToString());
        Utility.WriteToFile(path, "BreakableMass " + "|" + _LevelInfo.GetBreakableMass.ToString());
        Utility.WriteToFile(path, "BouncePower " + "|" + _LevelInfo.GetBouncepower.ToString());
        Utility.WriteToFile(path, "FrictionValue " + "|" + _LevelInfo.GetIcynessValue.ToString());
        string info;
        bool changed;
        foreach (Transform t in transform)
        {
            changed = false;
            info = "";
            if(t.gameObject.GetComponent<State>().Changed)
            {
                info += "?|";
                changed = true;
            }
            if (t.gameObject.GetComponent<BombTile>() != null)
            {
                info += "Bomb" + "|"+ Utility.VectorToString(t.position);
                if(changed)
                {
                    info += "|"+ t.gameObject.GetComponent<BombTile>().PowerMultiplier.ToString();
                    info += "|"+ t.gameObject.GetComponent<BombTile>().MaxDistance.ToString();
                    info += "|"+ t.gameObject.GetComponent<BombTile>().ExplodeTimer.ToString();
                    info += "|"+ t.gameObject.GetComponent<BombTile>().ResetTime.ToString();
                }
                Utility.WriteToFile(path, info);
            }
            else if (t.tag == "Player")
            {
                info += "Player" + "|" + Utility.VectorToString(t.position);
                Utility.WriteToFile(path,info);
            }
            else if (t.gameObject.GetComponent<Rigidbody>() != null)
            {
                info += "Breakable" + "|" + Utility.VectorToString(t.position);
                if(changed)
                {
                    info += "|"+t.gameObject.GetComponent<Rigidbody>().mass.ToString();
                }
                Utility.WriteToFile(path, info);
            }
            else if (t.gameObject.GetComponent<MultiDirectionalBoost>() != null)
            {
                info += "MultiDirectionalBoost" + "|" + Utility.VectorToString(t.position);
                if(changed)
                {
                    info += "|"+t.gameObject.GetComponent<MultiDirectionalBoost>().SpeedBoost.ToString();
                }
                Utility.WriteToFile(path, info);
            }
            else if (t.gameObject.GetComponent<OneWayBoost>() != null)
            {
                info += "OneWayBoost" + "|" + Utility.VectorToString(t.position);
                if (changed)
                {
                    info += "|"+t.gameObject.GetComponent<OneWayBoost>().SpeedBoost.ToString()+"|";
                    info += ((int)(t.gameObject.GetComponent<OneWayBoost>().Direction)).ToString();
                }
                Utility.WriteToFile(path, info);
            }
            else if (t.gameObject.GetComponent<SlowDown>() != null)
            {
                info += "SlowBlock" + "|" + Utility.VectorToString(t.position);
                if(changed)
                {
                    info += "|"+t.gameObject.GetComponent<SlowDown>().SlowDownMultiplier.ToString()+ "|";
                    info += t.gameObject.GetComponent<SlowDown>().SlowedDownSpeed.ToString();
                }
                Utility.WriteToFile(path, info);
            }
            else
            {
                info += "NormalBlock" + "|" + Utility.VectorToString(t.position);
                Utility.WriteToFile(path, info);
            }
        }
    }
    public void LoadLevel(out string sceneName, out float playerMass,out float breakableMass,
                            out float bouncePower,out float icynessValue)
    {
        string path = "Assets\\Saves\\New Level.txt";
        string[] all = Utility.ReadFromFile(path).Split('\n');
        Debug.Log("Loading level");
        bool changed;
        GameObject currentTile;
        foreach (string s in all)
        {
            changed = false;
            string[] split = s.Split('|');
            int where = 0;
            if(split[0]=="?")
            {
                changed = true;
                where++;
            }

            if (split[where] == "Bomb")
            {
                currentTile = Instantiate(_LevelInfo.GetBombTileBrush);
                where++;
                currentTile.transform.position = Utility.StringToVector(split[where]);
                where++;
                if(changed)
                {

                }
            }
            else if (split[where] == "Player")
            {
                ///separate into selection of the 2 players
                currentTile = Instantiate(_LevelInfo.GetBombTileBrush);
                where++;
                currentTile.transform.position = Utility.StringToVector(split[where]);
                where++;
                if (changed)
                {

                }
            }
            else if (split[where] == "Breakable")
            {
                currentTile = Instantiate(_LevelInfo.GetBreakableTileBrush);
                where++;
                currentTile.transform.position = Utility.StringToVector(split[where]);
                where++;
                if (changed)
                {

                }
            }
            else if (split[where] == "MultiDirectionalBoost")
            {
                currentTile = Instantiate(_LevelInfo.GetMultiDirBoostBrush);
                where++;
                currentTile.transform.position = Utility.StringToVector(split[where]);
                where++;
                if (changed)
                {

                }
            }
            else if (split[where] == "OneWayBoost")
            {
                currentTile = Instantiate(_LevelInfo.GetOneWayBoostBrush);
                where++;
                currentTile.transform.position = Utility.StringToVector(split[where]);
                where++;
                if (changed)
                {

                }
            }
            else if (split[where] == "SlowBlock")
            {
                currentTile = Instantiate(_LevelInfo.GetSlowDownBlockBrush);
                where++;
                currentTile.transform.position = Utility.StringToVector(split[where]);
                where++;
                if (changed)
                {

                }
            }
            else if (split[where] == "NormalBlock")
            {
                currentTile = Instantiate(_LevelInfo.GetNormalBlockBrush);
                where++;
                currentTile.transform.position = Utility.StringToVector(split[where]);
                where++;
                if (changed)
                {

                }
            }
            else
            {
                throw new Exception("dont know this block type");
            }

        }
        sceneName = "New Level";
        playerMass = 50;
        breakableMass = 25;
        bouncePower = 0.8f;
        icynessValue = 0.1f;

    }
}
