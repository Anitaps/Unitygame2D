using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {
//we are going to find from the collider code, other and then the gameobject that we collider into and 
//we are going to tell unity to destroy that gameobject for us

	void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}
