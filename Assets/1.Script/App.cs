using System;
using UnityEngine;

public class App : Singleton<App>
{
    public int Gold = 10;
    private int hp;

    public int HP
    {
        get
        {
            return hp;
        }
        set
        {

            hp = value;
            if (hp <= 0)
            {
                Death();
            }
        }
    }

    private void Death()
    {
        Debug.LogWarning($" Game Over");

    }

    public bool BuyTower(int towercost)   // App.Instace.BuyTower(0) = true ->  돈있음. false면 돈없음.
    {
        if (towercost < Gold)
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
