using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactBP : MonoBehaviour {
    //holds next pointing time we are going to spawn cactus
    private float Nextspawn = 0;
    //holds a refrence to the cactBP that we want to spawn
    public Transform cactBP;
    public AnimationCurve spawncrve;
    //setting the curve length to 30 as default
    public float curvelngthinsecond = 30f;
    private float startTime;
    public float jitter = 0.25f;

   
   // float randomE;
  //  Vector2 whereToSpawn;
  //  public float sapwnRate = 2;
   


    // Use this for initialization
    void Start () {
        //To know how much time is passed since cactBP spawner is created
        startTime = Time.time;
		
	}
	
	// Update is called once per frame
	void Update () {
        //if current time of game is bigger than nextspawn time then we do spawning
        if (Time.time > Nextspawn)
        {
           //Quaternion is being used for rotation
            Instantiate(cactBP, transform.position, Quaternion.identity);
            // Nextspawn = Time.time + spawnRate + Random.Range(0, randomDelay);
            //The number of the seconds that have been passed since the cacBP was created and divide it to
            //curve length in seconds
            float crvePosition = (Time.time - startTime) / curvelngthinsecond;
            //The curve play through once and then restarts and will get faster and faster and then 
            //will slow down again
            if (crvePosition > 1f)
            {
                crvePosition = 1f;
                //reset start time
                startTime = Time.time;
            }
            //calculate our next spawn time
            Nextspawn = Time.time + spawncrve.Evaluate(crvePosition) + Random.Range(-jitter,jitter);
        }
    //  if (Time.time > Nextspawn)
     // {
            // nextSpawn = Time.time + spawnRate;
            // randomE = randomE.Range(-8.4f, 8.4f);
       //Netspawn = Time.time + spawnRate + randomE.Range(-jitter,jitter);
          //whereToSpawn = new Vector2(randomE, transform.position.y);
       //  Instantiate(flyenemy, whereToSpawn, Quaternion.identity);
     // }
    }

}
