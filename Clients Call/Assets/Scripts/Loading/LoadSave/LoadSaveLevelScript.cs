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
        Utility.WriteToFile(path, "PlayerMass " + _LevelInfo.GetPlayerMass.ToString()+"|");
        Utility.WriteToFile(path, "BreakableMass " + _LevelInfo.GetBreakableMass.ToString() + "|");
        Utility.WriteToFile(path, "BouncePower " + _LevelInfo.GetBouncepower.ToString() + "|");
        Utility.WriteToFile(path, "FrictionValue " + _LevelInfo.GetIcynessValue.ToString() + "|");
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
    public void LoadLevel(out string sceneName, out float playerMass,out float breakableMass
                            out float bouncePower,out float icynessValue)
    {
        sceneName = "";
        playerMass = 0;
        breakableMass = 0;
        bouncePower = 0;
        icynessValue = 0;
    }
}
