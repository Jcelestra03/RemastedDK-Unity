using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sounds : MonoBehaviour
{
    public bool Pu1;
    public bool Pu2;
    public bool Pu3;

    private AudioSource speaker;
    public AudioClip psound1;
    public AudioClip psound2;
    public AudioClip psound3;

    // Start is called before the first frame update
    void Start()
    {

        speaker = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Pu1)
        {
            speaker.clip = psound1;
            speaker.Play();
            Pu1 = false;
        }
        else if(Pu2)
        {
            speaker.clip = psound2;
            speaker.Play();
            Pu2 = false;
        }
        else if(Pu3)
        {
            speaker.clip = psound3;
            speaker.Play();
            Pu3 = false;
        }
    }
}
