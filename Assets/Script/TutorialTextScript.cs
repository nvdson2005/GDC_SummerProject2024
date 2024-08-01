using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextScript : MonoBehaviour
{
    GameObject playerr;
    float Counter;
    [SerializeField] float MaxCount;
    [SerializeField] Text WelcomeText;
    //[SerializeField] Text TutorialText1, TutorialText2, TutorialText3;
    public List<Text> tutorialtexts = new List<Text>();
    // Start is called before the first frame update
    void Start()
    {
        playerr = GameObject.FindGameObjectWithTag("Player");
        Counter = 0;
        WelcomeText.color = new Color(1, 1, 1, 0);
        WelcomeText.text = "Welcome!";
        foreach(Text text in tutorialtexts){
            text.color = new Color(1, 1, 1, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Appear(WelcomeText);
        foreach(Text text in tutorialtexts){
                if(Mathf.Abs(text.gameObject.transform.position.x - playerr.transform.position.x) < 1.5f){
                    Appear(text);
                }
        }
    }
    void Appear(Text text){
        if(text.color.a < 1f){
        if(!(Counter > MaxCount))
        {
            Counter += Time.deltaTime;
        float tmp = Mathf.Lerp(0f, 1f, Counter / MaxCount);
        text.color = new Color(1, 1, 1, tmp);
        } else Counter = 0;
        }
    }
}
