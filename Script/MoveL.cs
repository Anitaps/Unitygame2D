using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveL : MonoBehaviour {
    public float speed = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //transform is a component of the object(cactus) and we are taking the current position of the cactus
        //and we are going to add to it a value of left vector and multiply that by speed and multiply that with delta time
        //deltaTime is the amount of time that it took to complete the last frame in second
        //we want to move left at 10 units per second
        transform.position += Vector3.left * speed *Time.deltaTime;
		
	}
}
