using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer == 7){
            other.gameObject.GetComponent<PlayerScript>().Heal(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
