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
        Hero_Create();
    }

    void Gold_Check()   //돈 체크 - 돈이 충분하다면 타워 생성
    {

        if (App.Instance.BuyTower(1) == true)
        {
            Instantiate(this.hero_prefab, heropos, Quaternion.identity);  //해당 지점 좌표에 타워(Hero) 생성(Instantiate)
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
                    Gold_Check();
                }
            }
        }
    }

    void Level_Up() //타워의 레벨 업
    {
        //돈 있는지 확인
        //없으면 불가
        //레벨 업 하면 새로운 프리펩 불러오기
    }
}
