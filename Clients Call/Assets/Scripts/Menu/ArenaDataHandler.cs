using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaDataHandler : Singleton<ArenaDataHandler> {
    private List<GameObject> _queuedMaps;

    public List<GameObject> QueuedMaps {
        get { return _queuedMaps; }
        set { _queuedMaps = value; }
    }
}
