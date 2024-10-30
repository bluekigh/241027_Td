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

    public LayerMask layerMask;     //레이어를 받는 변수
    //public GameObject Enemy;        //적군의 정보를 받기
    float lefttime = 0f;

    public float Range = 1.0f;      //영웅의 사거리
    public float attack_delay = 0.1f; //공격의 딜레이
    //float elapseTime = 0;         //시간 값을 받는 변수
    //float attackDelay = 2f;       //공격의 딜레이

    void Start()
    {
        mypos = this.transform.position;
        animator = this.GetComponentInChildren<Animator>();
        h_state = Hero_State.FindEnemy; //영웅의 첫 상태
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(closestenemy); //공격 방향으로 쳐다보기
        Hero_Proc();
    }

    void Hero_Proc()    //타워의 상태(상태별 애니메이션 필요)
    {
        print("영웅 상태" + h_state);
        switch (h_state)
        {
            case Hero_State.FindEnemy: //정지상태(FindEnemy)
                animator.SetTrigger("Idle");
                FindEnemy();
                break;
            case Hero_State.Attack: //공격상태(Attack)
                Attack();
                break;
        }
    }

    

    void Attack()
    {
        //print(" attack.0");
        //StartCoroutine(Attack_motion());
        
        lefttime += Time.deltaTime;
        
        if(lefttime >= attack_delay)
        {

            print(" attack.1");
            animator.SetTrigger("Attack");
            closestenemy.GetComponent<Enemy_Damage>().TakeDamage(1);  //데미지 주는 함수
            lefttime = 0f; 
            
        }
        h_state = Hero_State.FindEnemy;
    }
    //IEnumerator Attack_motion() //타격이 끝나고 나면 일정 시간 후에 재타격
    //{
       
       
    //    yield return new WaitForSeconds(1f);
        
    //}


    void FindEnemy()  //근처의 적 검색탐지
    {

        Collider[] hitcolliders = Physics.OverlapSphere(mypos, Range, layerMask);  //12번이 Enemy 라는 전제하  //범위(range) 설정
        print("근처 적의 수" + hitcolliders.Length);


        if (hitcolliders.Length > 0) //탐지 범위 내에 들어오면
        {

            foreach (Collider collider in hitcolliders)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                closestenemy = collider.transform;  //가장 가까운 적 설정
                
                if (distance <= Range)  //적 타격
                {
                    h_state = Hero_State.Attack;
                }
                else
                {
                    h_state = Hero_State.FindEnemy; //타격 대상이 없다면 정지상태(coroutine)
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
 * 






공격에 속성이 들어감 - 기존의 타워보다 성장한 능력치의 스크립드를 새로운 프리펩에 적용

*/