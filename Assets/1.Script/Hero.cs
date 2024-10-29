using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class Hero : MonoBehaviour
{
    Vector3 heropos;                //영웅의 생성 위치(클릭 위치)

    public GameObject hero_prefab;  //영웅 프리펩 받아오는 변수

    
    
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Gold_Check();
    }

    void Gold_Check()
    {
        if (App.Instance.BuyTower(0) == true)
        {
            Hero_Create();
        }
        else
        {
            print("돈이 부족합니다");
        }
        
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
                heropos = hit.transform.position; //충돌 지점을 heropos에 저장
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
   

    

}
/*
 * 돈 체크 - 돈이 충분하다면 타워 생성

타워의 상태(상태별 애니메이션 필요) -정지상태(FindEnemy) / 공격상태(Attack)
적 감지 -탐지 범위(range) 설정, 탐지 범위 내에 들어오면 적 타격, 타격이 끝나고 나면 일정 시간 후에 재타격, 타격 대상이 없다면 정지상태(coroutine)
타워의 공격행동 개시 => 적 데미지(Damage), 적 HP(HP)손실
*/