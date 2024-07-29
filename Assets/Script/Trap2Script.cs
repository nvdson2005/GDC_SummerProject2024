using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hitplayer = Physics2D.Raycast(transform.position, Vector2.down, dis, 1 << LayerMask.NameToLayer("Player"));
        if(player && hitplayer.collider != null){
            DetectFalling();
        }
    }
    void DetectFalling(){
        if(Mathf.Abs(player.transform.position.x - transform.position.x) < 0.5f){
            rigid.bodyType = RigidbodyType2D.Dynamic;
            coll.enabled = true;
        }
    }
    void OnCollisionEnter2D(Collision2D other){
        if(!FirstTime&&(other.gameObject.tag == "Player" || other.gameObject.tag == "FakeChest")){
            other.gameObject.GetComponent<PlayerScript>().TakeDamage(this.gameObject);
            //coll.enabled = false;
            FirstTime = true;
            rigid.bodyType = RigidbodyType2D.Static;
        }
    }
    void OnDrawGizmos(){
        Gizmos.DrawRay(transform.position, Vector2.down * dis);
    }
}
