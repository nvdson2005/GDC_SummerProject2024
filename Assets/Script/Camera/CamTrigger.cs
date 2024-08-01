using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    public Vector3 NewCamPos, NewPlayerPos;
    GameObject playerr;
    bool FirstTime;
    CamController camcontroller;
    // Start is called before the first frame update
    void Start()
    {
        playerr = GameObject.FindGameObjectWithTag("Player");
        FirstTime = false;
        camcontroller = Camera.main.GetComponent<CamController>();
    }

    // void OnTriggerEnter2D(Collider2D other){
    //     Debug.Log("Is Triggered");
    //     if(other.gameObject.CompareTag("Player")){
    //         camcontroller.MinPos += NewCamPos;
    //         camcontroller.MaxPos += NewCamPos;
    //         other.transform.position = NewPlayerPos;
    //     }
    // }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) &&Vector2.Distance(playerr.transform.position, transform.position) < 1.5f && !FirstTime && playerr.GetComponent<PlayerScript>().KeyCount > 0){
            camcontroller.MinPos += NewCamPos;
            camcontroller.MaxPos += NewCamPos;
            playerr.transform.position = NewPlayerPos;
            FirstTime = true;
            camcontroller.IncreaseCameraSize();
        }
    }
}
