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
        Utility.WriteToFile(path, "PlayerMass" + "|" + _LevelInfo.GetPlayerMass.ToString());
        Utility.WriteToFile(path, "BreakableMass" + "|" + _LevelInfo.GetBreakableMass.ToString());
        Utility.WriteToFile(path, "BouncePower" + "|" + _LevelInfo.GetBouncepower.ToString());
        Utility.WriteToFile(path, "FrictionValue" + "|" + _LevelInfo.GetIcynessValue.ToString());
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
            else if (t.gameObject.GetComponent<PlayerMovement>() != null)
            {
                info += "Player"+"|"+ t.gameObject.GetComponent<PlayerMovement>().Code.ToString() 
                    + "|" + Utility.VectorToString(t.position);
                Utility.WriteToFile(path, info);
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
    public void LoadLevel(string path, out float playerMass,out float breakableMass,
                            out float bouncePower,out float icynessValue)
    {
        //Debug.Log(sceneName);
        foreach(Transform t in transform)
        {
            if(t.tag!="StartTile")
            Destroy(t.gameObject);
        }
        //string path = "Assets\\Saves\\"+sceneName+".txt";
        string[] all = Utility.ReadFromFile(path).Split('\n');
        bool changed;
        GameObject currentTile = new GameObject();
        playerMass = 50;
        breakableMass = 25;
        bouncePower = 0.8f;
        icynessValue = 0.1f;
        foreach (string s in all)
        {
            changed = false;
            string[] split = s.Split('|');
            int where = 0;
            if (split[where] == "")
            {
                continue;
            }
            if (split[0]=="?")
            {
                changed = true;
                where++;
            }

            if (split[where] == "PlayerMass")
            {
                where++;
                playerMass = Convert.ToSingle(split[where]);

            }
            else if (split[where] == "BreakableMass")
            {
                where++;
                breakableMass = Convert.ToSingle(split[where]);
            }
            else if (split[where] == "BouncePower")
            {
                where++;
                bouncePower = Convert.ToSingle(split[where]);
            }
            else if (split[where] == "FrictionValue")
            {
                where++;
                icynessValue = Convert.ToSingle(split[where]);
            }else if (split[where] == "Bomb")
            {
                currentTile = Instantiate(_LevelInfo.GetBombTileBrush);
                where++;
                currentTile.transform.position = Utility.StringToVector(split[where++]);
                if(changed)
                {
                    currentTile.GetComponent<BombTile>().PowerMultiplier = Convert.ToSingle(split[where++]);
                    currentTile.GetComponent<BombTile>().MaxDistance = Convert.ToSingle(split[where++]);
                    currentTile.GetComponent<BombTile>().ExplodeTimer = Convert.ToSingle(split[where++]);
                    currentTile.GetComponent<BombTile>().ResetTime = Convert.ToSingle(split[where++]);
                }
            }
            else if (split[where] == "Player")
            {
                ///separate into selection of the 2 players
                where++;
                Vector3 pos = Utility.StringToVector(split[where++]);
                int code = Convert.ToInt32(split[where++]);
                if (code==1)
                {
                    currentTile = Instantiate(_LevelInfo.GetPlayer1);
                    currentTile.transform.position = pos;
                }
                else if(code==2)
                {
                    currentTile = Instantiate(_LevelInfo.GetPlayer2);
                    currentTile.transform.position = pos;
                }
            }
            else if (split[where] == "Breakable")
            {
                currentTile = Instantiate(_LevelInfo.GetBreakableTileBrush);
                where++;
                currentTile.transform.position = Utility.StringToVector(split[where++]);
                if (changed)
                {
                    currentTile.GetComponent<Rigidbody>().mass=Convert.ToSingle(split[where++]);
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
                    currentTile.GetComponent<MultiDirectionalBoost>().SpeedBoost = Convert.ToSingle(split[where++]);
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
                    currentTile.GetComponent<OneWayBoost>().SpeedBoost = Convert.ToSingle(split[where++]);
                    currentTile.GetComponent<OneWayBoost>().Direction = (OneWayBoost.DirectionValue)(Convert.ToInt32(split[where++]));
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
                    currentTile.GetComponent<SlowDown>().SlowDownMultiplier=Convert.ToSingle(split[where++]);
                    currentTile.GetComponent<SlowDown>().SlowedDownSpeed = Convert.ToSingle(split[where++]);
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
                Debug.Log(split[where]+"----dont know this block type");
            }
            currentTile.transform.SetParent(transform);

        }

    }
}
