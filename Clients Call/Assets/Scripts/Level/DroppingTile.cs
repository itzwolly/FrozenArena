using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DLLLibrary;

public class DroppingTile : MonoBehaviour
{
    [SerializeField]
    private StoredInfo Info;
    [SerializeField]
    private float _delayTime;
    [SerializeField]
    private float _time;
    [SerializeField]
    private int _changes;
    [SerializeField]
    private float _speedOfFall;
    [SerializeField]
    private float _waitAfterDown;

    private float _timeToNextFall;

    // Use this for initialization
    void Start()
    {
        _timeToNextFall = _time;

        StartCoroutine(Coroutines.CallVoidAfterSeconds(Down,_delayTime));

    }

    private void Down()
    {
        Debug.Log("Down");
        if (Info.MovableCubes.Count == 0)
        {
            Debug.Log("game is done");
            return;
        }
        GameObject obj = Utility.RandomSelectFromList(Info.MovableCubes);
        //int count = 0;
        //while (obj.GetComponent<State>().Down || obj.GetComponent<State>().Up)
        //{
        //    _tiles.Remove(obj);
        //    if (_tiles.Count == 0)
        //        return;
        //    obj = Utility.RandomSelectFromList(_tiles);
        //    count++;
        //}
        Info.MovableCubes.Remove(obj);
        obj.GetComponent<State>().Down = true;
        float height = 1;
        if (_waitAfterDown >= 0)
            StartCoroutine(Coroutines.MoveTransformByVector(obj.transform, WhenUp,obj, new Vector3(0,-height,0), _speedOfFall));

        if (Info.MovableCubes.Count > 0) StartCoroutine(Coroutines.CallVoidAfterSeconds(Down, _timeToNextFall));
    }

    private void WhenUp(GameObject obj)
    {
        StartCoroutine(Coroutines.CallVoidAfterSeconds(Destroy,obj, _waitAfterDown));
    }
    

	// Update is called once per frame
	void Update () {
		
	}
}
