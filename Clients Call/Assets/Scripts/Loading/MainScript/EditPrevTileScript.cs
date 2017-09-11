using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditPrevTileScript : MonoBehaviour {

    [SerializeField] List<GameObject> TileEditors;
    [SerializeField] Button Accept;
    

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
                Accept.onClick.Invoke();
                _editing = false;
            }
        }
	}

    public void EditObject(GameObject obj)
    {
        _editing = true;
        //compare components and set active if true
        //add children to lists (i dont know how)
        //use a modified version of the next/previouse selection to cycle through the 
        //childern and change values
    }
}
