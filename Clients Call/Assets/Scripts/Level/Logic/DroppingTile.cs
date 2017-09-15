using UnityEngine;
using DLLLibrary;

public class DroppingTile : MonoBehaviour {
    [SerializeField] private StoredInfo Info;
    [SerializeField] private float _startDelay;
    [SerializeField] [Range(0.01f, 0.5f)] private float _tickSpeed;
    [SerializeField] private float _speedOfFall;
    [SerializeField] private float _waitAfterDown;

    private GameObject _chosenTile;
    private Timer _timer;

    private float _timePassed;
    private float _difficultyValue;
    private float _currentLerpTime;
    private float _lerpPercentage;

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
        GameObject child = pTile.transform.GetChild(0).gameObject;

        _currentLerpTime += Time.deltaTime;
        if (_currentLerpTime > _difficultyValue) {
            _currentLerpTime = _difficultyValue;
        }

        _lerpPercentage = _currentLerpTime / _difficultyValue;
        child.GetComponent<Renderer>().material.SetFloat("_FreshSnow", Mathf.Lerp(0, 0.8f, _lerpPercentage));

        if (_lerpPercentage > 0.9f) {
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
        _lerpPercentage = 0;
        _currentLerpTime = 0;

        if (_chosenTile != null) {
            _timer.Resume();
        } else {
            _timer.Cancel();
        }
    }

    private void DestroyTile(GameObject obj) {
        StartCoroutine(Coroutines.CallVoidAfterSeconds(Destroy, obj, _waitAfterDown));
    }
}