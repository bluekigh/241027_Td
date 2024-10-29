using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawn : MonoBehaviour
{
    public Transform EnemyPrefab;          //�� ������
    public float  WavesTime  =5f;          //�� ���� �ð� ����
    private float Countdown  = 2f;         //�����ϱ��� ī��Ʈ
    private int WaveNumber   = 1;          //�� ������

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
