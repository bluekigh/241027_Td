using System;
using UnityEngine;
using UnityEngine.UI;

public class Game_UI : MonoBehaviour
{
    public Text goldtext;
    public Text hptext;
    // public Button BtnGold;
    // public Button BtnHp;

    private void Start()
    {
        App.Instance.goldChnage += goldtextChage;
        App.Instance.hpChnage += hptextChage;
        App.Instance.goldChnage.Invoke();
        App.Instance.hpChnage.Invoke();

        // BtnGold.onClick.AddListener(() =>
        // {
        //     App.Instance.Gold--;
        // });
        // BtnHp.onClick.AddListener(() =>
        // {
        //     App.Instance.HP--;
        // });
    }

    private void hptextChage()
    {
        hptext.text = App.Instance.HP.ToString();
    }

    private void goldtextChage()
    {
        goldtext.text = App.Instance.Gold.ToString();
    }
}
