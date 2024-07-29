using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class OrcScript : MonoBehaviour
{
    CircleCollider2D WeaponHit;
    [SerializeField] GameObject Atkpoint;
    [SerializeField] float atkrange;
    [SerializeField] LayerMask layerr;
    float _count;
    Transform _basetransform;
    [HideInInspector] GameObject player;
    Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        WeaponHit = GetComponentInChildren<CircleCollider2D>();
        _anim = GetComponent<Animator>();
        _basetransform = GetComponent<Transform>();
        _count = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, player.transform.position) < 2.25f && player.CompareTag("Player")){
            _anim.SetTrigger("Attack");
            DetectWhereToLook();
            Invoke("MakingHit", 0.5f);
        } else LookAround();
    }
    public void MakingHit(){
        // Collider2D hit = Physics2D.OverlapCircle(Atkpoint.transform.position, atkrange, layerr);
        //         //Debug.Log("We hit the player");
        //         hit.gameObject.GetComponent<PlayerScript>().TakeDamage(this.gameObject);
        WeaponHit.enabled = true;
        StartCoroutine(ResetAttack());
        //player.GetComponent<PlayerScript>().TakeDamage(this.gameObject);
    }
    IEnumerator ResetAttack(){
        yield return new WaitForSeconds(0.3f);
        WeaponHit.enabled = false;
    }
    public void DetectWhereToLook(){
        if(transform.position.x - player.transform.position.x < 0){
            transform.localScale = new Vector3(1,1,1);
        } else transform.localScale = new Vector3(-1,1,1);
    }
    public void LookAround(){
        _count += Time.deltaTime;
        if(_count > 3f){
            if(transform.localScale == new Vector3(1,1,1)){
                transform.localScale = new Vector3(-1,1,1);
            } else if(transform.localScale == new Vector3(-1,1,1)){
                transform.localScale = new Vector3(1,1,1);
            }
            _count = 0;
        }
    }
    void OnDrawGizmos(){
        Gizmos.DrawWireSphere(Atkpoint.transform.position, atkrange);
    }
}
