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



    //ladder shit
    public bool climbing;
    public int climbspeed;
    public float distance = 1f;
    public LayerMask WhatIsLadder;
 





    //basic movement
    private Rigidbody2D myRB;
    private Vector2 velocity;
    public int movementspeed = 5;
    public float jumpheight = 5f;
    public float jumptimer;
    private Vector2 groundDetection;
    public float groundDetectDistance = .15f;
    private Quaternion zero;






    //
    public GameObject spawnpoint;
    //

    // Start is called before the first frame update
    void Start()
    {
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

        if (Input.GetKeyDown(KeyCode.Space) && Physics2D.Raycast(groundDetection, Vector2.down, groundDetectDistance))
        {
            velocity.y = jumpheight;
        }


        if(speedpower == true)
        {
            StartCoroutine("speed");
            movementspeed = 7;
        }
        else if (shieldpower == true)
        {
            StartCoroutine("shield");
            this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        else if (jumppower == true)
        {
            StartCoroutine("jump");
            jumpheight = 8;
        }








        if (pON == false)
        {
            movementspeed = 5;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            jumpheight = 5;
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

    private IEnumerator speed()
    {
        while (speedpower == true)
        {

            yield return null;
        }
    }

    private IEnumerator shield()
    {
        while (shieldpower == true)
        {

            yield return null;
        }
    }

    private IEnumerator jump()
    {
        while (jumppower == true)
        {

            yield return null;
        }
    }


}
