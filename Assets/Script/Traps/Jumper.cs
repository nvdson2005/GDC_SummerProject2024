using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    GameObject playerr;
    [SerializeField] float force;
    bool isplayeroverjumper;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isplayeroverjumper = false;
        playerr = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PushUp(){
        
        if(isplayeroverjumper){
playerr.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, force), ForceMode2D.Impulse);
            isplayeroverjumper = false;
        } 
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer == 7){anim.SetTrigger("jump");other.gameObject.GetComponent<PlayerScript>().SetIsGrounded(false);isplayeroverjumper = true;}
    }
}
