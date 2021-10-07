using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerController : MonoBehaviour
{
    public bool pw1;
    public bool pw2;
    public bool pw3;
    public int Rpower;
    // Start is called before the first frame update
    void Start()
    {
        Rpower = (Random.Range(1, 4));
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Rpower == 1)
        {
            pw1 = true;
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            pw2 = false;
            pw3 = false;
        }
        else if (Rpower == 2)
        {
            pw2 = true;
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
            pw1 = false;
            pw3 = false;
        }
        else if (Rpower == 3)
        {
            pw3 = true;
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            pw2 = false;
            pw1 = false;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            if(pw1 == true)
            {
                GameObject.Find("SoundsObject").GetComponent<sounds>().Pu1 = true;
                GameObject.Find("Player").GetComponent<PlayerController>().speedpower = true;
            }
            else if (pw2 == true)
            {
                GameObject.Find("SoundsObject").GetComponent<sounds>().Pu2 = true;
                GameObject.Find("Player").GetComponent<PlayerController>().shieldpower = true;
            }
            else if (pw3 == true)
            {
                GameObject.Find("SoundsObject").GetComponent<sounds>().Pu3 = true;
                GameObject.Find("Player").GetComponent<PlayerController>().jumppower = true;
            }
            Destroy(gameObject);
        }
    }

}
