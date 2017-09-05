﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour {
    [SerializeField]
    private int _time;
    [SerializeField][Range(0,1)]
    private float _step;
    private List<GameObject> _ground = new List<GameObject>();
    System.Random rnd = new System.Random();

    private int _counter;
    private float _timeToDrop;
	// Use this for initialization
	void Start () {
        _counter = 0;
        _timeToDrop = _time;
        foreach(Transform t in transform) {
            foreach (Transform tile in t.transform) {
                if (tile.tag == "Ground") {
                    _ground.Add(t.gameObject);
                }
            }
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //_counter++;
        //if(_counter>=_timeToDrop)
        //{
        //    Debug.Log("Dropped Block");
        //    //DropRandomBlock();
        //    MoveRandomBlock();
        //    _timeToDrop *= _step;
        //    _counter = 0;
        //}
	}

    private void DropRandomBlock()
    {
        int nr = rnd.Next(0,_ground.Count);
        GameObject obj = _ground[nr];
        _ground.RemoveAt(nr);
        Destroy(obj);
    }


    private void MoveRandomBlock()
    {
        int which = rnd.Next(0, 2);
        int nr = rnd.Next(0, _ground.Count);

        GameObject obj = _ground[nr];
        //_ground.RemoveAt(nr);//uncomment if you dont want blocks to keep moving
        if(which==0)
        {
            //obj.transform.Translate(0,-1,0);
            StartCoroutine(MoveTransformByVector(obj.transform, new Vector3(0, -1, 0), 1));
            //Destroy(obj);
        }
        else
        {
            StartCoroutine(MoveTransformByVector(obj.transform, new Vector3(0, 1, 0), 1));
        }
    }


    public IEnumerator MoveTransformByVector(Transform pTransform, Vector3 distance, float time)
    {
        Vector3 currentPos = pTransform.position;

        Vector3 newPos = currentPos+distance;
        float t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime / time;
            pTransform.position = Vector3.Lerp(currentPos, newPos, t);
            yield return null;
        }
    }
}
