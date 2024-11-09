using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfScript : MonoBehaviour
{
    [SerializeField] float _jumpforce;
    CircleCollider2D WeaponCollider;
    GameObject player;
    [SerializeField] float _atkregrange;
    Animator _anim;
    [SerializeField] float _groundcheckdis;
    [SerializeField] float _movespeed;
    [SerializeField] GameObject _groundcheck;
    Rigidbody2D _rigid;
    bool onlyafterattack;
    // Start is called before the first frame update
    void Start()
    {
        onlyafterattack = false;
        player = GameObject.FindGameObjectWithTag("Player");
        WeaponCollider = GetComponentInChildren<CircleCollider2D>();
        _anim = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
        _anim.SetBool("isPatrol", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(SeePlayer() && !onlyafterattack){
            _anim.SetBool("isPatrol",false);
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
        if(Physics2D.Linecast(_groundcheck.transform.position, endgroundcheckpos, 1 << LayerMask.NameToLayer("Wall and Ground"))|| Physics2D.Linecast(_groundcheck.transform.position, endgroundcheckpos, 1 << LayerMask.NameToLayer("Trap"))){
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
        //Wolf is tricked by a grass
        if(rayleft.collider != null){
            if(rayleft.collider.gameObject.CompareTag("Player") || !rayleft.collider.gameObject.CompareTag("FakeGrass")){
                val = true;
            }
        }
        if(rayright.collider != null){
            if(rayright.collider.gameObject.CompareTag("Player") || !rayright.collider.gameObject.CompareTag("FakeGrass")){
                val = true;
            }
        }
        if(raytopright.collider != null){
            if(raytopright.collider.gameObject.CompareTag("Player") || !raytopright.collider.gameObject.CompareTag("FakeGrass")){
                val = true;
            }
        }
        if(raytopleft.collider != null){
            if(raytopleft.collider.gameObject.CompareTag("Player") || !raytopleft.collider.gameObject.CompareTag("FakeGrass")){
                val = true;
            }
        }
        return val;
    }
    void Attack(){
        if(transform.position.x >= player.transform.position.x){
            transform.localScale = new Vector3(-1, 1, 1);
        } else if (transform.position.x < player.transform.position.x){
            transform.localScale = new Vector3(1, 1, 1);
        }
        _anim.SetBool("Chase", true);
        Chase();
    }
    void Chase(){
        _rigid.velocity = new Vector3(transform.localScale.x * _movespeed * 3f, 0f, 0f);
            if(Vector2.Distance(transform.position, player.transform.position) < 7f){
            _anim.SetTrigger("Attack");
            WeaponCollider.enabled = true;
            StartCoroutine(ResetAttack());
            }
    }
    IEnumerator ResetAttack(){
        onlyafterattack = true;
        yield return new WaitForSeconds(0.5f);
        onlyafterattack = false;
        WeaponCollider.enabled = false;
                _anim.SetBool("Chase", false);
        _anim.SetBool("isPatrol", true);
        _anim.SetTrigger("BackToPatrol");
    }
    void OnDrawGizmos(){
        Vector2 raypos = new Vector2(transform.position.x, transform.position.y + 1); 
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(raypos, Vector2.left*_atkregrange);
        Gizmos.DrawRay(raypos, Vector2.right*_atkregrange);
        Gizmos.DrawRay( raypos,new Vector2(1, Mathf.Sin(15 * Mathf.Deg2Rad))* _atkregrange);
        Gizmos.DrawRay( raypos,new Vector2(-1, Mathf.Sin(15 * Mathf.Deg2Rad))* _atkregrange);
    }
}
