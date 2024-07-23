using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WolfScript : MonoBehaviour
{
    [SerializeField] float _jumpforce;
    
    CircleCollider2D WeaponCollider;
    [SerializeField] GameObject player;
    [SerializeField] float _atkregrange;
    Animator _anim;
    [SerializeField] float _groundcheckdis;
    [SerializeField] float _movespeed;
    [SerializeField] GameObject _groundcheck;
    Rigidbody2D _rigid;
    string _direction;
    // Start is called before the first frame update
    void Start()
    {
        WeaponCollider = GetComponentInChildren<CircleCollider2D>();
        _anim = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
        _anim.SetBool("isPatrol", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(SeePlayer()){
            _anim.SetBool("isPatrol",false);
            Debug.Log("Can see player");
            Attack();
        } else{
            _anim.SetBool("Chase", false);
            if(IsFacingRight()){
            _rigid.velocity = new Vector2(_movespeed, 0f);
        } else {
            _rigid.velocity = new Vector2(-_movespeed, 0f);
        }
        if(IsFacingWall() || IsFacingEndOfRoad()){
            transform.localScale = new Vector3(-Mathf.Sign(_rigid.velocity.x), transform.localScale.y, transform.localScale.z);
        }
        }
    }
    bool IsFacingEndOfRoad(){
        bool val = false;
        float castdist = _groundcheckdis;
        Vector3 endroadcheckpos = _groundcheck.transform.position;
        endroadcheckpos.y -= castdist;
        if(!Physics2D.Linecast(_groundcheck.transform.position, endroadcheckpos, 1 << LayerMask.NameToLayer("Wall and Ground"))){
            val = true;
        }
        return val;
    }
    //Cheking if the wolf is facing a wall
    bool IsFacingWall(){
        bool val = false;
        float castdist = _groundcheckdis;
        if(transform.localScale.x == 1) castdist = _groundcheckdis;
        else if(transform.localScale.x == -1)castdist = -_groundcheckdis;
        Vector3 endgroundcheckpos = _groundcheck.transform.position;
        endgroundcheckpos.x += castdist;
        if(Physics2D.Linecast(_groundcheck.transform.position, endgroundcheckpos, 1 << LayerMask.NameToLayer("Wall and Ground"))){
            val = true;
        }
        return val;
    }
    bool IsFacingRight(){
        return transform.localScale.x > Mathf.Epsilon;
    }
    //Cheking if the wolf is facing the end of a road
    bool SeePlayer(){
        bool val = false;
        Vector2 raypos = new Vector2(transform.position.x, transform.position.y + 1); 
        RaycastHit2D rayleft = Physics2D.Raycast(raypos, Vector2.left, _atkregrange, 1 << LayerMask.NameToLayer("Player"));
        RaycastHit2D rayright = Physics2D.Raycast(raypos, Vector2.right, _atkregrange, 1 << LayerMask.NameToLayer("Player"));
        RaycastHit2D raytopright = Physics2D.Raycast(raypos, new Vector2(1, Mathf.Sin(15 * Mathf.Deg2Rad)), _atkregrange,1 << LayerMask.NameToLayer("Player"));
        RaycastHit2D raytopleft = Physics2D.Raycast(raypos, new Vector2(-1, Mathf.Sin(15 * Mathf.Deg2Rad)),_atkregrange, 1 << LayerMask.NameToLayer("Player"));
        if(rayleft.collider != null){
            if(rayleft.collider.gameObject.CompareTag("Player")){
                val = true;
                _direction = "left";
            }
        }
        if(rayright.collider != null){
            if(rayright.collider.gameObject.CompareTag("Player")){
                val = true;
                _direction = "right";
            }
        }
        if(raytopright.collider != null){
            if(raytopright.collider.gameObject.CompareTag("Player")){
                val = true;
                _direction = "topright";
            }
        }
        if(raytopleft.collider != null){
            if(raytopleft.collider.gameObject.CompareTag("Player")){
                val = true;
                _direction = "topleft";
            }
        }
        // RaycastHit2D detector = Physics2D.CircleCast(transform.position, _atkregrange, Vector2.zero, 0);
        // if(detector.collider != null && detector.collider.gameObject.CompareTag("Player")){ val = true;}
        return val;
    }
    void Attack(){
        //Change the scale to look at enemy
        // if(_direction == "left"){
        //     transform.localScale = new Vector3(-1, 1, 1);
        // } else if(_direction == "right"){
        //     transform.localScale = new Vector3(1,1,1);
        // }
        if(transform.position.x >= player.transform.position.x){
            transform.localScale = new Vector3(-1, 1, 1);
        } else if (transform.position.x < player.transform.position.x){
            transform.localScale = new Vector3(1, 1, 1);
        }
        _anim.SetBool("Chase", true);
        //rigid.velocity = new Vector3(transform.localScale.x * _movespeed * 0.5f, 0f, 0f);
        Chase();
    }
    void Chase(){
        //Animation and Physical Moving to player
        
        Debug.Log(Mathf.Abs(transform.position.y - player.transform.position.y));
        _rigid.velocity = new Vector3(transform.localScale.x * _movespeed * 3f, 0f, 0f);
        // if(!(Mathf.Abs(transform.position.y - player.transform.position.y) <= 2f)){
        //     _rigid.AddForce(new Vector2(transform.localScale.x,3) * _jumpforce, ForceMode2D.Impulse);
        // } else{
            if(Vector3.Distance(transform.position, player.transform.position) < 6f){
            _anim.SetTrigger("Attack");
            WeaponCollider.enabled = true;
            StartCoroutine(ResetAttack());
            } 
        //}
    }
    IEnumerator ResetAttack(){
        yield return new WaitForSeconds(0.5f);
        WeaponCollider.enabled = false;
                _anim.SetBool("Chase", false);
        _anim.SetBool("isPatrol", true);
        _anim.SetTrigger("BackToPatrol");
    }
    // void OnCollisionEnter2D(Collision2D other){
    //     player.GetComponent<PlayerScript>().TakeDamage(this.gameObject);
    //     _anim.SetBool("Chase", false);
    //     _anim.SetBool("isPatrol", true);
    //     _anim.SetTrigger("BackToPatrol");
    // }
    void OnDrawGizmos(){
        Vector2 raypos = new Vector2(transform.position.x, transform.position.y + 1); 
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(raypos, Vector2.left*_atkregrange);
        Gizmos.DrawRay(raypos, Vector2.right*_atkregrange);
        Gizmos.DrawRay( raypos,new Vector2(1, Mathf.Sin(15 * Mathf.Deg2Rad))* _atkregrange);
        Gizmos.DrawRay( raypos,new Vector2(-1, Mathf.Sin(15 * Mathf.Deg2Rad))* _atkregrange);
    }
}
