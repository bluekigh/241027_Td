using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;


public enum Hero_State //영웅 상태
{
    FindEnemy,
    Attack
}
public class hero_prefab : MonoBehaviour
{
    Hero_State h_state;             //영웅 상태 받기


    Vector3 mypos;                  //영웅의 현재 위치(생성된 영웅)
    Animator animator;              //애니메이터 정보
    Transform closestenemy;         //가장 가까운 적군 객체를 저장할 변수

    public GameObject Enemy;        //적군의 정보를 받기
    public float Range = 3.0f;      //영웅의 사거리
    float elapseTime = 0;           //시간 값을 받는 변수
    float attackDelay = 2f;         //공격의 딜레이

    void Start()
    {
        mypos = this.transform.position;
        animator = this.GetComponentInChildren<Animator>();
        h_state = Hero_State.FindEnemy; //영웅의 첫 상태
    }

    // Update is called once per frame
    void Update()
    {
        Hero_Proc();


    }

    void Hero_Proc()
    {
        print("영웅 상태" + h_state);
        switch (h_state)
        {
            case Hero_State.FindEnemy:
                animator.SetTrigger("Idle");
                FindEnemy();
                break;
            case Hero_State.Attack:
                Attack();
                break;
        }
    }

    /*IEnumerator Attack_Delay()
    {
        //애니메이션 세팅
        Attack();
        yield return new WaitForSeconds(2f);
    }*/

    void Attack()
    {
        elapseTime += Time.deltaTime;
        if (elapseTime > attackDelay)
        {
            // Enemy.GetComponent<Enemy_Move>().Damage(1f);  데미지 주는 함수
        }
        animator.SetTrigger("Attack");
        h_state = Hero_State.FindEnemy;
    }


    void FindEnemy()  //근처의 적 검색
    {
        Collider[] hitcolliders = Physics.OverlapSphere(mypos, Range, 12);  //12번이 Enemy 라는 전제하
        print("근처 적의 수" + hitcolliders.Length);


        if (hitcolliders.Length > 0)
        {

            foreach (Collider collider in hitcolliders)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                closestenemy = collider.transform;

                if (distance <= Range)
                {
                    Attack();
                }
                else
                {
                    h_state = Hero_State.FindEnemy;
                }
            }

        }
        else
        {
            h_state = Hero_State.FindEnemy;
        }
    }
    //void searching_enemy()
    //{
    //    if (Enemy != null)
    //    {
    //        mypos = this.transform.position;
    //        float search_dist = Vector3.Distance(mypos, Enemy.transform.position);

    //        if (search_dist <= Range)   //탐지거리 이내면
    //        {
    //            h_state = Hero_State.Attack;
    //        }
    //        else
    //        {
    //            h_state = Hero_State.FindEnemy;
    //        }
    //    }
    //    else 
    //    {
    //        h_state = Hero_State.FindEnemy;
    //    }
    //}
}
/*
 * 돈 체크 - 돈이 충분하다면 타워 생성

타워의 상태(상태별 애니메이션 필요) -정지상태(FindEnemy) / 공격상태(Attack)
적 감지 -탐지 범위(range) 설정, 탐지 범위 내에 들어오면 적 타격, 타격이 끝나고 나면 일정 시간 후에 재타격, 타격 대상이 없다면 정지상태(coroutine)
타워의 공격행동 개시 => 적 데미지(Damage), 적 HP(HP)손실
타워가 레벨업을함.
공격에 속성이 들어감

*/