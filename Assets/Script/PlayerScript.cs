using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    float time;
    [SerializeField] int _jumpforce;
    [SerializeField] int _runforce;
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
            time += Time.deltaTime;
            if(time >= 10f){
                istransformed = false;
                time = 0;
                anim.enabled = true;
                Resources.UnloadAsset(spriteRenderer.sprite);
                this.gameObject.tag = "Player";
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == 6){
            isGrounded = true;
            anim.SetBool("isJump", false);
        }
    }
}
