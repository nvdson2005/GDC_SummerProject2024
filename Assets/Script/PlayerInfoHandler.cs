using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoHandler : MonoBehaviour
{
    [SerializeField] Text KeyCountText;
    public Image SkillImage;
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
        SkillImage.sprite = Resources.Load<Sprite>("firstskillicon");
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
    public void LoadSkillImage(string skilliconname = "face_normal"){
        Resources.UnloadAsset(SkillImage.sprite);
        SkillImage.sprite = Resources.Load<Sprite>(skilliconname);
    }
    //two more functions to increase hp and mana
}
