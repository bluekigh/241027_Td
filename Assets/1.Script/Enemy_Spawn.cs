using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawn : MonoBehaviour
{
    public Transform EnemyPrefab;          //적 프리팹
    public Transform spawnPoint;           //적 생성 지점
    public float  WavesTime  =5f;          //적 생성 시간 간격

    private float Countdown  = 3f;         //시작하기전 카운트
    private int WaveIndex    = 0;          //스테이지 번호

    private void Update()
    {
        if(Countdown <= 0f)
        {
            StartCoroutine(WaveSpawn());
            Countdown = WavesTime;
        }
        Countdown -= Time.deltaTime;
    }
    IEnumerator WaveSpawn()
    {
        WaveIndex++;
        for (int i = 0; i < WaveIndex; i++)
        {
            EnemySpawn();
            yield return new WaitForSeconds(0.5f);
        }
    }
    private void EnemySpawn()
    {
        Instantiate(EnemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
