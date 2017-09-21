using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour {
    [SerializeField] private Sprite _previewImage;
    [SerializeField] private string _name;
    [SerializeField] private string[] _tiles;
    [SerializeField] private LevelConfig.LevelDifficulty _difficulty;
    [SerializeField] private string _sceneName;

    public Sprite PreviewImage {
        get { return _previewImage; }
        set { _previewImage = value; }
    }
    public string Name {
        get { return _name; }
        set { _name = value; }
    }
    public string[] Tiles {
        get { return _tiles; }
        set { _tiles = Tiles; }
    }
    public LevelConfig.LevelDifficulty Difficulty {
        get { return _difficulty; }
        set { _difficulty = value; }
    }
    public string SceneName {
        get { return _sceneName; }
        set { _sceneName = value; }
    }
}
