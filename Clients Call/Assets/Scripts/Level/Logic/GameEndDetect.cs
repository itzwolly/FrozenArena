using UnityEngine;
using DLLLibrary;
using System.Linq;

public class GameEndDetect : MonoBehaviour {
    [SerializeField] private float _timeToEnd;
    [SerializeField] private float _timeToDestroy;

    private PlayerStatsHandler _handler;
    private bool _hasScored;

    private void Start() {
        //_handler = GameObject.FindGameObjectWithTag("Stats").GetComponent<PlayerStatsHandler>();
        _handler = PlayerStatsHandler.Instance;

        _hasScored = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
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
            StartCoroutine(Coroutines.CallVoidAfterSeconds(Utility.RestartLevel, _timeToEnd));
        }
        else if (collision.transform.tag == "BreakableTile")
        {
            StartCoroutine(Coroutines.CallVoidAfterSeconds(Destroy, collision.gameObject, _timeToDestroy));
        }
    }
    
}
