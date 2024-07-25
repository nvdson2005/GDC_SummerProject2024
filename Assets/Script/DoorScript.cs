using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] GameObject playerr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(playerr.transform.position, transform.position) < 1.5f && Input.GetKeyDown(KeyCode.F)){
            Debug.Log("The door is opened!");
            playerr.GetComponent<PlayerScript>().playerinfohandler.DeleteKey();
            //Load Scene or Moving Camera
        }
    }
}
