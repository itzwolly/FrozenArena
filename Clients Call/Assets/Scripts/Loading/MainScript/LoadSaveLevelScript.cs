﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadSaveLevelScript : MonoBehaviour {
    [SerializeField] CreateSceneButton _LevelInfo;
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
        Utils.WriteToFile(path, "PlayerMass " + _LevelInfo.GetPlayerMass.ToString()+"|");
        Utils.WriteToFile(path, "BreakableMass " + _LevelInfo.GetBreakableMass.ToString() + "|");
        Utils.WriteToFile(path, "BouncePower " + _LevelInfo.GetBouncepower.ToString() + "|");
        Utils.WriteToFile(path, "FrictionValue " + _LevelInfo.GetIcynessValue.ToString() + "|");
        string info;
        foreach (Transform t in transform)
        {
            info = "";
            if(t.gameObject.GetComponent<State>().Changed)
            {
                info += "?|";
            }
            if (t.gameObject.GetComponent<BombTile>() != null)
            {
                info += "Bomb" + "|"+VectorToString(t.position);
                Utils.WriteToFile(path, info);
            }
            else if (t.tag == "Player")
            {
                info += "Player" + "|" + VectorToString(t.position);
                Utils.WriteToFile(path,info);
            }
            else if (t.gameObject.GetComponent<Rigidbody>() != null)
            {
                info += "Breakable" + "|" + VectorToString(t.position);
                Utils.WriteToFile(path, info);
            }
            else if (t.gameObject.GetComponent<MultiDirectionalBoost>() != null)
            {
                info += "MultiDirectionalBoost" + "|" + VectorToString(t.position);
                Utils.WriteToFile(path, info);
            }
            else if (t.gameObject.GetComponent<OneWayBoost>() != null)
            {
                info += "OneWayBoost" + "|" + VectorToString(t.position);
                Utils.WriteToFile(path, info);
            }
            else if (t.gameObject.GetComponent<SlowDown>() != null)
            {
                info += "SlowBlock" + "|" + VectorToString(t.position);
                Utils.WriteToFile(path, info);
            }
            else
            {
                info += "NormalBlock" + "|" + VectorToString(t.position);
                Utils.WriteToFile(path, info);
            }
        }
    }

    public string VectorToString(Vector3 vec)
    {
        string words = vec.x + "," + vec.y + "," + vec.z;

        return words;
    }
    public Vector3 StringToVector(string str)
    {
        Vector3 vec = new Vector3();
        string[] split = str.Split('(', ',');
        vec.x = Convert.ToSingle(split[0]);
        vec.y = Convert.ToSingle(split[1]);
        vec.z = Convert.ToSingle(split[2]);
        return vec;
    }

}
