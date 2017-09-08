using UnityEngine;

public class LevelConfig : MonoBehaviour {
    [SerializeField] private LevelMode _mode;
    [SerializeField] private LevelDifficulty _difficulty;

    [Space(10)]
    [SerializeField] private SurvivalProperties _survivalProperties;

    public LevelDifficulty Difficulty {
        get { return _difficulty; }
        set { _difficulty = value; }
    }
    public LevelMode Mode {
        get { return _mode; }
        set { _mode = value; }
    }

    /* Survival */
    public int s_TileDropDuration {
        get {
            switch (_difficulty) {
                case LevelDifficulty.Slow:
                    return _survivalProperties.Slow;
                case LevelDifficulty.Normal:
                    return _survivalProperties.Normal;
                case LevelDifficulty.Fast:
                    return _survivalProperties.Fast;
                default:
                    return -1;
            }
        }
    }


    public enum LevelDifficulty {
        Slow,
        Normal,
        Fast
    }
    public enum LevelMode {
        Versus,
        Survival,
        Race,
        DropAWood
    }
}