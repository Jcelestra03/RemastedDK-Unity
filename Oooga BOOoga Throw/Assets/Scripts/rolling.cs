using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rolling : MonoBehaviour
{

    private Rigidbody2D myRB;
    private Vector2 velocity;

    public bool rolR;
    public bool rolL;
    public bool isrolling;
    public bool fall;

    public int randnum1;
    public int randnum2;


    public float timer;
    private float timedifference;

    public GameObject spawnpoint;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        timedifference = 1f;
        timer = 0f;
        randnum1 = (Random.Range(1, 3));
        randnum2 = (Random.Range(1, 3));
    }






    // Update is called once per frame
    void Update()
    {
        velocity = myRB.velocity;

        if(fall == true)
        {
            this.GetComponent<CircleCollider2D>().isTrigger = true;
            timer += Time.deltaTime;
            if (timer >= timedifference)
            {
                timer = 0;
                fall = false;
            }
        }

        else if(fall == false)
        {
            this.GetComponent<CircleCollider2D>().isTrigger = false;
        }


        myRB.velocity = velocity;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("despawn"))
        {
            Destroy(gameObject);

        }


    }






    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("trigger"))
        {
            rolR = false;
            //velocity.x = -15;
            isrolling = true;
            rolL = true;
            StartCoroutine("rollL");
            myRB.velocity = velocity;
        }

        if (collision.gameObject.name.Contains("trigger2"))
        {
            rolL = false;
            //velocity.x = 15;
            isrolling = true;
            rolR = true;
            StartCoroutine("rollR");
            myRB.velocity = velocity;
        }
        if (collision.gameObject.name.Contains("trapdoor"))
        {
            
            if (GameObject.Find("trapdoor").GetComponent<FallingController>().ranum1 == randnum1 || (GameObject.Find("trapdoor").GetComponent<FallingController>().ranum1 == randnum2))
            {
                fall = true;
                StartCoroutine("falling");
            }
        }

        if(collision.gameObject.name.Contains("trapd2"))
        {

            if (GameObject.Find("trapd2").GetComponent<FallingController>().ranum1 == randnum1 || (GameObject.Find("trapd2").GetComponent<FallingController>().ranum1 == randnum2))
            {
                fall = true;
                StartCoroutine("falling");
            }
        }
        if(collision.gameObject.name.Contains("shield"))
        {
            Destroy(gameObject);
        }

    }











    private IEnumerator rollL()
    {
        while(rolL == true)
        {
            velocity.x = -15;
            myRB.velocity = velocity;
            yield return null;
        }
    }
    private IEnumerator rollR()
    {
        while(rolR == true)
        {
            velocity.x = 15;
            myRB.velocity = velocity;
            yield return null;
        }
    }
    private IEnumerator falling()
    {
        while (fall == true)
        {
            velocity.y = -5;
            myRB.velocity = velocity;
            yield return null;
        }
    }
}
