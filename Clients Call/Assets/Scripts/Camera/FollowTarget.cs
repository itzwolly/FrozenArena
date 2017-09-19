using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {
    [SerializeField] private GameObject _target;

    private Vector3 _offset;

    void Start() {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        _offset = transform.position - _target.transform.position;
    }
    
    void LateUpdate() {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = _target.transform.position + _offset;
    }

    //// Update is called once per frame
    //private void Update () {
    //    //transform.position = new Vector3(_target.transform.position.x, transform.position.y, _target.transform.position.z - _cameraZOffset);
    //    transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, 0.01f);
    //}
}
