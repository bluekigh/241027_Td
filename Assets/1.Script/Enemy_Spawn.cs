using System.Collections;
using UnityEngine;

public class Enemy_Spawn : MonoBehaviour
{
    public Transform[] EnemyPrefab;        //다양한 프리팹 적 배열 
    public float  WavesTime  = 2f;         //적 생성 시간 간격
    private float Countdown  = 5f;         //시작하기전 카운트

    private int stage        = 1;          //현재 스테이지
    private int EnemyToSpawn = 1;          //현재 스테이지에서 생성할 적의 수
    private int SpawnedEnemy = 0;          //현재 스테이지에서 생성된 적의 수
    private int EnemyAlive   = 0;          //살아있는 적의 수

    private bool isCountdownComplete = false;  //카운트다운 완료여부
    Coroutine Coroutine_control;
    private void Start()
    {
        Debug.Log("Enemy_Spawn 스크립트가 실행되었습니다.");
        StartStage(stage);
    }
    private void Update()
    {
        if (!isCountdownComplete)
        {
            Countdown -= Time.deltaTime;         //카운트다운 감소
            if(Countdown <= 0 )
            {
                isCountdownComplete = true;      //카운트다운 완료
                Coroutine_control =StartCoroutine(SpawnEnemies());  //적생성 시작
            }
        }
    }
    private void StartStage(int stageNumber)
    {
        //스테이지에 따라 생성할 적의 수 설정
        if (stageNumber == 1)
        {
            EnemyToSpawn = 20;
        }
        else if (stageNumber == 2)
        {
            EnemyToSpawn = 30;
        }

        SpawnedEnemy = 0;                  //생성된 적 수 초기화
        EnemyAlive   = 0;                  //살아있는 적 수 초기화
        isCountdownComplete = false;       //카운트다운 초기화
        Countdown    = 5f;                 //카운트다운 시간 재설정
        //StartCoroutine(SpawnEnemies());  //적 생성 시작
        if (Coroutine_control != null) StopCoroutine(Coroutine_control);
    }
    private IEnumerator SpawnEnemies()   
    {
        while (SpawnedEnemy < EnemyToSpawn)
        {
            EnemySpawn();                   //적 한마리 생성
            SpawnedEnemy++;                 //스폰된 적 수 늘리기             
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
        
        //모든 적이 사망했으면 다음 스테이지로 이동
        if(EnemyAlive <=0 && SpawnedEnemy >=EnemyToSpawn)
        //생성할 적의 수가 생성된 적의 수보다 많거나 같을때
        {
            stage++;
            App.Instance.changestage.Invoke();
            StartStage(stage);   //다음 스테이지 시작
            Debug.Log("다음스테이지로 넘어갑니다");
        }    
    }
}
