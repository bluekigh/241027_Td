using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score
{
    public string name;
    public int score;
    public Score(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}


public class App : Singleton<App>
{
    [SerializeField]
    public List<Score> highscore;
    private int gold = 10;
    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            if (goldChnage != null) goldChnage.Invoke();
        }
    }
    public Action changestage;
    public Action goldChnage;
    public Action hpChnage;
    private int hp = 10;
    int curStage = 1;
    public int HP
    {
        get
        {
            return hp;
        }
        set
        {

            hp = value;
            if (hpChnage != null) hpChnage.Invoke();
            if (hp <= 0)
            {
                Death();
            }
        }
    }
    public Action Explosion;
    public Vector3[] Waypoint { get; internal set; }
    public int CurStage
    {
        get => curStage;
        set
        {
            curStage = value;

        }
    }

    private void Death()
    {
        Debug.LogWarning($" Game Over");
        sceneChange(1);

    }


    public bool BuyTower(int towercost)   // App.Instace.BuyTower(0) = true ->  돈있음. false면 돈없음.
    {
        if (towercost <= gold)
        {
            Gold -= towercost;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ReachDestination(int hp)
    {
        HP -= hp;
        Explosion.Invoke();
    }

    public void sceneChange(int sceneName)
    {
        switch (sceneName)
        {
            case 0:
                SceneManager.LoadScene("0_Title");
                break;

            case 1:
                SceneManager.LoadScene("1_Game");
                App.Instance.HP = 10;  // TODO : brute init Need change
                App.Instance.Gold = 10; // TODO : brute init Need change
                break;

            case 2:
                SceneManager.LoadScene("2_HighScore");
                break;
            case 3:
                SceneManager.LoadScene("3_Quit");
                break;
            default:
                Debug.Log($" Error. in App.sceneChange");
                break;
        }

    }
    public void Start()
    {
        initHighScore();

    }

    private void initHighScore()
    {
        highscore = new List<Score>();
        for (int i = 0; i < 12; i++)
        {
            highscore.Add(new Score("AAA" + i, 10 + i)); // AAA0, AAA1, ..., AAA11
        }
    }

    public void AddScore(string name, int score)
    {
        highscore.Add(new Score(name, score));
        highscore = highscore.OrderByDescending(x => x.score).Take(12).ToList();

    }

    public void SaveScore()
    {
        ScoreList scoreList = new ScoreList() { scores = highscore };
        string json = JsonUtility.ToJson(scoreList);
        PlayerPrefs.SetString("highscoreKey", json);
        PlayerPrefs.Save();
    }

    public void LoadScore()
    {
        Debug.Log($" before {highscore.Count} score");

        if (PlayerPrefs.HasKey("highscoreKey"))
        {
            string json = PlayerPrefs.GetString("highscoreKey");
            Debug.Log($" Json is {json}");
            ScoreList loadedscore = JsonUtility.FromJson<ScoreList>(json);
            highscore = loadedscore.scores;
        }

        Debug.Log($" After load: {highscore.Count} score");
        if (highscore.Count > 0)
        {
            foreach (var item in highscore)
            {
                Debug.Log($" {item.name}, {item.score}");
            }
        }
    }
}

[System.Serializable]
public class ScoreList
{
    public List<Score> scores;
}