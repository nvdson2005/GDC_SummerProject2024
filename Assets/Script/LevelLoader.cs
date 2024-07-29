using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    Animator transition;
    public float transitionTime = 1f;

    void Start(){
        transition = GetComponentInChildren<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void LoadNextLevel(){
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public IEnumerator LoadLevel(int level){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(level);
    }
}
