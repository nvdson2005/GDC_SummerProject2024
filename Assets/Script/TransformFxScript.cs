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
        if(Input.GetKeyDown(KeyCode.Q) && !istransformed){
            vpanim.SetBool("BienHinh", true);
//            Debug.Log("Q Pressed in TransformFx");
            istransformed = true;
        }
        else vpanim.SetBool("BienHinh",false);
        if(istransformed){
            if(playerinfohandler.getMana() <=0 || Input.GetKeyDown(KeyCode.E)){
                istransformed = false;
                vpanim.SetBool("BienHinh", true);
            }
        }
    }
}
