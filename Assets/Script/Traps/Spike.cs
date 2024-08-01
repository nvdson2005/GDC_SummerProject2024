using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer == 7){
            
            GetComponent<Animator>().SetBool("isAttack", true);
            StartCoroutine(DelayAttack(other.gameObject));
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.layer == 7){
            GetComponent<Animator>().SetBool("isAttack", false);
        }
    }
    IEnumerator DelayAttack(GameObject other){
        yield return new WaitForSeconds(0.2f);
        other.gameObject.GetComponent<PlayerScript>().TakeDamage(15, this.gameObject);
    }
}

