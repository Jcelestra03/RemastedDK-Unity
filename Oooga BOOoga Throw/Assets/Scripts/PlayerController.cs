using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{


    //powerups
    public bool pON;
    public bool shieldpower;
    public bool speedpower;
    public bool jumppower;
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
 





    //basic movement
    private Rigidbody2D myRB;
    private Vector2 velocity;
    public int movementspeed;
    public float jumpheight;
    public float jumptimer;
    private Vector2 groundDetection;
    public float groundDetectDistance = .15f;
    private Quaternion zero;



    //movement restrictions
    public bool canjump;



    //audio
    private AudioSource speaker;

    public AudioClip mokey;




    //
    public GameObject spawnpoint;
    //

    // Start is called before the first frame update
    void Start()
    {
        speaker = GetComponent<AudioSource>();
        myRB = GetComponent<Rigidbody2D>();
        zero = new Quaternion();
        pON = false;

    }

    // Update is called once per frame
    void Update()
    {
        velocity = myRB.velocity;
        velocity.x = Input.GetAxisRaw("Horizontal") * movementspeed;

        groundDetection = new Vector2(transform.position.x, transform.position.y - 1.1f);



        if (canjump || jumppower == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) && Physics2D.Raycast(groundDetection, Vector2.down, groundDetectDistance))
            {
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
    
        if(speedpower == true)
        {
            StartCoroutine("speed");
            movementspeed = 10;
        }
        else if (shieldpower == true)
        {
            StartCoroutine("shield");
            GameObject.Find("shield").GetComponent<BoxCollider2D>().enabled = true;
        }
        else if (jumppower == true)
        {
            StartCoroutine("jump");
            jumpheight = 15;
        }

        if(speedpower == false)
        {
            movementspeed = 7;
        }
        if (shieldpower == false)
        {
            GameObject.Find("shield").GetComponent<BoxCollider2D>().enabled = false;
        }
        if (jumppower == false)
        {
            jumpheight = 11;
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
                this.GetComponent<CircleCollider2D>().isTrigger = true;
                myRB.gravityScale = 0;
            
            }
            else
            {
                this.GetComponent<CircleCollider2D>().isTrigger = false;
                myRB.gravityScale = 2;
            }










        myRB.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Contains("despawn"))
        {
            transform.position = spawnpoint.transform.position;
        }
        if(collision.gameObject.name.Contains("barrel"))
        {
            transform.position = spawnpoint.transform.position;
        }
        if (collision.gameObject.name.Contains("Arrow"))
        {
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
        while (speedpower == true)
        {
            timer1 += Time.deltaTime;
            if(timer1 >= powertimer)
            {
                timer1 = 0;
                speedpower = false;
            }
            yield return null;
        }
    }

    public IEnumerator shield()
    {
        while (shieldpower == true)
        {
            timer2 += Time.deltaTime;
            if(timer2 >= powertimer2)
            {
                timer2 = 0;
                shieldpower = false;
            }
            yield return null;
        }
    }

    public IEnumerator jump()
    {
        while (jumppower == true)
        {
            timer3 += Time.deltaTime;
            if(timer3 >= powertimer3)
            {
                timer3 = 0;
                jumppower = false;
            }
            yield return null;
        }
    }


}
