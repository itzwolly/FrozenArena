using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DLLLibrary;

public class DroppingTile : MonoBehaviour {
    [SerializeField] private StoredInfo Info;
    [SerializeField] private float _delayTime;
    [SerializeField] private float _time;
    [SerializeField] private int _changes;
    [SerializeField] private float _speedOfFall;
    [SerializeField] private float _waitAfterDown;

    private float _timeToNextFall;
    private bool _countingDown = false;

    private int _difficultyValue;

    // Use this for initialization
    private void Start() {
        _difficultyValue = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelConfig>().GetDifficultyValue();

        _timeToNextFall = _time;
        _delayTime -= _difficultyValue;

        StartCoroutine(Coroutines.CallVoidAfterSeconds(DropTile, _delayTime));
    }

    private void DropTile() {
        if (Info.MovableCubes.Count == 0) {
            return;
        }

        GameObject obj = Utility.RandomSelectFromList(Info.MovableCubes);
        StartCoroutine(WaitAndCountDown(obj, _difficultyValue));
        StartCoroutine(WaitAndExecute(obj));
    }

    private void DestroyTile(GameObject obj) {
        StartCoroutine(Coroutines.CallVoidAfterSeconds(Destroy,obj, _waitAfterDown));
    }

    private IEnumerator WaitAndCountDown(GameObject pTile, float pWaitTime) {
        _countingDown = true;
        TextMesh txtMesh = pTile.GetComponent<TextMesh>();
        for (int i = 0; i <= pWaitTime; i++) {
            yield return new WaitForSeconds(1); // need to wait 1 second because we are counting down
            if (txtMesh != null) {
                txtMesh.text = (pWaitTime - i).ToString();
            }
        }
        txtMesh.text = "";
        _countingDown = false;
    }

    private IEnumerator WaitAndExecute(GameObject pObj) {
        // this stalls the tile from falling down before the count is at 0.
        while (_countingDown) {
            yield return new WaitForSeconds(0.1f);
        }

        GameObject obj = pObj;
        int count = 0;

        while (obj.GetComponent<State>().Down || obj.GetComponent<State>().Up) {
            obj = Utility.RandomSelectFromList(Info.MovableCubes);
            count++;
        }

        Info.MovableCubes.Remove(obj);
        obj.GetComponent<State>().Down = true;

        float height = 1;
        if (_waitAfterDown >= 0) {
            StartCoroutine(Coroutines.MoveTransformByVector(obj.transform, DestroyTile, obj, new Vector3(0, -height, 0), _speedOfFall));
        }
        if (Info.MovableCubes.Count > 0) {
            StartCoroutine(Coroutines.CallVoidAfterSeconds(DropTile, _timeToNextFall));
        }
    }
}
