using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    bool FirstTime;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        FirstTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player && FirstTime){
            player.transform.position = transform.position;
            FirstTime = false;
        }
    }
}
