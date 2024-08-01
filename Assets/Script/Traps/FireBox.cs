using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBox : MonoBehaviour
{
    Animator anim;
    [SerializeField] GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.layer == 7){
            anim.SetBool("isFire", true);
            StartCoroutine(Fire());
        }
    }
    void OnCollisionExit2D(Collision2D other){
        if(other.gameObject.layer == 7){
            anim.SetBool("isFire", false);
        }
    }
    IEnumerator Fire(){
        yield return new WaitForSeconds(1f);
        child.SetActive(true);
        yield return new WaitForSeconds(1f);
        child.SetActive(false);
    }
}
