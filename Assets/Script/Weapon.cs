using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject parent;
    //This is used for deal with collision between weapon and player
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CircleCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            StartCoroutine(HandleCollisionWithPlayer(other));
        }
    }
    IEnumerator HandleCollisionWithPlayer(Collider2D newother){
        yield return new WaitForSeconds(0f);
        Debug.Log("We hit the player");
        newother.gameObject.GetComponent<PlayerScript>().TakeDamage(parent);
        //GetComponent<CircleCollider2D>().enabled = false;
    }
}
