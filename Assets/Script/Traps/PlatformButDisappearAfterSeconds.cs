using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformButDisappearAfterSeconds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 7)
        {
            StartCoroutine(Troll());
        }
    }
    IEnumerator Troll(){
        yield return new WaitForSeconds(2f);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1f);
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
    }
}
