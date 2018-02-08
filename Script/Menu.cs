using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    public Text Bestscore;

	// Use this for initialization
	void Start () {
        //recording seconds and store it as a floating value 
        Bestscore.text = PlayerPrefs.GetFloat("Bestscore", 0).ToString("0,0");
		
	}
	
	// Update is called once per frame
	void Update () {
        //by pressing Esc key on title scene you can exit the game
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
    //when we call this method we use the load level on the application to load our game scene
     public void startgame()
    {
        Application.LoadLevel("Gamescene");
    }
}
