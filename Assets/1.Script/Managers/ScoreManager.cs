using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text[] playername;
    public Text[] score;

    public Button Btn_Restart;
    public Button Btn_Quit;

    private void Start()
    {
        //for (int i = 0; i < 12; i++)
        //{
        //    playername[i].text = App.Instance.highscore[i].name;
        //    score[i].text = App.Instance.highscore[i].score.ToString();
        //}

        Btn_Restart.onClick.AddListener(() =>
        {
            App.Instance.sceneChange(1);
        });
        Btn_Quit.onClick.AddListener(() =>
        {
            App.Instance.sceneChange(3);
        });
    }

}
