using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    GameObject startpoint;
    [HideInInspector]
    public PlayerInfoHandler playerinfohandler;
    public int KeyCount{get; private set;}
    [SerializeField] int Hp;
    [SerializeField] int Mana;
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
        startpoint = GameObject.Find("PlayerStartPoint");
        transform.position = startpoint.transform.position;
        playerinfohandler = FindObjectOfType<PlayerInfoHandler>().GetComponent<PlayerInfoHandler>();
        DontDestroyOnLoad(this);
        KeyCount = 0;
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
            FindObjectOfType<AudioManager>().Play("Jump");
        }
        if(Input.GetKeyDown(KeyCode.Q)){
            FindObjectOfType<AudioManager>().Play("Transform");
            Debug.Log(" Q Pressed in Player");
            anim.enabled = false;
            spriteRenderer.sprite = Resources.Load<Sprite>("crate_1");
            istransformed = true;
            this.gameObject.tag = "FakeChest";
        }
        //Reduce mana when using skill
        if(istransformed){
            IgnoreEnemy();
            time += Time.deltaTime;
            //Debug.Log(time);
            if(time >= 1){
                Mana -= 4;
                playerinfohandler.UpdateManaWhenUseSkill(4);
                time = 0;
            }
            if(Mana <= 0 || (Input.GetKeyDown(KeyCode.E) && istransformed)){
                FindObjectOfType<AudioManager>().Play("Transform");
                istransformed = false;
                time = 0;
                anim.enabled = true;
                Resources.UnloadAsset(spriteRenderer.sprite);
                this.gameObject.tag = "Player";
                StopIgnoreEnemy();
            }
        }
        if(Input.GetKeyDown(KeyCode.R)){
            SelectTransformObject();
        }
    }
    void SelectTransformObject(){
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("FakeChest");
        foreach(GameObject game in gameObjects){
            Debug.Log(game.name);
        }
    }
    //Play Walk Sound Effect (In Animation Event)
    public void PlayWalkSfx(){
        if(isGrounded){
                if(!FindObjectOfType<AudioManager>().IsPlaying("Walk")){
                    FindObjectOfType<AudioManager>().Play("Walk");
                }else
                FindObjectOfType<AudioManager>().Stop("Walk");
        }
        else FindObjectOfType<AudioManager>().Stop("Walk");
    }
    //TakeDamage is used to call the hit force in the Enemy Script
    public void TakeDamage(GameObject enemy){
        FindObjectOfType<AudioManager>().Play("Hurt");
        if(enemy.gameObject.CompareTag("Traps")){
            playerinfohandler.UpdateHPWhenHit(Hp);
            Hp -= Hp;
            Dead();
            return;
        }
        if(enemy.gameObject.CompareTag("TrapsNotDead")){
            Hp -= 30;
            playerinfohandler.UpdateHPWhenHit(30);
            return;
        }
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
    public void Heal(GameObject bottle){
        FindObjectOfType<AudioManager>().Play("Heal");
        if(bottle.CompareTag("HPBottle")){
            Hp += 20;
            if(Hp > 100) Hp = 100;
            playerinfohandler.IncreaseHP(20);
        }
        if(bottle.CompareTag("ManaBottle")){
            Mana += 30;
            if(Mana > 100) Mana = 100;
            playerinfohandler.IncreaseMana(30);
        }
    }
    IEnumerator Reset(){
        yield return new WaitForSeconds(1f);
        playerRigid.velocity = Vector2.zero;
    }
    void OnTriggerEnter2D(Collider2D other){
        //Collect Key
        if(other.gameObject.CompareTag("Key")){
            Destroy(other.gameObject);
            KeyCount++;
            playerinfohandler.AddKey();
            FindObjectOfType<AudioManager>().Play("GetKey");
        }      
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
        }
        
    }
    private void IgnoreEnemy(){
        Physics2D.IgnoreLayerCollision(7,8, true);
    }
    private void StopIgnoreEnemy(){
        Physics2D.IgnoreLayerCollision(7,8, false);
    }
    private void Dead(){
        if(anim.enabled == false) anim.enabled = true;
        anim.SetTrigger("isDead");
        Debug.Log("Player dies");
        GetComponent<PlayerScript>().enabled = false;
    }
}
