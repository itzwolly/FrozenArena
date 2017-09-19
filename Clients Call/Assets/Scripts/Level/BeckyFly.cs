using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeckyFly : MonoBehaviour {
    private Vector3 direction;
    [SerializeField] private float _speed;
    [SerializeField] private float _time;
    System.Random rnd = new System.Random();
    private Vector3 _spawnPosition;
    // Use this for initialization
    void Start () {
        //direction = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1,1));
        transform.LookAt(new Vector3(0, -40, 0));
        _spawnPosition = GetRandomPos(-21, -40, -15, 21, -40, 15);
        transform.position = _spawnPosition;
        //Invoke("ChooseDirection", _time);
    }

    private Vector3 GetRandomPos(int minx, int miny, int minz, int maxx,int maxy,int maxz)
    {
        Vector3 vec = new Vector3();

        vec.x = rnd.Next(minx, maxx);
        vec.x = rnd.Next(miny, maxy);
        vec.x = rnd.Next(minz, maxz);

        return vec;
    }

    private void ChooseDirection()
    {
        transform.LookAt(new Vector3(0,0,0));
        //transform.position = GetRandomPos(-21, -10, -15, 21, -3, 15);
        Invoke("ChooseDirection", _time);
    }
	
	// Update is called once per frame
	void Update () {
        //transform.position += direction.normalized * _speed * Time.deltaTime;
        //transform.rotation = Quaternion.Euler(direction.normalized);
        //transform.LookAt(new Vector3(0, 0, 0));
        gameObject.GetComponent<Rigidbody>().AddForce(-_spawnPosition.normalized);
    }
}
