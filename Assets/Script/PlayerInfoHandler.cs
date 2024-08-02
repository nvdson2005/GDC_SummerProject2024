using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoHandler : MonoBehaviour
{
    [SerializeField] Text KeyCountText;
    public Image[] skillimages;
    [SerializeField] Slider HPBar, ManaBar;
    [SerializeField] TMP_Text HPText, ManaText;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        KeyCountText.text = "0";
        HPText.text = "100";
        HPBar.value = 100;
        ManaBar.value = 100;
        ManaText.text = "100";
        for(int i=1; i<=3; i++){
            skillimages[i].GetComponent<Image>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateHPWhenHit(int HP){
        HPBar.value -= HP;
        HPText.text = HPBar.value.ToString();
    }
    public void UpdateManaWhenUseSkill(int Mana){
        ManaBar.value -= Mana;
        ManaText.text = ManaBar.value.ToString();
    }
    public void IncreaseHP(int HP){
        HPBar.value += HP;
        if(HPBar.value > 100) HPBar.value = 100;
        HPText.text = HPBar.value.ToString();
    }
    public void IncreaseMana(int mana){
        ManaBar.value += mana;
        if(ManaBar.value > 100) ManaBar.value = 100;
        ManaText.text = ManaBar.value.ToString();
    }
    public int getHp(){
        return (int)HPBar.value;
    }
    public int getMana(){
        return (int)ManaBar.value;
    }
    public void AddKey(){
        KeyCountText.text = (int.Parse(KeyCountText.text) + 1).ToString();
    }
    public void DeleteKey(){
        if(int.Parse(KeyCountText.text) > 0) KeyCountText.text = (int.Parse(KeyCountText.text) - 1).ToString();
    }
    public void LoadSkillImage(string tag){
        tag += "Image";
        Debug.Log(tag);
        foreach(Image image in skillimages){
            if(image.gameObject.name == tag){
                image.GetComponent<Image>().enabled = true;
            } else {
                image.GetComponent<Image>().enabled = false;
            }
        }
    }
    public void LoadPlayerImageInSkillIcon(){
        for(int i=0; i<=3; i++){
            skillimages[i].GetComponent<Image>().enabled = false;
        }
        skillimages[4].GetComponent<Image>().enabled = true;
    }
    //two more functions to increase hp and mana
}
