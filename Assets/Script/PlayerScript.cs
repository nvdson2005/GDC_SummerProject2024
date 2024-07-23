using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] int Hp;
    [SerializeField] int Mana;
    public PlayerInfoHandler playerinfohandler;
    public float time {get; private set;}
    [SerializeField] int _jumpforce;
    [SerializeField] int _runforce;
    [SerializeField] float _hitForce;
    Animator anim;
    Rigidbody2D playerRigid;
    bool isGrounded;
    bool istransformed;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
        time = 0;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isGrounded = true;
        playerRigid = GetComponent<Rigidbody2D>();
        istransformed = false;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("yvelocity", playerRigid.velocity.y);
        //Movement
        if(Input.GetKey(KeyCode.A)){
            anim.SetBool("isRun", true);
            this.transform.localScale = new Vector3(-1, 1, 1);
            transform.Translate(Vector3.left * _runforce * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D)){
            anim.SetBool("isRun", true);
            this.transform.localScale = new Vector3(1,1,1);
            transform.Translate(Vector3.right * _runforce * Time.deltaTime);
        } else{
            anim.SetBool("isRun", false);
        }
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            anim.SetBool("isJump", true);
            playerRigid.AddForce(new Vector2(0, _jumpforce), ForceMode2D.Impulse);
            isGrounded = false;
        }
        if(Input.GetKeyDown(KeyCode.Q)){
            Debug.Log(" Q Pressed in Player");
            anim.enabled = false;
            //Tim mot animation bien hinh roi bo vo truoc phan load Sprite.
            spriteRenderer.sprite = Resources.Load<Sprite>("crate_1");
            istransformed = true;
            this.gameObject.tag = "FakeChest";
        }
        if(istransformed){
            IgnoreEnemy();
            time += Time.deltaTime;
            Debug.Log(time);
            if(time >= 1){
                Mana -= 2;
                playerinfohandler.UpdateManaWhenUseSkill(2);
                time = 0;
            }
            if(Mana <= 0 || (Input.GetKeyDown(KeyCode.E) && istransformed)){
                istransformed = false;
                time = 0;
                anim.enabled = true;
                Resources.UnloadAsset(spriteRenderer.sprite);
                this.gameObject.tag = "Player";
                StopIgnoreEnemy();
            }
        }
    }
    //TakeDamage is used to call the hit force in the Enemy Script
    public void TakeDamage(GameObject enemy){
        if(enemy.transform.localScale.x == 1){
                //playerRigid.AddForce(Vector2.one*_hitForce, ForceMode2D.Impulse);
                playerRigid.velocity = new Vector2 (_hitForce, _hitForce);
            } else if (enemy.transform.localScale.x == -1){
                playerRigid.velocity = new Vector2 (-_hitForce, _hitForce);
                //playerRigid.AddForce(new Vector2(-1,1) * _hitForce, ForceMode2D.Impulse);
            }
        // playerRigid.velocity = new Vector2 (_hitForce, _hitForce);
        Hp -= 10;
        Debug.Log("Hit by " + enemy.name);
        Debug.Log("hp " + Hp);
        playerinfohandler.UpdateHPWhenHit(10);
        //Disable Keyboard Movement
        StartCoroutine(Reset());
        //If the HP is equal to 0, the player is dead
        if(Hp <= 0){
            Dead();
        }
    }
    IEnumerator Reset(){
        yield return new WaitForSeconds(1f);
        playerRigid.velocity = Vector2.zero;
    }
    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == 6){
            isGrounded = true;
            anim.SetBool("isJump", false);
        }
        if(other.gameObject.CompareTag("Enemy")){
            //Debug.Log("Player is hit");
            if(other.transform.localScale.x == 1){
                playerRigid.AddForce(Vector2.one*_hitForce, ForceMode2D.Impulse);
            } else if (other.transform.localScale.x == -1){
                playerRigid.AddForce(new Vector2(-1,1) * _hitForce, ForceMode2D.Impulse);
            }
            
            //Decrease HP
        }
        // if(istransformed){
        //     if(other.gameObject.CompareTag("Enemy")){
        //         Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.collider, true);
        //         Debug.Log("Collision Ignored: " + Physics2D.GetIgnoreCollision(GetComponent<Collider2D>(), other.collider));
        //     }
        // } else{
        //     Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.collider, false);
        //     Debug.Log("Collision Ignored: " + Physics2D.GetIgnoreCollision(GetComponent<Collider2D>(), other.collider));
        // }
    }
    private void IgnoreEnemy(){
        Physics2D.IgnoreLayerCollision(7,8, true);
    }
    private void StopIgnoreEnemy(){
        Physics2D.IgnoreLayerCollision(7,8, false);
    }
    private void Dead(){
        anim.SetTrigger("isDead");
        GetComponent<PlayerScript>().enabled = false;
    }
}
