using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap2Script : MonoBehaviour
{
    bool FirstTime;
    RaycastHit2D hitplayer;
    GameObject player;
    Rigidbody2D rigid;
    Collider2D coll;
    [SerializeField] float dis;
    // Start is called before the first frame update
    void Start()
    {
        FirstTime = false;
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        hitplayer = Physics2D.Raycast(transform.position, Vector2.down, dis, 1 << LayerMask.NameToLayer("Player"));
        if(player && hitplayer.collider != null){
            DetectFalling();
        }
    }
    void DetectFalling(){
        if(Mathf.Abs(player.transform.position.x - transform.position.x) < 0.5f){
            rigid.bodyType = RigidbodyType2D.Dynamic;
            rigid.mass = 1000000;
            rigid.gravityScale = 4.5f;
            coll.enabled = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        if(!FirstTime && (other.gameObject.layer == 7)){
            other.gameObject.GetComponent<PlayerScript>().TakeDamage(this.gameObject);
            //coll.enabled = false;
            FirstTime = true;
        }
    }
    void OnDrawGizmos(){
        Gizmos.DrawRay(transform.position, Vector2.down * dis);
    }
}
