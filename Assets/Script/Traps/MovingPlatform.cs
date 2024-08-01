using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float MovingTime, Speed;
    float Counter;
    // Start is called before the first frame update
    void Start()
    {
        Counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Counter <= MovingTime)
        {transform.Translate(new Vector2(Speed, 0) * Time.deltaTime);
        Counter += Time.deltaTime;}
        else{
            Speed = -Speed;
            Counter = 0;
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer == 7){
            other.transform.SetParent(this.transform);
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.layer == 7){
            other.transform.SetParent(null);
        }
    }
}
