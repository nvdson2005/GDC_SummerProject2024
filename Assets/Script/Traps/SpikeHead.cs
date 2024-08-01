using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : MonoBehaviour
{
    [SerializeField] float Speed, AccelerateTime;
    float realspeed, counter;
    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        realspeed = 0;
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(counter <= AccelerateTime){
            counter += Time.deltaTime;
            realspeed = Mathf.Lerp(0, Speed, counter/AccelerateTime);
        }
        rigid.velocity = new Vector2(realspeed, 0);
    }
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.layer == 7){
            other.gameObject.GetComponent<PlayerScript>().TakeDamage(10, this.gameObject);
        } else{
            StartCoroutine(Reset());
        }
    }
    // void OnTriggerEnter2D(Collider2D other){
    //     if(other.gameObject.tag == "Player"){
    //         other.gameObject.GetComponent<PlayerScript>().TakeDamage(10, this.gameObject);
    //     }
    // }
    IEnumerator Reset(){
        yield return new WaitForSeconds(1f);
        realspeed = 0;
            counter = 0;
            Speed = -Speed;
    }
}
