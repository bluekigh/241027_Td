using System;
using UnityEngine;
using UnityEngine.UI;

public class Game_UI : MonoBehaviour
{
    public Text goldtext;
    public Text hptext;
    public Text StageText;
    // public Button BtnGold;
    // public Button BtnHp;
    int stageNum = 0;
    private void Start()
    {
        stageNum = 0;
        StageText.text = "Stage 1";
        App.Instance.goldChnage = goldtextChage;
        App.Instance.hpChnage = hptextChage;
        App.Instance.goldChnage.Invoke();
        App.Instance.hpChnage.Invoke();
        App.Instance.changestage += changeStage;

        // BtnGold.onClick.AddListener(() =>
        // {
        //     App.Instance.Gold--;
        // });
        // BtnHp.onClick.AddListener(() =>
        // {
        //     App.Instance.HP--;
        // });
    }

    private void changeStage()
    {
        if (stageNum == 0)
        {
            StageText.text = "Stage 2";
            stageNum++;
        }
        else return;
    }

    private void OnDisable()
    {
        App.Instance.goldChnage -= goldtextChage;
        App.Instance.hpChnage -= hptextChage;
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
