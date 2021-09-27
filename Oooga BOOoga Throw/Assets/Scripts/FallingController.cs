using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingController : MonoBehaviour
{
    public bool isfalling = false;
    public int ranum1;
    public int ranum2;

    // Start is called before the first frame update
    void Start()
    {
        ranum1 = (Random.Range(1, 5));
        ranum2 = (Random.Range(1, 5));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
