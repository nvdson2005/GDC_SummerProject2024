using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsScript : MonoBehaviour
{
    Collider2D _collider;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("FakeChest")){
            other.gameObject.GetComponent<PlayerScript>().TakeDamage(this.gameObject);
            _collider.enabled = false;
        }
    }
    
}
