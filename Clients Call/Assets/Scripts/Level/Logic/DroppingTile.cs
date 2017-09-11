﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DLLLibrary;

public class DroppingTile : MonoBehaviour {
    [SerializeField] private StoredInfo Info;
    //[SerializeField] private float _startDelay;
    [SerializeField] private float _delayBetweenTiles;
    [SerializeField] private int _changes;
    [SerializeField] private float _speedOfFall;
    [SerializeField] private float _waitAfterDown;

    private float _timeToNextFall;

    private float _difficultyValue;
    private GameObject _chosenTile;
    private Timer _timer;

    private float _timePassed;

    // Use this for initialization
    private void Start() {
        _difficultyValue = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelConfig>().GetDifficultyValue();
        _timeToNextFall = _delayBetweenTiles;

        _chosenTile = Utility.RandomSelectFromList(Info.MovableCubes);
        _timer = Timer.Register(0.1f, () => StartDropTimer(_chosenTile), isLooped: true);
    }

    private void StartDropTimer(GameObject pTile) {
        TextMesh txtMesh = pTile.GetComponent<TextMesh>();

        float timeLeft = _delayBetweenTiles - _timePassed;
        Debug.Log(timeLeft);
        txtMesh.text = ((int) (timeLeft)).ToString();

        _timePassed += 0.1f;

        if (timeLeft <= 1) {
            _timer.Pause();
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