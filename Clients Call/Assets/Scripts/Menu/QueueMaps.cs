using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class QueueMaps : MonoBehaviour {
    [SerializeField] KeyCode _interactionKey;
    [SerializeField] EventSystem _eventSystem;

    private List<GameObject> _queuedMaps = new List<GameObject>();

    public List<GameObject> QueuedMaps {
        get { return _queuedMaps; }
    }

    private void Start() {
        ArenaDataHandler.Instance.QueuedMaps = new List<GameObject>();
    }

    // Update is called once per frame
    private void Update () {
		if (Input.GetKeyUp(_interactionKey)) {
            if (_eventSystem.currentSelectedGameObject != null) {
                GameObject obj = _eventSystem.currentSelectedGameObject;

                if (!_queuedMaps.Any(o => o == obj)) {
                    print("Added");
                    _queuedMaps.Add(obj);
                    obj.transform.GetChild(0).gameObject.SetActive(true);
                    ArenaDataHandler.Instance.QueuedMaps = _queuedMaps;
                } else {
                    _queuedMaps.Remove(obj);
                    obj.transform.GetChild(0).gameObject.SetActive(false);
                    ArenaDataHandler.Instance.QueuedMaps = _queuedMaps;
                }
            }
        }
	}
}
