using System.Collections;
using UnityEngine;

public class Enemy_Spawn : MonoBehaviour
{
    public Transform[] EnemyPrefab;        //�پ��� ������ �� �迭 
    public float  WavesTime  = 2f;         //�� ���� �ð� ����
    private float Countdown  = 5f;         //�����ϱ��� ī��Ʈ

    private int stage        = 1;          //���� ��������
    private int EnemyToSpawn = 1;          //���� ������������ ������ ���� ��
    private int SpawnedEnemy = 0;          //���� ������������ ������ ���� ��
    private int EnemyAlive   = 0;          //����ִ� ���� ��

    private bool isCountdownComplete = false;  //ī��Ʈ�ٿ� �ϷῩ��
    Coroutine Coroutine_control;
    private void Start()
    {
        Debug.Log("Enemy_Spawn ��ũ��Ʈ�� ����Ǿ����ϴ�.");
        StartStage(stage);
    }
    private void Update()
    {
        if (!isCountdownComplete)
        {
            Countdown -= Time.deltaTime;         //ī��Ʈ�ٿ� ����
            if(Countdown <= 0 )
            {
                isCountdownComplete = true;      //ī��Ʈ�ٿ� �Ϸ�
                Coroutine_control =StartCoroutine(SpawnEnemies());  //������ ����
            }
        }
    }
    private void StartStage(int stageNumber)
    {
        //���������� ���� ������ ���� �� ����
        if (stageNumber == 1)
        {
            EnemyToSpawn = 20;
        }
        else if (stageNumber == 2)
        {
            EnemyToSpawn = 30;
        }

        SpawnedEnemy = 0;                  //������ �� �� �ʱ�ȭ
        EnemyAlive   = 0;                  //����ִ� �� �� �ʱ�ȭ
        isCountdownComplete = false;       //ī��Ʈ�ٿ� �ʱ�ȭ
        Countdown    = 5f;                 //ī��Ʈ�ٿ� �ð� �缳��
        //StartCoroutine(SpawnEnemies());  //�� ���� ����
        if (Coroutine_control != null) StopCoroutine(Coroutine_control);
    }
    private IEnumerator SpawnEnemies()   
    {
        while (SpawnedEnemy < EnemyToSpawn)
        {
            EnemySpawn();                   //�� �Ѹ��� ����
            SpawnedEnemy++;                 //������ �� �� �ø���             
            EnemyAlive++;                   
            
            yield return new WaitForSeconds(WavesTime);
        }
    }
    private void EnemySpawn()
    {
        Transform selectEnemyPrefab = EnemyPrefab[Mathf.Min(stage - 1, EnemyPrefab.Length - 1)]; 
        Vector3 spawnpoint = App.Instance.Waypoint[0];
        Instantiate(selectEnemyPrefab, spawnpoint, Quaternion.Euler(0, 180, 0));
    }
    public void OnEnemyDeath()
    {
        EnemyAlive--;
        
        //��� ���� ��������� ���� ���������� �̵�
        if(EnemyAlive <=0 && SpawnedEnemy >=EnemyToSpawn)
        //������ ���� ���� ������ ���� ������ ���ų� ������
        {
            stage++;
            App.Instance.changestage.Invoke();
            StartStage(stage);   //���� �������� ����
            Debug.Log("�������������� �Ѿ�ϴ�");
        }    
    }
}
