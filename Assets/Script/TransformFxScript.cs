using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformFxScript : MonoBehaviour
{
    Animator vpanim;
    PlayerInfoHandler playerinfohandler;
    bool istransformed;
    // Start is called before the first frame update
    void Start()
    {
        vpanim = GetComponent<Animator>();
        playerinfohandler = GetComponentInParent<PlayerScript>().playerinfohandler;
        istransformed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!istransformed){
            if(Input.GetKeyDown(KeyCode.Q) && playerinfohandler.getMana() > 0){
            vpanim.SetBool("BienHinh", true);
            istransformed = true;
            }
            else vpanim.SetBool("BienHinh",false);
        }   
        else{
            if(playerinfohandler.getMana() <=0 || Input.GetKeyDown(KeyCode.Q)){
                istransformed = false;
                vpanim.SetBool("BienHinh", true);
            } else vpanim.SetBool("BienHinh",false);
        }
    }
}
