using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoHandler : MonoBehaviour
{
    [SerializeField] Slider HPBar, ManaBar;
    [SerializeField] TMP_Text HPText, ManaText;
    // Start is called before the first frame update
    void Start()
    {
        HPText.text = "100";
        HPBar.value = 100;
        ManaBar.value = 100;
        ManaText.text = "100";
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
    public int getHp(){
        return (int)HPBar.value;
    }
    public int getMana(){
        return (int)ManaBar.value;
    }
    //two more functions to increase hp and mana
}
