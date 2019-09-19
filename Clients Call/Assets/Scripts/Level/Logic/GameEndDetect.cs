using UnityEngine;
using DLLLibrary;
using System.Linq;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameEndDetect : MonoBehaviour {
    [SerializeField] private float _timeToEnd;
    [SerializeField] private float _timeToDestroy;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip PlayerDeath;

    private PlayerStatsHandler _handler;
    private bool _hasScored;
    private LevelConfig _levelConfig;

    private void Start() {
        _handler = PlayerStatsHandler.Instance;
        _hasScored = false;
        _levelConfig = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelConfig>();

        PlayerStatsHandler.Instance.PlayerData["Player_1"].ItemsPickedUp = 0;

        print("Player_1 score: " + PlayerStatsHandler.Instance.PlayerData["Player_1"].Score);

        if (MenuDataHandler.Instance.PlayersReady == 2) {
            if (PlayerStatsHandler.Instance.PlayerData["Player_1"].Score == 3) {
                PlayerStatsHandler.Instance.PlayerData["Player_1"].HasWon = true;
                MenuDataHandler.Instance.WinnerIndex = 1;
            } else if (PlayerStatsHandler.Instance.PlayerData["Player_2"].Score == 3) {
                PlayerStatsHandler.Instance.PlayerData["Player_2"].HasWon = true;
                MenuDataHandler.Instance.WinnerIndex = 2;
            }
        }
        
    }

    private void Update() {
        if (MenuDataHandler.Instance.PlayersReady == 2) {
            if (PlayerStatsHandler.Instance.PlayerData["Player_1"].HasWon || PlayerStatsHandler.Instance.PlayerData["Player_2"].HasWon) {
                StartCoroutine(ScaleTime(1.0f, 0.0f, 3.0f));

                // resolution screen
                if (MenuDataHandler.Instance.QueuedMaps.Count == 0) {
                    // load the continue to main map
                    SceneManager.LoadScene("Resolution_nonextmap");
                } else {
                    // load the next map
                    SceneManager.LoadScene("Resolution_nextmap");
                }
            }
        }
    }

    IEnumerator ScaleTime(float pStart, float pEnd, float pTime) {
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0.0f;

        while (timer < pTime) {
            Time.timeScale = Mathf.Lerp(pStart, pEnd, timer / pTime);
            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;
            yield return null;
        }

        Time.timeScale = pEnd;
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
