// using System.Collections;
// using System.Collections.Generic;
// using UnityEditor;
// using UnityEngine;

// public class Weapon : MonoBehaviour
// {
//     GameObject prefabGameobject;
//     [SerializeField] GameObject parent;
//     GameObject playerr;
//     //This is used for deal with collision between weapon and player
//     // Start is called before the first frame update
//     void Start()
//     {
//         prefabGameobject = PrefabUtility.GetCorrespondingObjectFromSource(this.gameObject);
//         Debug.Log(prefabGameobject.name);
//         playerr = GameObject.FindGameObjectWithTag("Player");
//         GetComponent<CircleCollider2D>().enabled = false;
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
//     void OnTriggerEnter2D(Collider2D other){
        
//         //var prefabGameobject = PrefabUtility.GetCorrespondingObjectFromSource(this.gameObject);
//         Debug.Log(prefabGameobject.name);
//         if(prefabGameobject.name == "Wolf"){
//             if(other.gameObject.CompareTag("FakeBag") || other.gameObject.CompareTag("FakeContainer") || other.gameObject.CompareTag("Player")){
//                 playerr.GetComponent<PlayerScript>().StopIgnoreEnemy();
//                 other.gameObject.GetComponent<PlayerScript>().TakeDamage(parent);
//                 playerr.GetComponent<PlayerScript>().IgnoreEnemy();
//             }
//         }
//         if(prefabGameobject.name == "Orc"){
//             if(other.gameObject.CompareTag("FakeBag") || other.gameObject.CompareTag("FakeChest") || other.gameObject.CompareTag("Player")){
//                 playerr.GetComponent<PlayerScript>().StopIgnoreEnemy();
//                 other.gameObject.GetComponent<PlayerScript>().TakeDamage(parent);
//                 playerr.GetComponent<PlayerScript>().IgnoreEnemy();
//             }
//         }
//         if(prefabGameobject.name == "Skeleton"){
//             if(other.gameObject.CompareTag("FakeContainer") || other.gameObject.CompareTag("FakeChest") || other.gameObject.CompareTag("Player")){
//                 playerr.GetComponent<PlayerScript>().StopIgnoreEnemy();
//                 other.gameObject.GetComponent<PlayerScript>().TakeDamage(parent);
//                 playerr.GetComponent<PlayerScript>().IgnoreEnemy();
//             }
//         }
//         // if(other.gameObject.CompareTag("Player")){
//         //     // StartCoroutine(HandleCollisionWithPlayer(other));
//         //     other.gameObject.GetComponent<PlayerScript>().TakeDamage(parent);
//         // }
//         // if(other.gameObject.CompareTag("FakeChest")){
            
//         //     other.gameObject.GetComponent<PlayerScript>().TakeDamage(parent);
//         // }
//         // if(other.gameObject.CompareTag("FakeBag")){
//         //     other.gameObject.GetComponent<PlayerScript>().TakeDamage(parent);
//         // }
//         // if(other.gameObject.CompareTag("FakeContainer")){
//         //     other.gameObject.GetComponent<PlayerScript>().TakeDamage(parent);
//         // }
//     }
// }
