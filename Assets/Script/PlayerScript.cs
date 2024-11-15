using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
public class PlayerScript : MonoBehaviour
{
    [SerializeField] GameObject MainCharacterLight;
    float tmpplayerlocalscale;
    [SerializeField] float PlayerScale;
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
    //
    string nameofobjectbeselected;
    string tagofobjectselected;
    //
    public GameObject selection;
    bool openSelection;
    // Start is called before the first frame update
    void Start()
    {
        tmpplayerlocalscale = PlayerScale; //tmpplayerlocalscale is used to keep the value of PlayerScale. When transform, 
        //the value of PlayerScale will be changed (to 1) so that the size of the object is the same as the original object (because)
        //player's PlayerScale is not 1 (0.9 now). After transforming the object, PlayerScale will be set back to 0.9 by tmpplayerlocalscale.
        openSelection = false;
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
        MainCharacterLight.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("yvelocity", playerRigid.velocity.y);
        //Movement
        if(Input.GetKey(KeyCode.A)){
            anim.SetBool("isRun", true);
            this.transform.localScale = new Vector2(-PlayerScale, PlayerScale);
            transform.Translate(Vector3.left * _runforce * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D)){
            anim.SetBool("isRun", true);
            this.transform.localScale = new Vector2(PlayerScale, PlayerScale);
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
        //For transforming into other objects
        TransformHandler();
        //Reduce mana when using skill
        if(Input.GetKeyDown(KeyCode.R) && !openSelection){
            FindObjectOfType<AudioManager>().Play("OpenAndCloseObjectSelection");
            SelectTransformObject();
            openSelection = true;
        } else if (Input.GetKeyDown(KeyCode.R) && openSelection){
            FindObjectOfType<AudioManager>().Play("OpenAndCloseObjectSelection");
            DestroySelection();
            openSelection = false;
        }
    }
    void ChangeSpriteAccordingToTag(string tag, bool value){
        switch(tag){
                case "FakeChest":
                    anim.SetBool("isFakeChest", value);
                    break;
                case "FakeBag":
                    anim.SetBool("isFakeBag", value);
                    break;
                case "FakeContainer":
                    anim.SetBool("isFakeContainer", value);
                    break;
                case "FakeGrass":
                    anim.SetBool("isFakeGrass", value);
                    break;
                case "FakeBones":
                    anim.SetBool("isFakeBones", value);
                    break;
            }
    }
    void TransformHandler(){
        if(!istransformed){
            if(Input.GetKeyDown(KeyCode.Q) && nameofobjectbeselected != null && playerinfohandler.getMana() > 0){
            IgnoreEnemy();
            FindObjectOfType<AudioManager>().Play("Transform");
            istransformed = true;
            this.gameObject.tag = tagofobjectselected;
            ChangeSpriteAccordingToTag(tagofobjectselected, true);
            PlayerScale = 1f;
            transform.localScale = new Vector2(PlayerScale,PlayerScale);
            MainCharacterLight.SetActive(false);
        } else if (nameofobjectbeselected == null && Input.GetKeyDown(KeyCode.Q)){
            Debug.LogWarning("No object selected for player's skill");
        }
        } else{
            playerinfohandler.LoadPlayerImageInSkillIcon();
            time += Time.deltaTime;
            if(time >= 1){
                float tmp = UnityEngine.Random.Range(0f, 0.1f);
                int subtrahend = 4 + (int) Mathf.Lerp(0, Mana, tmp) + (int) UnityEngine.Random.Range(0f, 6f); 
                Mana -= subtrahend;
                playerinfohandler.UpdateManaWhenUseSkill(subtrahend);
                time = 0;
            }
            if(Mana <= 0 || (Input.GetKeyDown(KeyCode.Q))){
                StopIgnoreEnemy();
                FindObjectOfType<AudioManager>().Play("Transform");
                istransformed = false;
                time = 0;
                MainCharacterLight.SetActive(true);
                ChangeSpriteAccordingToTag(tagofobjectselected, false);
            PlayerScale = tmpplayerlocalscale;
            transform.localScale = new Vector2(PlayerScale,PlayerScale);
                this.gameObject.tag = "Player";
                playerinfohandler.LoadSkillImage(tagofobjectselected);
            }
        }
    }
    //Return an array that contains all objects that can be transformed
    GameObject[] ReturnGameObjectsThatCanBeTransformed(){
        GameObject[] gameObjects1 = GameObject.FindGameObjectsWithTag("FakeChest");
        GameObject[] gameObjects2 = GameObject.FindGameObjectsWithTag("FakeBag");
        GameObject[] gameObjects3 = GameObject.FindGameObjectsWithTag("FakeContainer");
        GameObject[] gameObjects4 = GameObject.FindGameObjectsWithTag("FakeBones");
        GameObject[] gameObjects5 = GameObject.FindGameObjectsWithTag("FakeGrass");
        GameObject[] gameObjects = gameObjects1.Concat(gameObjects2).Concat(gameObjects3).Concat(gameObjects4).Concat(gameObjects5).ToArray();
        return gameObjects;
    }
    //Open the selection menu
    void SelectTransformObject(){
        GameObject[] gameObjects = ReturnGameObjectsThatCanBeTransformed();
        //gameObjects.Append<GameObject>(GameObject.FindGameObjectsWithTag("Chest"));
        foreach(GameObject game in gameObjects){
            if(game.name == "Player") continue;
            Instantiate(selection, new Vector3(game.transform.position.x, game.transform.position.y + 0.5f, 0), Quaternion.identity, game.transform);
        }
    }
    //Destroy the selection menu 
    void DestroySelection(){
        GameObject[] gameObjects = ReturnGameObjectsThatCanBeTransformed();
        GameObject tmp = null;
        foreach(GameObject game in gameObjects){
            Collider2D coll = Physics2D.OverlapBox(game.transform.position, new Vector2(1, 1), 0, 1 << LayerMask.NameToLayer("Player"));
            if(coll != null){
                tmp = game;
            }
        }
        if(tmp != null){
            nameofobjectbeselected = tmp.GetComponent<SpriteRenderer>().sprite.name;
            tagofobjectselected = tmp.tag;
            playerinfohandler.LoadSkillImage(tagofobjectselected);
        }
        GameObject[] gameObjectss = GameObject.FindGameObjectsWithTag("Selection");
        foreach(GameObject game in gameObjectss){
            Destroy(game);
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
                playerRigid.velocity = new Vector2 (_hitForce, _hitForce);
            } else if (enemy.transform.localScale.x == -1){
                playerRigid.velocity = new Vector2 (-_hitForce, _hitForce);
            }
        Hp -= 10;
        //Debug
        Debug.Log("Hit by " + enemy.name);
        Debug.Log("hp " + Hp);
        playerinfohandler.UpdateHPWhenHit(10);
        StartCoroutine(Reset());
        //If the HP is equal to 0, the player is dead
        if(Hp <= 0){
            Dead();
        }
    }
    //Using for handle the damage effect
    public void TakeDamage(int dmg, GameObject enemy){
        FindObjectOfType<AudioManager>().Play("Hurt");
        Hp -= dmg;
        playerinfohandler.UpdateHPWhenHit(dmg);
        if(transform.position.x > enemy.transform.position.x){
                playerRigid.velocity = new Vector2 (_hitForce, _hitForce);
            } else if (transform.position.x <= enemy.transform.position.x){
                playerRigid.velocity = new Vector2 (-_hitForce, _hitForce);
            }
        StartCoroutine(Reset());
        if(Hp <= 0){
            Dead();
        }
    }
    IEnumerator Reset(){
        yield return new WaitForSeconds(1f);
        playerRigid.velocity = Vector2.zero;
    }
    //Heal when using bottles
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
    //Collect key using collider with trigger on
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Key")){
            Destroy(other.gameObject);
            KeyCount++;
            playerinfohandler.AddKey();
            FindObjectOfType<AudioManager>().Play("GetKey");
        }      
    }
    //Ground check and add force when being hit
    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == 6 || other.gameObject.layer == 9){
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
    //These two functions are used to ignore the collision between the player and the enemy when using skill
    public void IgnoreEnemy(){
        Debug.Log("Ignore is called");
        Physics2D.IgnoreLayerCollision(7,8, true);
    }
    public void StopIgnoreEnemy(){
        Debug.Log("StopIgnore is called");
        Physics2D.IgnoreLayerCollision(7,8, false);
    }
    //Things that happen when the main character dies
    private void Dead(){
        if(anim.enabled == false) anim.enabled = true;
        anim.SetTrigger("isDead");
        Debug.Log("Player dies");
        GetComponent<PlayerScript>().enabled = false;
        Invoke("Endgame", 2f);
    }
    //Load End Game Scene
    public void Endgame(){
        SceneManager.LoadScene(2);
    }
    //For external set for KeyCount in PlayerInfoHandler.cs
    public void DeleteKey(){
        if(KeyCount > 0){
            KeyCount--;
            playerinfohandler.DeleteKey();
        }
    }
    //For external set for isGrounded in Jumper.cs
    public void SetIsGrounded(bool value){
        isGrounded = value;
    }
}
