using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerHandler : MonoBehaviour {
    private int _totalSeconds;

    public int TotalSeconds {
        get { return _totalSeconds; }
    }

    // Update is called once per frame
    private void Update () {
        _totalSeconds = (int) Time.time;
    }
}
