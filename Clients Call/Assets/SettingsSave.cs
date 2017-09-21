using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using DLLLibrary;

public class SettingsSave : MonoBehaviour
{
    [SerializeField] string _saveName;
    [SerializeField] LeftRightSelect _acceptDecline;
    [SerializeField] Button _button;
    [SerializeField] Slider _masterVolume;
    [SerializeField] Slider _effectsVolume;
    [SerializeField] Slider _musicVolume;
    string _saveLocation;
    private const float DEFAULTMASTER=1f;
    private const float DEFAULTEFFECTS=0.8f;
    private const float DEFAULTMUSIC = 0.8f;
    MenuDataHandler _handler;
    string path;
    // Use this for initialization
    void Start ()
    {
        path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\FrozenArena";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        path += "\\"+_saveName+".txt";
        _handler = MenuDataHandler.Instance;
        _handler.MasterVolume = GetSavedMasterVolume();
        _handler.EffectsVolume = GetSavedEffectsVolume();
        _handler.MusicVolume = GetSavedMusicVolume();

        if (!File.Exists(path))
        {
            SetToDefault();
        }
        else
        {
            SetValues(_handler.MasterVolume,_handler.EffectsVolume,_handler.MusicVolume);
        }

    }

    private void SetValues(float mast,float effec, float musi)
    {
        _masterVolume.value = mast;
        _effectsVolume.value = effec;
        _musicVolume.value = musi;
    }

	private void SetToDefault()
    {
        Utility.WriteToFile(path, "");
        string words = "MasterVolume|" + DEFAULTMASTER + Environment.NewLine + "EffectsVolume|" + DEFAULTEFFECTS
            + Environment.NewLine + "MusicVolume|" + DEFAULTMUSIC;
        Utility.WriteToFile(path, words);
        _handler.MasterVolume = DEFAULTMASTER;
        _handler.EffectsVolume = DEFAULTEFFECTS;
        _handler.MusicVolume = DEFAULTMUSIC;
    }
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Space))
        {
            if(_acceptDecline.On)
                SaveSettings();

            //return to menu here
        }
    }

    private float GetSavedMasterVolume()
    {
        return  Utility.GetValueAfterString(path, "MasterVolume");
    }

    private float GetSavedEffectsVolume()
    {
        return Utility.GetValueAfterString(path, "EffectsVolume");
    }

    private float GetSavedMusicVolume()
    {
        return Utility.GetValueAfterString(path, "MusicVolume");
    }

    private void SaveSettings()
    {
        Debug.Log("Saving");
        Utility.SetValueAfterString(path, "MasterVolume", _masterVolume.value);
        Utility.SetValueAfterString(path, "EffectsVolume", _effectsVolume.value);
        Utility.SetValueAfterString(path, "MusicVolume", _musicVolume.value);
    }
}
