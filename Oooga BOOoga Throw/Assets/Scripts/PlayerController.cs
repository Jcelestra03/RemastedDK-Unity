using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    public bool climbing;
    public int climpos;



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

        if (climbing == true)
        {
            StartCoroutine("climb");
            myRB.velocity = velocity;
        }

        myRB.velocity = velocity;
    }

 
    private void OnTriggerStay2D(Collider2D collision)
    {
        myRB.constraints = RigidbodyConstraints2D.FreezePositionY;
        climbing = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        climbing = false;
    }

    private IEnumerator climb()
    {
        while(climbing == true)
        {
            
            if (Input.GetKeyDown(KeyCode.W))
            {
                myRB.constraints = RigidbodyConstraints2D.None;
                velocity.y = 4;
                
            }
            myRB.velocity = velocity;
        }

        yield return null;
    }
}
