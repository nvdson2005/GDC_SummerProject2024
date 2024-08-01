using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfWeapon : MonoBehaviour
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
        playerr.GetComponent<PlayerScript>().StopIgnoreEnemy();
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerScript>().TakeDamage(parent);
        } else if(other.gameObject.CompareTag("FakeBag") || other.gameObject.CompareTag("FakeContainer")){
                other.gameObject.GetComponent<PlayerScript>().TakeDamage(parent);
                playerr.GetComponent<PlayerScript>().IgnoreEnemy();
            }
    }
    void ReIgnore(){
        playerr.GetComponent<PlayerScript>().IgnoreEnemy();
    }
}
