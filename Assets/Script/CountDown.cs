using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CountDown : MonoBehaviour
{
    UnityEngine.UI.Image image;
    Text text;
    float countdown;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<UnityEngine.UI.Image>();
        text = GetComponentInChildren<Text>();
        image.enabled = false;
        text.enabled = false;
        image.fillAmount = 1f;
        countdown = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            image.enabled = true;
            text.enabled = true;
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
