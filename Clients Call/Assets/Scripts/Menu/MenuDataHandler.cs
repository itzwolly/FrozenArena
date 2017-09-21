﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDataHandler : Singleton<MenuDataHandler> {
    private int _playersReady;
    private bool _isPlayer1Purple;
    
    private KeyCode[] _player1Keys = new KeyCode[5];
    private KeyCode[] _player2Keys = new KeyCode[5];

    private Sprite _player1PreviewSkin;
    private Sprite _player2PreviewSkin;
    private Sprite _player1HeaderImage;
    private Sprite _player2HeaderImage;
    
    private List<MapData> _queuedMaps;
    private List<MapData> _copyQueuedMaps;

    private bool _hasCreatedEmitter;

    private int _winnerIndex;

    public bool HasCreatedEmitter {
        get { return _hasCreatedEmitter; }
        set { _hasCreatedEmitter = value; }
    }

    public List<MapData> QueuedMaps {
        get { return _queuedMaps; }
        set { _queuedMaps = value; }
    }
    public List<MapData> CopyQueuedMaps {
        get { return _copyQueuedMaps; }
        set { _copyQueuedMaps = value; }
    }
    public Sprite Player1PreviewSkin {
        get { return _player1PreviewSkin; }
        set { _player1PreviewSkin = value; }
    }
    public Sprite Player2PreviewSkin {
        get { return _player2PreviewSkin; }
        set { _player2PreviewSkin = value; }
    }

    public Sprite Player1HeaderImage {
        get { return _player1HeaderImage; }
        set { _player1HeaderImage = value; }
    }

    public Sprite Player2HeaderImage {
        get { return _player2HeaderImage; }
        set { _player2HeaderImage = value; }
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
    public int WinnerIndex {
        get { return _winnerIndex; }
        set { _winnerIndex = value; }
    }
}
