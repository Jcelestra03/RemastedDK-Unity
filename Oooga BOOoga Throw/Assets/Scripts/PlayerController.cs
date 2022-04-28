using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{


    //powerups
    public List<bool> powerUps = new List<bool>();
    // [0] = jump; [1] = speed; [2] = shield
    public bool pON;
    
    public float powertimer;
    public float powertimer2;
    public float powertimer3;





    //timers
    public float timer1;
    public float timer2;
    public float timer3;

    public float timer;
    public float timedifference;



    //ladder shit
    public bool climbing;
    public int climbspeed;
    public float distance = 1f;
    public LayerMask WhatIsLadder;


    public int deaths;



    //basic movement
    private Rigidbody2D myRB;
    private Vector2 velocity;
    public int movementspeed;
    public float jumpheight;
    public float jumptimer;
    private Vector2 groundDetection;
    public float groundDetectDistance = .15f;



    //movement restrictions
    public bool canjump;



    //audio
    private AudioSource speaker;

    public AudioClip jsound;

    public AudioClip mokey;



    //
    public GameObject spawnpoint;
    //

    // Start is called before the first frame update
    void Start()
    {
        speaker = GetComponent<AudioSource>();
        myRB = GetComponent<Rigidbody2D>();
        pON = false;

    }

    // Update is called once per frame
    void Update()
    {
        velocity = myRB.velocity;
        velocity.x = Input.GetAxisRaw("Horizontal") * movementspeed;

        groundDetection = new Vector2(transform.position.x, transform.position.y - 1.1f);



        if (canjump || powerUps[0] == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) && Physics2D.Raycast(groundDetection, Vector2.down, groundDetectDistance))
            {
                speaker.clip = jsound;
                speaker.Play();
                velocity.y = jumpheight;
                canjump = false;
            }
            
        }
        else if (!canjump)
        {
            timer += Time.deltaTime;
            if(timer >= timedifference)
            {
                canjump = true;
                timer = 0;
            }
        }
    
        if(powerUps[1] == true)
        {
            StartCoroutine("speed");
            movementspeed = 10;
        }
        else if (powerUps[2] == true)
        {
            StartCoroutine("shield");
            GameObject.Find("shield").GetComponent<BoxCollider2D>().enabled = true;
        }
        else if (powerUps[0] == true)
        {
            StartCoroutine("jump");
            jumpheight = 15;
        }

        if(powerUps[1] == false)
        {
            movementspeed = 7;
        }
        if (powerUps[2] == false)
        {
            GameObject.Find("shield").GetComponent<BoxCollider2D>().enabled = false;
        }
        if (powerUps[0] == false)
        {
            jumpheight = 11;
        }


        if(deaths >= 10)
        {
            GameObject.Find("GM").GetComponent<GameManager>().loss();
        }





        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, WhatIsLadder);
        
            if(hitInfo.collider !=null)
            {
                if(Input.GetKeyDown(KeyCode.W))
                {
                velocity.y = 3;
                myRB.velocity = velocity;
                climbing = true;
                }

                if(Input.GetKeyDown(KeyCode.S))
                {
                velocity.y = -3;
                myRB.velocity = velocity;
                }
            }
            else
            {
                climbing = false;
            }
            if(climbing == true)
            {
                GetComponent<CircleCollider2D>().isTrigger = true;
                myRB.gravityScale = 0;
            
            }
            else
            {
                GetComponent<CircleCollider2D>().isTrigger = false;
                myRB.gravityScale = 2;
            }










        myRB.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Contains("despawn"))
        {
            deaths = deaths + 1;
            transform.position = spawnpoint.transform.position;
        }
        if(collision.gameObject.name.Contains("barrel"))
        {
            deaths = deaths + 1;
            transform.position = spawnpoint.transform.position;
        }
        if (collision.gameObject.name.Contains("Arrow"))
        {
            deaths = deaths + 1;
            transform.position = spawnpoint.transform.position;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        //myRB.constraints = RigidbodyConstraints2D.FreezePositionY;
        climbing = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        climbing = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Contains("wintrigger"))
        {
            speaker.clip = mokey;
            speaker.Play();
            GameObject.Find("GM").GetComponent<GameManager>().loadlevel2();
        }
    }

    public IEnumerator speed()
    {
        
        while (powerUps[1] == true)
        {
            
            timer1 += Time.deltaTime;
            if(timer1 >= powertimer)
            {
                GameObject.Find("SoundsObject").GetComponent<sounds>().Pu1 = false;
                timer1 = 0;
                powerUps[1] = false;
            }
            yield return null;
        }
    }

    public IEnumerator shield()
    {
        
        while (powerUps[2] == true)
        {
            timer2 += Time.deltaTime;
            if(timer2 >= powertimer2)
            {
                GameObject.Find("SoundsObject").GetComponent<sounds>().Pu2 = false;
                timer2 = 0;
                powerUps[2] = false;
            }
            yield return null;
        }
    }

    public IEnumerator jump()
    {
        
        while (powerUps[0] == true)
        {
            timer3 += Time.deltaTime;
            if(timer3 >= powertimer3)
            {
                GameObject.Find("SoundsObject").GetComponent<sounds>().Pu3 = false;
                timer3 = 0;
                powerUps[0] = false;
            }
            yield return null;
        }
    }


}
