using System.Linq;
using System.Reflection;
using UnityEngine;

public class LevelConfig : MonoBehaviour {
    [SerializeField] private LevelMode _mode;
    [SerializeField] private LevelDifficulty _difficulty;

    [Space(10)]
    [SerializeField] private VersusProperties _versusProperties;
    [SerializeField] private SurvivalProperties _survivalProperties;
    [SerializeField] private AToBProperties _aToBProperties;
    [SerializeField] private WoodToHoleProperties _woodToHoleProperties;

    public enum LevelDifficulty {
        Easy,
        Normal,
        Hard
    }
    public enum LevelMode {
        Versus,
        Survival,
        AToB,
        WoodToHole
    }

    private void OnValidate() {
        switch (_mode) {
            case LevelMode.Versus:
                GetComponentInChildren<RaisingTile>().enabled = _versusProperties.EnableRaisingTiles;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowTarget>().enabled = false;
                break;
            case LevelMode.Survival:
                GetComponentInChildren<RaisingTile>().enabled = false;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowTarget>().enabled = false;
                break;
            case LevelMode.AToB:
                GetComponentInChildren<RaisingTile>().enabled = false;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowTarget>().enabled = true;
                break;
            case LevelMode.WoodToHole:
            default:
                break;
        }
    }

    private Properties GetGameModeProperties() {
        switch (_mode) {
            case LevelMode.Survival:
                return _survivalProperties;
            case LevelMode.AToB:
                return _aToBProperties;
            case LevelMode.WoodToHole:
                return _woodToHoleProperties;
            case LevelMode.Versus: // returns null for now..
            default:
                return null;
        }
    }

    public int GetDifficultyValue() {
        return GetGameModeProperties().GetValue(_difficulty);
    }
}