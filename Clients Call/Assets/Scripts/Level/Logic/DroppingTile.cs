using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DLLLibrary;
using System;

public class DroppingTile : MonoBehaviour {
    [SerializeField] private StoredInfo Info;

    [SerializeField] private float _startDelay;
    //[SerializeField] private float _delayBetweenTiles;
    [SerializeField] [Range(0.01f, 0.5f)] private float _tickSpeed;
    //[SerializeField] private int _changes;
    [SerializeField] private float _speedOfFall;
    [SerializeField] private float _waitAfterDown;
    
    private GameObject _chosenTile;
    private Timer _timer;

    private float _timePassed;
    private float _difficultyValue;

    // Use this for initialization
    private void Start() {
        _difficultyValue = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelConfig>().GetDifficultyValue();

        _chosenTile = Utility.RandomSelectFromList(Info.MovableCubes);
        _timer = Timer.Register(_startDelay, () => CreateDropTimer());
    }

    private void CreateDropTimer() {
        _timer.Cancel();
        _timer = Timer.Register(_tickSpeed, () => StartDropTimer(_chosenTile), isLooped: true);
    }

    private void StartDropTimer(GameObject pTile) {
        TextMesh txtMesh = pTile.GetComponent<TextMesh>();

        float timeLeft = _difficultyValue - _timePassed;
        txtMesh.text = (timeLeft).ToString("n2");
        _timePassed += 0.1f;

        if (timeLeft <= 0) {
            _timePassed = 0;

            if (Info.MovableCubes.Count > 0) {
                DropTile();
            }
        }
    }

    private void DropTile() {
        _timer.Pause();

        if (Info.MovableCubes.Count == 0) {
            return;
        }

        _chosenTile.GetComponent<TextMesh>().text = "";
        while (_chosenTile.GetComponent<State>().Down || _chosenTile.GetComponent<State>().Up) {
            _chosenTile = Utility.RandomSelectFromList(Info.MovableCubes);
        }

        Info.MovableCubes.Remove(_chosenTile);
        _chosenTile.GetComponent<State>().Down = true;

        float height = 1;
        if (_waitAfterDown >= 0) {
            StartCoroutine(Coroutines.MoveTransformByVector(_chosenTile.transform, DestroyTile, _chosenTile, new Vector3(0, -height, 0), _speedOfFall));
        }

        _chosenTile = Utility.RandomSelectFromList(Info.MovableCubes);
        _timer.Resume();

        //if (Info.MovableCubes.Count > 0) {
        //    StartCoroutine(Coroutines.CallVoidAfterSeconds(DropTile, _timeToNextFall));
        //}
    }

    private void DestroyTile(GameObject obj) {
        StartCoroutine(Coroutines.CallVoidAfterSeconds(Destroy, obj, _waitAfterDown));
    }
}