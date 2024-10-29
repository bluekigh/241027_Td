using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawn : MonoBehaviour
{
    public Transform EnemyPrefab;          //적 프리팹
    public float  WavesTime  =5f;          //적 생성 시간 간격
    private float Countdown  = 2f;         //시작하기전 카운트
    private int WaveNumber   = 1;          //적 마리수

    private void Update()
    {
        if(Countdown <= 0f)
        {
            WaveSpawn();
            Countdown = WavesTime;
        }
        Countdown -= Time.deltaTime;
    }
    private void WaveSpawn()
    {
        for (int i = 0; i < WaveNumber; i++)
        {
            EnemySpawn();
        }
        
    }
    private void EnemySpawn()
    {

        Vector3 spawnpoint = App.Instance.Waypoint[0];
        Instantiate(EnemyPrefab, spawnpoint, Quaternion.Euler(0, 180, 0));
    }
}
