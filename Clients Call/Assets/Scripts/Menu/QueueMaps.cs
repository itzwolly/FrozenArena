using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class QueueMaps : MonoBehaviour {
    [SerializeField] KeyCode _interactionKey;
    [SerializeField] EventSystem _eventSystem;
    [SerializeField] GameObject _start;

    private List<MapData> _queuedMaps = new List<MapData>();

    public List<MapData> QueuedMaps {
        get { return _queuedMaps; }
    }

    private void Start() {
        MenuDataHandler.Instance.QueuedMaps = new List<MapData>();
    }

    // Update is called once per frame
    private void Update () {
		if (Input.GetKeyUp(_interactionKey)) {
            if (_eventSystem.currentSelectedGameObject != null) {
                GameObject obj = _eventSystem.currentSelectedGameObject;

                if (obj != _start) {
                    if (!_queuedMaps.Any(o => o == obj)) {
                        _queuedMaps.Add(obj.GetComponent<MapData>());
                        obj.transform.GetChild(0).gameObject.SetActive(true);
                        MenuDataHandler.Instance.QueuedMaps = _queuedMaps;
                    } else {
                        _queuedMaps.Remove(obj.GetComponent<MapData>());
                        obj.transform.GetChild(0).gameObject.SetActive(false);
                        MenuDataHandler.Instance.QueuedMaps = _queuedMaps;
                    }
                }
            }
        }
	}
}
