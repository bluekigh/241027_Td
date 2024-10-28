using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawn : MonoBehaviour
{
    public Transform EnemyPrefab;          //�� ������
    public Transform spawnPoint;           //�� ���� ����
    public float  WavesTime  =5f;          //�� ���� �ð� ����

    private float Countdown  = 3f;         //�����ϱ��� ī��Ʈ
    private int WaveIndex    = 0;          //�������� ��ȣ

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
