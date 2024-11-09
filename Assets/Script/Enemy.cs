using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    CircleCollider2D WeaponCollider;
    public GameObject PointA;
    public GameObject PointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;
    public LayerMask layerr;
    private RaycastHit2D ray;
    //private Collider2D childcollider;
    [SerializeField] private float regRange;
    private GameObject player;
    [SerializeField] GameObject Atkpoint;
    [SerializeField] float atkrange;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //Initialization for weapon collider
        WeaponCollider = GetComponentInChildren<CircleCollider2D>();
        WeaponCollider.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = PointA.transform;
        transform.localScale = new Vector3(-1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(SeePlayer()){
            //anim.SetBool("isPatrol", false);
            anim.SetBool("Attack", true);
            Invoke("Attack", 1f);
            // Invoke("Attack", 1f);
        } else {
            anim.SetBool("Attacking",false);
            //childcollider.enabled = false;
            anim.SetBool("Attack", false);
            Move();
        }
    }

    public bool SeePlayer(){
        if(transform.localScale.x == 1){
            ray = Physics2D.Raycast(transform.position, Vector3.right, regRange, 1 << LayerMask.NameToLayer("Player"));
            Debug.DrawRay(transform.position, Vector3.right * regRange, Color.red);
        } else if(transform.localScale.x == -1)
        {
            ray = Physics2D.Raycast(transform.position, Vector3.left, regRange, 1 << LayerMask.NameToLayer("Player"));
            Debug.DrawRay(transform.position, Vector3.left * regRange, Color.red);
        }
        bool res = false;
        if(ray.collider != null){
            if(ray.collider.gameObject.CompareTag("Player") || !ray.collider.gameObject.CompareTag("FakeBones")){
                res = true;
                //Debug.Log("Enemy can see player");
            }
        }
        return res;
    }
    public void Move(){
        anim.SetBool("isPatrol", true);
        transform.position = Vector2.MoveTowards(transform.position, currentPoint.position, speed*Time.deltaTime);
        if(Vector2.Distance(transform.position, PointB.transform.position) < 2f){
            currentPoint = PointA.transform;
            anim.SetBool("isPatrol", false);
            transform.localScale = new Vector3(-1, 1, 1);
        } else if (Vector2.Distance(transform.position, PointA.transform.position) < 2f ){
            currentPoint = PointB.transform;
            anim.SetBool("isPatrol", false);
            transform.localScale = new Vector3(1, 1, 1);
        }
        if(transform.position.x - PointA.transform.position.x < 0){
            transform.localScale = new Vector3(1,1,1);
        }
        if(transform.position.x - PointB.transform.position.x > 0){
            transform.localScale = new Vector3(-1,1,1);
        }
    }
    public void Attack(){
        // transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed* 2 *Time.deltaTime);
        if(transform.localScale.x == 1){
            rb.velocity = Vector2.right * 2 *speed;
        } else if(transform.localScale.x == -1){
            rb.velocity = Vector2.left * 2 * speed;
        }
        //Debug.Log("Distance between player and skeleton: " + Vector2.Distance(transform.position, player.transform.position));
        if(Vector2.Distance(transform.position, player.transform.position) < 1.5f){
            anim.SetBool("Attacking", true);
            
        } else anim.SetBool("Attacking", false);
        if(Vector2.Distance(transform.position, player.transform.position) < 0.8f){
            rb.velocity = Vector2.zero;
        }
        //Making hit is called by Animation Event
    }
    public void MakingHit(){
        WeaponCollider.enabled = true;
        StartCoroutine(ResetAttack());
    }
    IEnumerator ResetAttack(){
        yield return new WaitForSeconds(0.3f);
        WeaponCollider.enabled = false;
    }
    void OnDrawGizmos(){
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(PointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(PointB.transform.position, 0.5f);
        Gizmos.DrawLine(PointA.transform.position,PointB.transform.position);
    }
}
