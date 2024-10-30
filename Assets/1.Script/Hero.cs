using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ObjectChangeEventStream;



public class Hero : MonoBehaviour
{
    Vector3 heropos;                //영웅의 생성 위치(클릭 위치)

    public GameObject hero_prefab1;  //영웅 레벨1 프리펩 받아오는 변수
    public GameObject hero_prefab2;  //레벨2 프리펩 받아오는 변수
    //public GameObject tile;



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Hero_Create();
    }

    void Gold_Check(BuildAble tile_build)   //돈 체크 - 돈이 충분하다면 타워 생성
    {
        
        int num = tile_build.BuildLV();
        Debug.Log("초기num"+num);
        if (App.Instance.BuyTower(1) == true)
        {
            if (num == 0)
            {
                Debug.Log("1차num" + num);
                lev_1();    //레벨 1 타워 생성
                tile_build.build();
            }
            else if (num == 1)   //레벨2 타워 생성
            {
                Debug.Log("2차num" + num);
                lev_2();
                tile_build.build();
            }
            else if(num == 2)
            {
                return;
            }

        }
      
        else
        {
            print("돈이 부족합니다");
        }

    }
    void lev_1()
    {
        Debug.Log("레벨1 타워생성");
        //Instantiate(this.hero_prefab1, heropos, Quaternion.identity);  
        GameObject temp1 = Instantiate(this.hero_prefab1, heropos, Quaternion.identity); //해당 지점 좌표에 타워(Hero) 생성(Instantiate)
        BuildAble b_mathod = FindObjectOfType<BuildAble>();
        b_mathod.build(temp1);
    }
    void lev_2() 
    {
        Debug.Log("레벨2 타워생성");
        GameObject temp2 = Instantiate(this.hero_prefab2, heropos, Quaternion.identity);
        BuildAble b_mathod = FindObjectOfType<BuildAble>();
        b_mathod.build(temp2);
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
                    BuildAble tile_build = hit.collider.GetComponent<BuildAble>();
                    Gold_Check(tile_build);
                }
            }
        }
    }

    
}
