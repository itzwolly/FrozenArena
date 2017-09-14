using System.Linq;
using System.Reflection;
using UnityEngine;

public class LevelConfig : MonoBehaviour {
    [SerializeField] private LevelMode _mode;
    [Space(-8)]
    [Header("When versus mode is selected, the difficulty does not matter.")]
    [Space(2)]
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
                GameObject survCam = GameObject.FindGameObjectWithTag("MainCamera");
                if (survCam.GetComponent<FollowTarget>() != null) {
                    survCam.GetComponent<FollowTarget>().enabled = false;
                }
                break;
            case LevelMode.AToB:
                GetComponentInChildren<RaisingTile>().enabled = false;
                GameObject aToBCam = GameObject.FindGameObjectWithTag("MainCamera");
                if (aToBCam.GetComponent<FollowTarget>() != null) {
                    aToBCam.GetComponent<FollowTarget>().enabled = false;
                }
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

    public float GetDifficultyValue() {
        if (GetGameModeProperties() != null) {
            return GetGameModeProperties().GetValue(_difficulty);
        } else {
            return _versusProperties.DropAndRaiseSpeed;
        }
    }
}