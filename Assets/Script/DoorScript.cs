using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    AudioManager audiomanager;
    [SerializeField] GameObject playerr;
    [SerializeField] GameObject levelloader;
    // Start is called before the first frame update
    void Start()
    {
        audiomanager = GameObject.FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(playerr.transform.position, transform.position) < 1.5f && Input.GetKeyDown(KeyCode.F) && playerr.GetComponent<PlayerScript>().KeyCount > 0){
            StartCoroutine(PlayDoorSound());
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("door1");
            Debug.Log("The door is opened!");
            playerr.GetComponent<PlayerScript>().playerinfohandler.DeleteKey();
            //Load Scene or Moving Camera
            levelloader.GetComponent<LevelLoader>().LoadNextLevel();
        }
    }
    IEnumerator PlayDoorSound(){
        audiomanager.Play("OpenDoor");
        yield return new WaitForSeconds(0.5f);
        audiomanager.Play("CloseDoor");
    }
}
