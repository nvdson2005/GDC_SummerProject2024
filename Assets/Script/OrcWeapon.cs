using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcWeapon : MonoBehaviour
{
    [SerializeField] GameObject parent;
    GameObject playerr;
    //This is used for deal with collision between weapon and player
    // Start is called before the first frame update
    void Start()
    {
        
        playerr = GameObject.FindGameObjectWithTag("Player");
        GetComponent<CircleCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            playerr.GetComponent<PlayerScript>().StopIgnoreEnemy();
            other.gameObject.GetComponent<PlayerScript>().TakeDamage(parent);
        } else if(other.gameObject.CompareTag("FakeBag") || other.gameObject.CompareTag("FakeChest")){
                playerr.GetComponent<PlayerScript>().StopIgnoreEnemy();
                other.gameObject.GetComponent<PlayerScript>().TakeDamage(parent);
                playerr.GetComponent<PlayerScript>().IgnoreEnemy();
            }
    }
}
