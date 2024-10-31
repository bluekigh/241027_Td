using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;


public enum Hero_State //영웅 상태
{
    FindEnemy,
    Attack
}
public class hero_prefab : MonoBehaviour
{
    Hero_State h_state;                  //영웅 상태 받기

    //public AudioSource locationsound;  //타워 설치 오디오 받는 변수
    public AudioSource AttackSound;      //오디오 소스 받는 변수
    public AudioClip Hitsound;           //공격 오디오 클립 받는 변수
    public GameObject Shootprefab;       //타격 이펙트

    Vector3 mypos;                       //영웅의 현재 위치(생성된 영웅)
    Vector3 enemyvector;                 //가장 가까운 적의 vector값을 받는 변수
    Animator animator;                   //애니메이터 정보
    Transform closestenemy;              //가장 가까운 적군 객체를 저장할 변수

    Coroutine a_coroutine;               //공격 코루틴

    public LayerMask layerMask;          //레이어를 받는 변수

    float e_distance;                    //적군까지의 거리를 받는 변수
    public float Range = 1.0f;           //영웅의 사거리
    public float attack_delay;           //공격의 딜레이
    public int attack_damage = 1;        //영웅의 공격력

    void Start()
    {
        mypos = this.transform.position;
        animator = this.GetComponentInChildren<Animator>();
        animator.SetTrigger("Start");
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
        switch (h_state)
        {
            case Hero_State.FindEnemy: //정지상태(FindEnemy)
                animator.SetTrigger("Idle");
                FindEnemy();
                break;
            case Hero_State.Attack: //공격상태(Attack)
                if (a_coroutine == null)
                {
                    a_coroutine = StartCoroutine(Attack_routine());
                }
                break;
        }
    }

   
    IEnumerator Attack_routine() //타격이 끝나고 나면 일정 시간 후에 재타격
    {
        First_Attack();
        yield return new WaitForSeconds(attack_delay);
        Debug.Log("공격");
        h_state = Hero_State.FindEnemy;
        a_coroutine = null;
    }
    void First_Attack()
    {
        if (closestenemy != null)
        {
            Instantiate(Shootprefab, enemyvector, Quaternion.identity);
            //AttackSound.Play();
            AttackSound.PlayOneShot(Hitsound);
            animator.SetTrigger("Attack");
            Invoke("Add_Damage", 0.3f);

        }
    }
    void Add_Damage()
    {
        if (closestenemy != null)
        {
            closestenemy.GetComponent<Enemy_Damage>().TakeDamage(attack_damage);

        }
    }

    void FindEnemy()  //근처의 적 검색탐지
    {
        closestenemy = null;
        Collider[] hitcolliders = Physics.OverlapSphere(mypos, Range, layerMask);  //12번이 Enemy 라는 전제하  //범위(range) 설정

        if (hitcolliders.Length > 0) //탐지 범위 내에 들어오면
        {
            foreach (Collider collider in hitcolliders)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                e_distance = distance;
                closestenemy = collider.transform;  //가장 가까운 적 객체 설정
                enemyvector = collider.transform.position;  //가장 가까운 적의 vector값

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
}
