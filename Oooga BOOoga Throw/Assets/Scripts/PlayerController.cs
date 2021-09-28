using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    public bool climbing;
    public int climpos;
    public float distance;
    public LayerMask WhatIsLadder;
    //public float inputVertical;

    private Rigidbody2D myRB;
    private Vector2 velocity;
    public int movementspeed = 5;
    public float jumpheight = 5f;
    private Vector2 groundDetection;
    public float groundDetectDistance = .15f;
    private Quaternion zero;

    

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        zero = new Quaternion();

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

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, WhatIsLadder);
        
            if(hitInfo.collider !=null)
            {
                if(Input.GetKeyDown(KeyCode.W))
                {
                velocity.y = movementspeed;
                myRB.velocity = velocity;
                climbing = true;
                }

                if(Input.GetKeyDown(KeyCode.S))
                {
                velocity.y = -movementspeed;
                myRB.velocity = velocity;
                }
            }
            else
            {
            climbing = false;
            }
            if(climbing == true)
            {
                
                myRB.gravityScale = 0;
            
            }
            else
            {
            myRB.gravityScale = 1;
            }

        myRB.velocity = velocity;
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

}
