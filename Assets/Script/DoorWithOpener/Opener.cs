using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opener : MonoBehaviour
{
    [SerializeField] GameObject door;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer == 7){
            anim.SetBool("isPressed", true);
            //Call to the door
            door.GetComponent<Collider2D>().enabled = false;
            door.GetComponent<Animator>().SetTrigger("open"); 
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.layer == 7){
            anim.SetBool("isPressed", false);
        }
    }
}
