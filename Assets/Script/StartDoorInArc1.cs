using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDoorInArc1 : MonoBehaviour
{
    AudioSource au;
    // Start is called before the first frame update
    void Start()
    {
        au = GetComponent<AudioSource>();
        Invoke("Play", 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Play(){
        au.Play();
    }
}
