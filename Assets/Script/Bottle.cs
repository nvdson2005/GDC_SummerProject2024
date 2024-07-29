using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("FakeChest")){
            other.gameObject.GetComponent<PlayerScript>().Heal(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
