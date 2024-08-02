using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CountDown : MonoBehaviour
{
    [SerializeField] Text textforskillbutton;
    UnityEngine.UI.Image image;
    Text text;
    float countdown;
    bool isusingskill;
    // Start is called before the first frame update
    void Start()
    {
        isusingskill = false;
        image = GetComponent<UnityEngine.UI.Image>();
        text = GetComponentInChildren<Text>();
        image.enabled = false;
        text.enabled = false;
        image.fillAmount = 1f;
        countdown = 1f;
        textforskillbutton.text = "Q";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && !isusingskill){
            image.enabled = true;
            text.enabled = true;
            isusingskill = true;
            textforskillbutton.text = "E";
        } else if(Input.GetKeyDown(KeyCode.E) && isusingskill){
            image.enabled = true;
            text.enabled = true;
            isusingskill = false;
            textforskillbutton.text = "Q";
        }
        if(image.enabled == true && text.enabled == true){
                float tmp = Mathf.Round(countdown*10.0f)*0.1f;
                text.text = tmp.ToString();
                countdown -= Time.deltaTime;
                image.fillAmount -= Time.deltaTime;
            }
        if(countdown < 0f && image.fillAmount == 0f){
            image.enabled = false;
            text.enabled = false;
            image.fillAmount = 1f;
            countdown = 1f;
        }

    }
}
