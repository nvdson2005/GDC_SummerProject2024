using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameObject.Find("AudioManager"));
        Destroy(GameObject.Find("Player"));
        Destroy(GameObject.Find("PlayerInfo"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Restart(){
        SceneManager.LoadScene(0);
    }
    public void Quit(){
        Application.Quit();
    }
}
