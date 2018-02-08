using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bunnyjmp : MonoBehaviour
{
    //initiallze variable and store their data
    //adding rigidbody attribute to the player
    private Rigidbody2D myRigid;
    public float Jumpforce = 500f;
    //adding an animator object to handle state transitions,we are going to store reference to the animation
    private Animator myanimation;
    //storing in BunnydeathTime that whenever bunny gets collider we store the time value and later on we can compare
    //to see how much time is passed and if it's passed a certain amount of time then we can reload the level
    private float BunnydeathTime = -1;
    //adding collider to the player
    private Collider2D mycollid;
    public Text scoretxt;
    private float stratTime;
    //number of maximum jumps allowed
    private int Jumplimit = 2;
    //added sound effects for jumping and dying
    public AudioSource jumpi;
    public AudioSource deathi;
   // public GameObject heart, heart1, heart2;
    public int playerlife = 3;
    int playerLayer, enemyLayer;
    bool coroutlineAllowed = true;
    //making the hearts trasparent
    Color color;
    Renderer rend;
    public static int health;
    //private Collision2D collision;

    // Use this for initialization for all game objects and layers
    void Start()
    {
        //health = 3;
        playerLayer = this.gameObject.layer;
        enemyLayer = LayerMask.NameToLayer("enemy");
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        //assigning heart variable to corresponding game object

       // heart = GameObject.Find("heart");
       // heart1 = GameObject.Find("heart1");
       // heart2 = GameObject.Find("heart2");
       // heart.gameObject.SetActive(true);
       // heart1.gameObject.SetActive(true);
       // heart2.gameObject.SetActive(true);
       // rend = GetComponent<Renderer>();
       // color = rend.material.color;

        //look on our current game object and find rigid2D component that is attached to it and assign it to the myrigid2D variable
        myRigid = GetComponent<Rigidbody2D>();
        //animator component that is attached to our game object
        myanimation = GetComponent<Animator>();
        mycollid = GetComponent<Collider2D>();
        stratTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("Title");
        }
        //Here it checks that if the bunny is dead , we don't want to execute any of the code inside second if statement
        if (BunnydeathTime == -1)
        {

            if (Input.GetButtonUp("Jump") && Jumplimit > 0)
                //when we press space button is pressed it get input and when is released the bunny will jump
                if (Input.GetButtonUp("Jump"))
                {
                    //if bunny is falling down we will cancel out any velocity
                    if (myRigid.velocity.y < 0)
                    {
                        myRigid.velocity = Vector2.zero;
                    }
                    //every time we press the jump button we are going to add 500f unity force to up direction
                    myRigid.AddForce(transform.up * Jumpforce);
                    //reducing jumps by one
                    //Jumplimit--;
                    //playing sound effect for jumping
                    jumpi.Play();
                }
            //in every frame update we are going on the animator set its vvelocity parameters to current velocity
            //of the bunny as is which is up or down 
            myanimation.SetFloat("Vvlocity", myRigid.velocity.y);
            //calculate the time that Bunny has been alive
            scoretxt.text = (Time.time - stratTime).ToString("0.0");
        }
        else
        {
            //if current time is greater than bunnydeathTime plus 2( 2 seconds later) we are going to load the level
            if (Time.time > BunnydeathTime + 2)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }


    }
    //Something collides with our bunny
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Getting the layer of the object and recongnize if it is enemyLayer 
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("enemy"))
        {
            //when player touches enemy it decrease the player life by one 
           /** playerlife -= 1;
            switch (playerlife)
            {
                case 2:
                    heart2.gameObject.SetActive(false);
                    if (coroutlineAllowed)
                    {
                        StartCoroutine("Immortal");
                    }
                    break;
                case 1:
                    heart1.gameObject.SetActive(false);
                    if (coroutlineAllowed)
                    {
                        StartCoroutine("Immortal");
                    }
                    break;
                case 0:
                    heart.gameObject.SetActive(false);
                    if (coroutlineAllowed)
                    {
                        StartCoroutine("Immortal");
                    }
                    break;
            }
    **/
            // this method will be available on any gameobject called FindobjectsofType and unity 
            //will find any object which has CactBP script component into it and will disable spawning them as well 
            //as they have moveL component

            foreach (CactBP spawner in FindObjectsOfType<CactBP>())
            {
                spawner.enabled = false;
            }
            // this method will be available on any gameobject called FindobjectsofType and unity 
            //will find any object which has MoveL script component into it so an array of MoveL objects will
            //be returned and will loop through each one

            foreach (MoveL movelefter in FindObjectsOfType<MoveL>())
            {
                //setting enbaled flag to the false
                movelefter.enabled = false;
            }

            
            //setting bnnydeathTime to current game time/time in seconds which is fractional
            BunnydeathTime = Time.time;
            //setting bunny hurt animation to true so it will trigger that animation
            myanimation.SetBool("Bunnydeath", true);
            //stoping any kind of motion that bunny currently has
            myRigid.velocity = Vector2.zero;
            //shooting up bunny to the air to fall down when it's dying
            myRigid.AddForce(transform.up * Jumpforce);
            //making collider disabled
            mycollid.enabled = false;
            //playing death sound effect
            deathi.Play();
            //Bestscore
            float CrrBestscore = PlayerPrefs.GetFloat("Bestscore", 0);
            //current score
            float Crrscore = Time.time - stratTime;
            //if currentscore is bigger than Bestscore the we are goting to save new score as best score
            if (Crrscore > CrrBestscore)
            {
                PlayerPrefs.SetFloat("Bestscore", Crrscore);
            }
        }
        else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            //when we hit the ground we set back the number of jumps to 2
            //Jumplimit = 2;
        }

    }
    //whenever bunny hurts one of the hearts will disappear and bunny continues playing until third heart
   /** IEnumerator Immortal()
    {
        coroutlineAllowed = false;
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);
        color.a = 0.5f;
        rend.material.color = color;
        yield return new WaitForSeconds(3f);
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        color.a = 1f;
        rend.material.color = color;
        coroutlineAllowed = true;
    }**/
}


