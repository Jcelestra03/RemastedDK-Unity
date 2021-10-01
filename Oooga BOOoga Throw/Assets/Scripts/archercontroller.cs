using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class archercontroller : MonoBehaviour
{
    private Vector2 velocity;
    public bool isfollowing;
    public GameObject playertarget;

    public float bulletspeed;
    public float bulletLifespan;
    public GameObject bullet;
    public float timer;
    public float bcooldown;
    public bool canshoot;

    // Start is called before the first frame update
    void Start()
    {
        
        isfollowing = false;
        playertarget = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookPos = playertarget.transform.position - transform.position;

        lookPos.Normalize();
        if (isfollowing)
        {
            if (canshoot)
            {
                GameObject b = Instantiate(bullet, gameObject.transform);
                float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
                b.GetComponent<Rigidbody2D>().rotation = angle;
                b.GetComponent<Rigidbody2D>().velocity = new Vector2(lookPos.x * bulletspeed, lookPos.y * bulletspeed);
                
                
                Destroy(b, bulletLifespan);
                canshoot = false;
            }
            
            if(!canshoot)
            {
                timer += Time.deltaTime;
                if (timer >= bcooldown)
                {
                    canshoot = true;
                    timer = 0;
                }
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isfollowing && (collision.gameObject.name == "Player"))
        {
            isfollowing = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(isfollowing && (collision.gameObject.name == "Player"))
        {
            isfollowing = false;
        }
    }
}
