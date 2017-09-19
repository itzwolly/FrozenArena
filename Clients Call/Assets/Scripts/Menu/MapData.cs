using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour {
    [SerializeField] private Sprite _image;
    [SerializeField] private string _name;
    [SerializeField] private string[] _tiles;
    [SerializeField] private LevelConfig.LevelDifficulty _difficulty;

    public Sprite Image {
        get { return _image; }
    }
    public string Name {
        get { return _name; }
    }
    public string[] Tiles {
        get { return _tiles; }
    }
    public LevelConfig.LevelDifficulty Difficulty {
        get { return _difficulty; }
    }
}
