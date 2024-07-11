using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformFxScript : MonoBehaviour
{
    Animator vpanim;
    float time;
    bool istransformed;
    // Start is called before the first frame update
    void Start()
    {
        vpanim = GetComponent<Animator>();
        time = 0;
        istransformed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            vpanim.SetBool("BienHinh", true);
            Debug.Log("Q Pressed in TransformFx");
            istransformed = true;
        }
        else vpanim.SetBool("BienHinh",false);
        if(istransformed){
            time += Time.deltaTime;
            if(time >= 10f){
                istransformed = false;
                time = 0;
                vpanim.SetBool("BienHinh", true);
            }
        }
    }
}
