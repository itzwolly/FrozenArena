using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditPrevTileScript : MonoBehaviour {

    [SerializeField] List<GameObject> TileEditors;
    [SerializeField] Button Accept;

    private GameObject _activeObject;
    //private List<>
    private bool _editing;
    void Start () {
        _editing = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(_editing)
        {
            if(Input.GetKeyUp(KeyCode.Space))
            {
                _activeObject.SetActive(false);
                Accept.onClick.Invoke();
                _editing = false;
            }
        }
	}

    public void EditObject(GameObject obj)
    {
        _editing = true;
        obj.GetComponent<State>().Changed = true;
        //compare components and set active if true
        if (obj.GetComponent<BombTile>() != null)
        {
            _activeObject = TileEditors[1];
            TileEditors[1].SetActive(true);
            _activeObject.GetComponent<BombEdit>().EditTile(obj);
        }
        else if (obj.tag=="BreakableTile")
        {
            _activeObject = TileEditors[2];
            TileEditors[2].SetActive(true);
            _activeObject.GetComponent<BreakableEdit>().EditTile(obj);
        }
        else if(obj.GetComponent<MultiDirectionalBoost>()!=null)
        {
            _activeObject = TileEditors[3];
            TileEditors[3].SetActive(true);
            _activeObject.GetComponent<MultiBoostEdit>().EditTile(obj);
        }
        else if(obj.GetComponent<OneWayBoost>()!=null)
        {
            _activeObject = TileEditors[4];
            TileEditors[4].SetActive(true);
            _activeObject.GetComponent<UniBoostEdit>().EditTile(obj);
        }
        else if(obj.GetComponent<SlowDown>()!=null)
        {
            _activeObject = TileEditors[5];
            TileEditors[5].SetActive(true);
            _activeObject.GetComponent<SlowDownEdit>().EditTile(obj);
        }
        else
        {
            _activeObject = TileEditors[0];
            TileEditors[0].SetActive(true);
        }
        //add children to lists (i dont know how)
        //use a modified version of the next/previouse selection to cycle through the 
        //childern and change values
    }
}
