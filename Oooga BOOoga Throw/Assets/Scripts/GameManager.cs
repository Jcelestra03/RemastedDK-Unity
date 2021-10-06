using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    public bool pause;
    public bool imageON;
    public Image img1;
    public Image img2;
    public Image img3;
    public Image img4;

    // Start is called before the first frame update
    void Start()
    {
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (Time.timeScale > 0)
            {
                pause = false;
                Time.timeScale = 0;
                img1.enabled = true;

            }
            else
            {
                Time.timeScale = 1;
                pause = true;
                img1.enabled = false;

            }
        }
   
        if(GameObject.Find("Player").GetComponent<PlayerController>().shieldpower == true)
        {
            img4.enabled = true;
        }
        else if (GameObject.Find("Player").GetComponent<PlayerController>().jumppower == true)
        {
            img3.enabled = true;
        }
        else if (GameObject.Find("Player").GetComponent<PlayerController>().speedpower == true)
        {
            img2.enabled = true;
        }

        if (GameObject.Find("Player").GetComponent<PlayerController>().shieldpower == false)
        {
            img4.enabled = false;
        }
        else if (GameObject.Find("Player").GetComponent<PlayerController>().jumppower == false)
        {
            img3.enabled = false;
        }
        else if (GameObject.Find("Player").GetComponent<PlayerController>().speedpower == false)
        {
            img2.enabled = false;
        }
    }
    public void loadlevel1()
    {
        StartCoroutine(levelLoader("level1", 1));
    }
    public void loadlevel2()
    {
        StartCoroutine(levelLoader("Level2", 3));
    }

    IEnumerator levelLoader(string levelName, int waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(levelName);
    }
}
