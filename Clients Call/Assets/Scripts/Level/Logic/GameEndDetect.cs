using UnityEngine;
using DLLLibrary;
using System.Linq;

public class GameEndDetect : MonoBehaviour {
    [SerializeField] private float _timeToEnd;
    [SerializeField] private float _timeToDestroy;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip PlayerDeath;

    private PlayerStatsHandler _handler;
    private bool _hasScored;

    

    private LevelConfig _levelConfig;

    private void Start() {
        //_handler = GameObject.FindGameObjectWithTag("Stats").GetComponent<PlayerStatsHandler>();
        _handler = PlayerStatsHandler.Instance;
        _hasScored = false;
        _handler.PlayerData["Player_1"].ItemsPickedUp = 0;
        _levelConfig = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelConfig>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (_levelConfig.Mode == LevelConfig.LevelMode.Versus)
            {
                if (collision.name == "Player_1")
                {
                    if (!_hasScored)
                    {
                        _handler.PlayerData["Player_2"].Score++;
                        _hasScored = true;
                    }
                }
                else
                {
                    if (!_hasScored)
                    {
                        _handler.PlayerData["Player_1"].Score++;
                        _hasScored = true;
                    }
                }
            }
            else if (_levelConfig.Mode == LevelConfig.LevelMode.AToB)
            {
                _handler.PlayerData["Player_1"].ItemsPickedUp = 0;
            }

            GetComponent<AudioSource>().PlayOneShot(PlayerDeath);
            StartCoroutine(Coroutines.CallVoidAfterSeconds(Utility.RestartLevel, _timeToEnd));
        }
        else if (collision.transform.tag == "BreakableTile")
        {
            StartCoroutine(Coroutines.CallVoidAfterSeconds(Destroy, collision.gameObject, _timeToDestroy));
        }
        
    }
    
}
