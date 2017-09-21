using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GamePreviewTextHandler : MonoBehaviour {
    [SerializeField] private Image _mapImageContainer;
    [SerializeField] private Text _mapNameContainer;
    [SerializeField] private Text _containgTilesContainer;
    [SerializeField] private Image _difficultyContainer;

    [SerializeField] private Sprite[] _difficultySprites;

    private MapData _firstMap;
    private Sprite _mapImage;
    private string _mapName;
    private string[] _containgTiles;
    private LevelConfig.LevelDifficulty _difficulty;

	// Use this for initialization
	private void Start () {
        _firstMap = MenuDataHandler.Instance.QueuedMaps.First();

        _mapImage = _firstMap.PreviewImage;
        _mapName = _firstMap.Name;
        _containgTiles = _firstMap.Tiles;
        _difficulty = _firstMap.Difficulty;

        _mapImageContainer.sprite = _mapImage;
        _mapNameContainer.text = _mapName;
        _containgTilesContainer.text = "";
        for (int i = 0; i < _containgTiles.Length; i++) {
            _containgTilesContainer.text += _containgTiles[i] + ((i == _containgTiles.Length - 1) ? "." : ", ");
        }

        switch (_difficulty) {
            case LevelConfig.LevelDifficulty.Easy:
                _difficultyContainer.sprite = _difficultySprites[0];
                break;
            case LevelConfig.LevelDifficulty.Normal:
                _difficultyContainer.sprite = _difficultySprites[1];
                break;
            case LevelConfig.LevelDifficulty.Hard:
                _difficultyContainer.sprite = _difficultySprites[2];
                break;
            default:
                break;
        }

        _mapImageContainer.gameObject.SetActive(true);
    }
}
