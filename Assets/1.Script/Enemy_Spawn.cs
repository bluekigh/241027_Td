using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawn : MonoBehaviour
{
    public Transform EnemyPrefab;          //적 프리팹
    public float  WavesTime  =5f;          //적 생성 시간 간격
    private float Countdown  = 2f;         //시작하기전 카운트
    private int WaveNumber   = 1;          //스테이지 번호

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
        WaveNumber++;
    }
    private void EnemySpawn()
    {

        //Instantiate(EnemyPrefab, waypoints.points[0].position, waypoints.points[0].rotation );
        Instantiate(EnemyPrefab, App.Instance.Waypoint[0], Quaternion.identity);
    }
}
