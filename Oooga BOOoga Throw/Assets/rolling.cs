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

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = myRB.velocity;

        myRB.velocity = velocity;
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
}
