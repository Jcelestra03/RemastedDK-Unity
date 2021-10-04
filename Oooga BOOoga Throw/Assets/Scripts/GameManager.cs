using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
