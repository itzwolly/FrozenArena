using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDataHandler : Singleton<MenuDataHandler> {
    private int _playersReady;
    private bool _isPlayer1Purple;

    private float _masterVolume;
    private float _effectsVolume;
    private float _musicVolume;

    private KeyCode[] _player1Keys = new KeyCode[5];
    private KeyCode[] _player2Keys = new KeyCode[5];

    public float MasterVolume
    {
        get { return _masterVolume; }
        set { _masterVolume = value; }
    }

    public float EffectsVolume
    {
        get { return _effectsVolume; }
        set { _effectsVolume = value; }
    }

    public float MusicVolume
    {
        get { return _musicVolume; }
        set { _musicVolume = value; }
    }

    public int PlayersReady {
        get { return _playersReady; }
        set { _playersReady = value; }
    }
    public bool IsPlayer1Purple {
        get { return _isPlayer1Purple; }
        set { _isPlayer1Purple = value; }
    }
    public KeyCode[] Player1Keys {
        get { return _player1Keys; }
        set { _player1Keys = value; }
    }
    public KeyCode[] Player2Keys {
        get { return _player2Keys; }
        set { _player2Keys = value; }
    }
}
