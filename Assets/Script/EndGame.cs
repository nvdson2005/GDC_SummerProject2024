using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    public void Endgame(){
        SceneManager.LoadScene(2);
    }
    void OnCollisionEnter2D(Collision2D other){
        Endgame();
    }
}
