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
    public LayerMask layermask;      //타일 레이어 받아오기
    List<GameObject> Towers = new List<GameObject>();

    void Start()
    {
        App.Instance.changestage += Destory_All;
    }

    private void OnDisable()
    {
        App.Instance.changestage -= Destory_All;
    }

    // Update is called once per frame
    void Update()
    {
        Hero_Create();
    }

    void Gold_Check(BuildAble tile_build)   //돈 체크 - 돈이 충분하다면 타워 생성
    {
        int num = tile_build.BuildLV();
        if (num == 0)
        {
            
            if (App.Instance.BuyTower(1) == true) //타워 비용 소모
            {
                lev_1(tile_build); //레벨 1 타워 생성
            }


        }
        else if (num == 1)   //레벨2 타워 생성
        {
           
            if (App.Instance.BuyTower(1) == true)
            {
                lev_2(tile_build);
            }

        }
        else if (num == 2)
        {
            Debug.Log("해당 지역에 설치할 수 없습니다.");
            return;
        }
    }
    void lev_1(BuildAble tile_build)
    {
        Debug.Log("레벨1 타워생성");
        //Instantiate(this.hero_prefab1, heropos, Quaternion.identity);  
        GameObject temp1 = Instantiate(this.hero_prefab1, heropos, Quaternion.identity); //해당 지점 좌표에 타워(Hero) 생성(Instantiate)
        //BuildAble b_mathod = FindObjectOfType<BuildAble>();
        tile_build.build(temp1);
        Towers.Add(temp1);
    }
    void lev_2(BuildAble tile_build)
    {
        Debug.Log("레벨2 타워생성");
        GameObject temp2 = Instantiate(this.hero_prefab2, heropos, Quaternion.identity);
        //BuildAble b_mathod = FindObjectOfType<BuildAble>();
        tile_build.build(temp2);
        Towers.Add(temp2);
    }
    void Hero_Create()  //Hero 생성
    {

        if (Input.GetMouseButtonDown(0) == true)
        //laycast 만들기 -해당 지점 좌표 측정(print)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();


            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask) == true) //특정 레이어의 오브젝트만 찾고 나머지는 관통
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
    public void Destory_All()
    {
        foreach (GameObject Tower in Towers)
        {
            Destroy(Tower);
        }
    }

}
