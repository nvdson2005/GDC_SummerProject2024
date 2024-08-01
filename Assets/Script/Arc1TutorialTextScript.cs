using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Arc1TutorialTextScript : MonoBehaviour
{
    Image cover;
    GameObject playerr;
    public Text[] MonsterInfoList;
    Text text;
    public string[] texts;
    int Counter;
    // Start is called before the first frame update
    void Start()
    {
        cover = GetComponentInChildren<Image>();
        playerr = GameObject.FindGameObjectWithTag("Player");
        //Deactivate PlayerScript to show tutorial texts
        playerr.GetComponent<PlayerScript>().enabled = false;
        //
        Counter = 0;
        text = GetComponentInChildren<Text>();
        //StartCoroutine(ShowText());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && Counter < texts.Length){
            finalfortutorialtext();
        } else if (Input.GetMouseButtonDown(0) && Counter == texts.Length){
            cover.enabled = false;
            AppearMonsterInfo();
            playerr.GetComponent<PlayerScript>().enabled = true;
        }
    }
    // IEnumerator ShowText(){
    //     yield return new WaitForSeconds(5f);
    //     StartCoroutine(DisappearText());
    //     text.text = "You can turn yourself into many objects to trick the monsters!";
    //     StartCoroutine(AppearText());
    //     yield return new WaitForSeconds(4f);
    //     StartCoroutine(DisappearText());
    //     text.text = "First, press R to open your object selection. Objects that you can turn into will be highlighted";
    //     StartCoroutine(AppearText());
    //     yield return new WaitForSeconds(5f);
    //     StartCoroutine(DisappearText());
    //     text.text = "Next, move the player to the location of the object you want to turn into, then press R again to confirm that object";
    //     StartCoroutine(AppearText());
    //     yield return new WaitForSeconds(5f);
    //     StartCoroutine(DisappearText());
    //     text.text = "Finally, press Q to use your skill to turn into the selected object";
    //     StartCoroutine(AppearText());
    //     yield return new WaitForSeconds(5f);
    //     StartCoroutine(DisappearText());
    //     text.text = "Using skill decreases your mana. You can turn back into human by pressing E";
    //     StartCoroutine(AppearText());
    //     yield return new WaitForSeconds(5f);
    //     StartCoroutine(DisappearText());
    //     text.text = "Each type of monster can be tricked by different objects. So using your skill wisely!";
    //     StartCoroutine(AppearText());
    //     yield return new WaitForSeconds(5f);
    //     StartCoroutine(DisappearText());
    //     text.text = "Good luck!";
    //     StartCoroutine(AppearText());
    // }
    IEnumerator AppearText(Text text){
        for(float counter = 0f; counter<1f; counter+= 0.1f){
            float tmp = Mathf.Lerp(0f, 1f, counter / 1f);
            text.color = new Color(1, 1, 1, tmp);
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator DisappearText(Text text){
        for(float counter = 1f; counter>0f; counter-= 0.1f){
            float tmp = Mathf.Lerp(0f, 1f, counter / 1f);
            text.color = new Color(1, 1, 1, tmp);
            yield return new WaitForSeconds(0.1f);
        }
    }
    void ShowText(Text text){
        text.text = texts[Counter];
        Counter++;
    }
    void finalfortutorialtext(){
        Debug.Log("Clicked");
        StartCoroutine(DisappearText(text));
        ShowText(text);
        StartCoroutine(AppearText(text));
    }
    void AppearMonsterInfo(){
        foreach(Text text in MonsterInfoList){
            // text.enabled = true;
            StartCoroutine(AppearText(text));
            Counter++;
            // ShowText(text);
            // StartCoroutine(DisappearText(text));
        }
    }
}
