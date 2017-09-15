using UnityEngine;
using DLLLibrary;
using System.Linq;

public class GameEndDetect : MonoBehaviour {
    [SerializeField] private float _timeToEnd;
    [SerializeField] private float _timeToDestroy;

    private PlayerStatsHandler _handler;
    private bool _hasScored;

    private LevelConfig _config;

    private void Start() {
        //_handler = GameObject.FindGameObjectWithTag("Stats").GetComponent<PlayerStatsHandler>();
        _handler = PlayerStatsHandler.Instance;
        _hasScored = false;

        _config = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelConfig>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (_config.Mode == LevelConfig.LevelMode.Versus) {
                if (collision.name == "Player_1") {
                    if (!_hasScored) {
                        _handler.PlayerData["Player_2"].Score++;
                        _hasScored = true;
                    }
                } else {
                    if (!_hasScored) {
                        _handler.PlayerData["Player_1"].Score++;
                        _hasScored = true;
                    }
                }
            } else if (_config.Mode == LevelConfig.LevelMode.AToB) {
                _handler.PlayerData["Player_1"].ItemsPickedUp = 0;
            }
            StartCoroutine(Coroutines.CallVoidAfterSeconds(Utility.RestartLevel, _timeToEnd));
        }
        else if (collision.transform.tag == "BreakableTile")
        {
            StartCoroutine(Coroutines.CallVoidAfterSeconds(Destroy, collision.gameObject, _timeToDestroy));
        }
    }
    
}
