using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDataHandler : Singleton<MenuDataHandler> {
    private int _playersReady;
    private bool _isPlayer1Purple;

    public int PlayersReady {
        get { return _playersReady; }
        set { _playersReady = value; }
    }
    public bool IsPlayer1Purple {
        get { return _isPlayer1Purple; }
        set { _isPlayer1Purple = value; }
    }
}
