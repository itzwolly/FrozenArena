using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DLLLibrary;

public class RaisingTile : MonoBehaviour
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
    void Start ()
    {
        _timeToNextFall = _time;
        StartCoroutine(Coroutines.CallVoidAfterSeconds(Up,_delayTime));
		
	}

    private void Up()
    {
        Debug.Log("Up");
        if (Info.MovableCubes.Count == 0)
        {
            Debug.Log("game is done");
            return;
        }
        GameObject obj = Utility.RandomSelectFromList(Info.MovableCubes);
        //int count = 0;
        //while (obj.GetComponent<State>().Up || obj.GetComponent<State>().Down)
        //{
        //    _tiles.Remove(obj);
        //    if (_tiles.Count == 0)
        //        return;
        //    obj = Utility.RandomSelectFromList(_tiles);
        //    count++;
        //}
        Info.MovableCubes.Remove(obj);
        obj.GetComponent<State>().Up = true;
        float height = 1;
        if (_waitAfterDown >= 0)
            StartCoroutine(Coroutines.MoveTransformByVector(obj.transform, WhenUp,obj, new Vector3(0, +height, 0), _speedOfFall));

        if (Info.MovableCubes.Count > 0) StartCoroutine(Coroutines.CallVoidAfterSeconds(Up, _timeToNextFall));
    }

    private void WhenUp(GameObject obj)
    {
        StartCoroutine(Coroutines.CallVoidAfterSeconds(Refall, obj, _waitAfterDown));
        //StartCoroutine(Coroutines.CallVoidAfterSeconds(Destroy, obj, _waitAfterDown));
    }

    private void Refall(GameObject obj)
    {
        StartCoroutine(Coroutines.MoveTransformByVector(obj.transform,ReAddToList,obj, new Vector3(0,-1,0),_speedOfFall));
    }

    private void ReAddToList(GameObject obj)
    {
        Info.MovableCubes.Add(obj);
        obj.GetComponent<State>().Up = false;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
