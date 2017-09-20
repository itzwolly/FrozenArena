using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArenaPreviewHandler : MonoBehaviour {
    [SerializeField] Image _previewImage;
    [SerializeField] Text _mapName;
    [SerializeField] Text _containing;
    [SerializeField] Text _difficulty;
    [SerializeField] EventSystem _eventSystem;

    private GameObject _selected = null;

    // Update is called once per frame
    private void Update () {
        if (_eventSystem.currentSelectedGameObject != null) {
            if (_selected != _eventSystem.currentSelectedGameObject) {

                _selected = _eventSystem.currentSelectedGameObject;
                if (_selected.GetComponent<MapData>() != null) {
                    MapData data = _selected.GetComponent<MapData>();
                    _previewImage.sprite = data.Image;
                    _mapName.text = data.Name;
                    _containing.text = "Special Tiles: ";
                    for (int i = 0; i < data.Tiles.Length; i++) {
                        _containing.text += data.Tiles[i] + ((i == data.Tiles.Length - 1) ? "." : ", ");
                    }
                    _difficulty.text = "Difficulty: " + data.Difficulty.ToString();
                }
            }
        }
    }
}
