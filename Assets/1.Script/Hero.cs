using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum Hero_State //영웅 상태
{
    FindEnemy,
    Attack
}
public class Hero : MonoBehaviour
{
    Hero_State h_state;             //영웅 상태 받기

    Transform Enemy;                //적군의 정보를 받기

    Vector3 heropos;                //영웅의 생성 위치(클릭 위치)
    Vector3 mypos;                  //영웅의 현재 위치(생성된 영웅)
    
    public float Range = 3.0f;      //영웅의 사거리

    public GameObject hero_prefab;  //영웅 프리펩 받아오는 변수

    float elapseTime = 0;           //시간 값을 받는 변수
    float attackDelay = 2f;         //공격의 딜레이

    void Start()
    {
        h_state = Hero_State.FindEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        Hero_Create();
    }

    void Hero_Create()  //Hero 생성
    {
        if (Input.GetMouseButtonDown(0) == true)
        //laycast 만들기 -해당 지점 좌표 측정(print)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();


            if (Physics.Raycast(ray, out hit) == true)
            {
                heropos = hit.point; //충돌 지점을 heropos에 저장
                print("클릭 위치" + heropos);
                //이펙트 설정
                if (hit.transform.gameObject.layer == 10)
                //layer 10번을 설치타일로 설정
                {
                    Instantiate(this.hero_prefab, heropos, Quaternion.identity);  //해당 지점 좌표에 타워(Hero) 생성(Instantiate)
                }
            }
        }
    }

    void Hero_Proc()
    {
        print("영웅 상태" + h_state);
        switch (h_state)
        {
            case Hero_State.FindEnemy:
                FindEnemy();
                break;
            case Hero_State.Attack:
                Attack();
                break;
        }



    }

    void FindEnemy()
    {
        if (Enemy != null)
        {
            mypos =this.transform.position;
            float search_dist = Vector3.Distance(mypos,Enemy.transform.position);

            if (search_dist <= Range)   //탐지거리 이내면
            {
                h_state = Hero_State.Attack;
            }
        }
    }

    void Attack()
    {
        mypos = this.transform.position;
        float search_dist = Vector3.Distance(mypos, Enemy.transform.position);

        if (search_dist <= Range)
        {
            elapseTime += Time.deltaTime;
            if (elapseTime > attackDelay)
            {
                Attack_func();
            }
        }
        else
        {
            h_state= Hero_State.FindEnemy;
        }

    }
    void Attack_func()
    {
        //Enemy.GetComponent<Enemy>().Damage(1f);
    }

}
/*
 * 돈 체크 - 돈이 충분하다면 타워 생성

타워의 상태(상태별 애니메이션 필요) -정지상태(FindEnemy) / 공격상태(Attack)
적 감지 -탐지 범위(range) 설정, 탐지 범위 내에 들어오면 적 타격, 타격이 끝나고 나면 일정 시간 후에 재타격, 타격 대상이 없다면 정지상태(coroutine)
타워의 공격행동 개시 => 적 데미지(Damage), 적 HP(HP)손실
*/