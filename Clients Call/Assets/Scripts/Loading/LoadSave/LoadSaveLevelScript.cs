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
    [SerializeField] GameObject _normalTiles;
    [SerializeField] GameObject _specialTiles;
    public Transform SpecialTileSave
    {
        get { return _specialTiles.transform; }
    }
    public Transform NormalTileSave
    {
        get { return _normalTiles.transform; }
    }
    public void SaveLevel()
    {
        string path = _LevelInfo.GetSceneName;
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
        Debug.Log("Level Now Saving from: " + transform.name);
        Utility.WriteToFile(path, "PlayerMass" + "|" + _LevelInfo.GetPlayerMass.ToString());
        Utility.WriteToFile(path, "BreakableMass" + "|" + _LevelInfo.GetBreakableMass.ToString());
        Utility.WriteToFile(path, "BouncePower" + "|" + _LevelInfo.GetBouncepower.ToString());
        Utility.WriteToFile(path, "FrictionValue" + "|" + _LevelInfo.GetIcynessValue.ToString());
        SaveInfo(path, _normalTiles.transform);
        SaveInfo(path, _specialTiles.transform);
    }

    private void SaveInfo(string path, Transform trans)
    {
        string info;
        bool changed;
        foreach (Transform t in trans)
        {
            Debug.Log(t.name);
            changed = false;
            info = "";
            if (t.gameObject.GetComponent<State>() != null && t.gameObject.GetComponent<State>().Changed)
            {
                Debug.Log(t.name + " has stats changed");
                info += "?|";
                changed = true;
            }
            if (t.gameObject.GetComponent<BombTile>() != null)
            {
                info += "Bomb" + "|" + Utility.VectorToString(t.position);
                if (changed)
                {
                    info += "|" + t.gameObject.GetComponent<BombTile>().PowerMultiplier.ToString();
                    info += "|" + t.gameObject.GetComponent<BombTile>().MaxDistance.ToString();
                    info += "|" + t.gameObject.GetComponent<BombTile>().ExplodeTimer.ToString();
                    info += "|" + t.gameObject.GetComponent<BombTile>().ResetTime.ToString();
                }
            }
            else if (t.tag == "Player")
            {
                Debug.Log("player");
                info += "Player" + "|" + t.gameObject.GetComponent<PlayerMovement>().Code.ToString()
                    + "|" + Utility.VectorToString(t.position);
            }
            else if (t.tag == "BreakableTile")
            {
                info += "Breakable" + "|" + Utility.VectorToString(t.position);
                if (changed)
                {
                    info += "|" + t.gameObject.GetComponent<Rigidbody>().mass.ToString();
                }
            }
            else if (t.gameObject.GetComponent<MultiDirectionalBoost>() != null)
            {
                info += "MultiDirectionalBoost" + "|" + Utility.VectorToString(t.position);
                if (changed)
                {
                    info += "|" + t.gameObject.GetComponent<MultiDirectionalBoost>().SpeedBoost.ToString();
                }
            }
            else if (t.gameObject.GetComponent<OneWayBoost>() != null)
            {
                info += "OneWayBoost" + "|" + Utility.VectorToString(t.position);
                if (changed)
                {
                    info += "|" + t.gameObject.GetComponent<OneWayBoost>().SpeedBoost.ToString() + "|";
                    info += ((int)(t.gameObject.GetComponent<OneWayBoost>().GetDirection)).ToString();
                }
            }
            else if (t.gameObject.GetComponent<SlowDown>() != null)
            {
                info += "SlowBlock" + "|" + Utility.VectorToString(t.position);
                if (changed)
                {
                    info += "|" + t.gameObject.GetComponent<SlowDown>().SlowDownMultiplier.ToString() + "|";
                    info += t.gameObject.GetComponent<SlowDown>().SlowedDownSpeed.ToString();
                }
            }
            else
            {
                //Debug.Log("normal block");
                info += "NormalBlock" + "|" + Utility.VectorToString(t.position);
            }
            Utility.WriteToFile(path, info);
        }
    }

    public void LoadLevel(string path, out float playerMass, out float breakableMass,
                            out float bouncePower, out float icynessValue)
    {
        //Debug.Log(sceneName);
        foreach (Transform t in _normalTiles.transform)
        {
            if (t.tag != "StartTile")
                Destroy(t.gameObject);
        }
        foreach (Transform t in _specialTiles.transform)
        {
            if (t.tag != "StartTile")
                Destroy(t.gameObject);
        }

        List<GameObject> players = new List<GameObject>();
        List<BombTile> bombs = new List<BombTile>();
        //string path = "Assets\\Saves\\"+sceneName+".txt";
        string[] all = Utility.ReadFromFile(path).Split('\n');
        bool changed;
        GameObject currentTile;
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
            if (split[0] == "?")
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
            }
            else if (split[where] == "Bomb")
            {
                currentTile = Instantiate(_LevelInfo.GetBombTileBrush);
                where++;
                currentTile.transform.position = Utility.StringToVector(split[where++]);
                if (changed)
                {
                    currentTile.GetComponent<BombTile>().PowerMultiplier = Convert.ToSingle(split[where++]);
                    currentTile.GetComponent<BombTile>().MaxDistance = Convert.ToSingle(split[where++]);
                    currentTile.GetComponent<BombTile>().ExplodeTimer = Convert.ToSingle(split[where++]);
                    currentTile.GetComponent<BombTile>().ResetTime = Convert.ToSingle(split[where++]);
                }
                bombs.Add(currentTile.GetComponent<BombTile>());
                currentTile.transform.SetParent(_specialTiles.transform);
            }
            else if (split[where] == "Player")
            {
                ///separate into selection of the 2 players
                where++;
                int code = Convert.ToInt32(split[where++]);
                Vector3 pos = Utility.StringToVector(split[where++]);
                if (code == 1)
                {
                    currentTile = Instantiate(_LevelInfo.GetPlayer1);
                    currentTile.transform.position = pos;
                    players.Add(currentTile);
                    currentTile.transform.SetParent(_specialTiles.transform);
                }
                else if (code == 2)
                {
                    currentTile = Instantiate(_LevelInfo.GetPlayer2);
                    currentTile.transform.position = pos;
                    players.Add(currentTile);
                    currentTile.transform.SetParent(_specialTiles.transform);
                }
            }
            else if (split[where] == "Breakable")
            {
                currentTile = Instantiate(_LevelInfo.GetBreakableTileBrush);
                where++;
                currentTile.transform.position = Utility.StringToVector(split[where++]);
                if (changed)
                {
                    currentTile.GetComponent<Rigidbody>().mass = Convert.ToSingle(split[where++]);
                }
                currentTile.transform.SetParent(_specialTiles.transform);
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
                currentTile.transform.SetParent(_specialTiles.transform);
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
                    currentTile.GetComponent<OneWayBoost>().GetDirection = (OneWayBoost.Direction)(Convert.ToInt32(split[where++]));
                }
                currentTile.transform.SetParent(_specialTiles.transform);
            }
            else if (split[where] == "SlowBlock")
            {
                currentTile = Instantiate(_LevelInfo.GetSlowDownBlockBrush);
                where++;
                currentTile.transform.position = Utility.StringToVector(split[where]);
                where++;
                if (changed)
                {
                    currentTile.GetComponent<SlowDown>().SlowDownMultiplier = Convert.ToSingle(split[where++]);
                    currentTile.GetComponent<SlowDown>().SlowedDownSpeed = Convert.ToSingle(split[where++]);
                }
                currentTile.transform.SetParent(_specialTiles.transform);
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
                currentTile.transform.SetParent(_normalTiles.transform);
            }
            else
            {
                Debug.Log(split[where] + "----dont know this block type");
            }

        }
        foreach (BombTile b in bombs)
        {
            b.Players = players;
        }
        Debug.Log("finished loading level of: " + all.Length);
    }
}
