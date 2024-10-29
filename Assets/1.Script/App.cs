using System;
using UnityEngine;

public class App : Singleton<App>
{
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
            goldChnage.Invoke();
        }
    }
    public Action goldChnage;
    public Action hpChnage;
    private int hp = 10;

    public int HP
    {
        get
        {
            return hp;
        }
        set
        {

            hp = value;
            hpChnage.Invoke();
            if (hp <= 0)
            {
                Death();
            }
        }
    }

    public Vector3[] Waypoint { get; internal set; }

    private void Death()
    {
        Debug.LogWarning($" Game Over");

    }


    public bool BuyTower(int towercost)   // App.Instace.BuyTower(0) = true ->  돈있음. false면 돈없음.
    {
        if (towercost < gold)
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
    }

}
