using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour 
{

    public List<GameObject> Players;
    // Use this for initialization

    private List<GameObject> Collided = new List<GameObject>();

    public void CollidedWithPlayer(GameObject who)
    {
        Collided.Add(who);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
