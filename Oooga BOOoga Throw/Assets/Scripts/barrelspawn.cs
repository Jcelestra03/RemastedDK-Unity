using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrelspawn : MonoBehaviour
{
    public float timer;
    public float timedifference;

    public GameObject spawn;
    public GameObject throwing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= timedifference)
        {
            GameObject barrel = Instantiate(throwing, gameObject.transform);
            barrel.GetComponent<Transform>().position = spawn.transform.position;
            timer = 0;
        }
    }
}
