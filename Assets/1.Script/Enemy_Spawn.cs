using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawn : MonoBehaviour
{
    public Transform EnemyPrefab;          //�� ������
    public float  WavesTime  =5f;          //�� ���� �ð� ����
    private float Countdown  = 2f;         //�����ϱ��� ī��Ʈ
    private int WaveNumber   = 1;          //�������� ��ȣ

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
