using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ObjectChangeEventStream;



public class Hero : MonoBehaviour
{
    Vector3 heropos;                //������ ���� ��ġ(Ŭ�� ��ġ)

    public GameObject hero_prefab1;  //���� ����1 ������ �޾ƿ��� ����
    public GameObject hero_prefab2;  //����2 ������ �޾ƿ��� ����
    //public GameObject tile;



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Hero_Create();
    }

    void Gold_Check(BuildAble tile_build)   //�� üũ - ���� ����ϴٸ� Ÿ�� ����
    {
        
        int num = tile_build.BuildLV();
        Debug.Log("�ʱ�num"+num);
        if (App.Instance.BuyTower(1) == true)
        {
            if (num == 0)
            {
                Debug.Log("1��num" + num);
                lev_1();    //���� 1 Ÿ�� ����
                tile_build.build();
            }
            else if (num == 1)   //����2 Ÿ�� ����
            {
                Debug.Log("2��num" + num);
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
            print("���� �����մϴ�");
        }

    }
    void lev_1()
    {
        Debug.Log("����1 Ÿ������");
        //Instantiate(this.hero_prefab1, heropos, Quaternion.identity);  
        GameObject temp1 = Instantiate(this.hero_prefab1, heropos, Quaternion.identity); //�ش� ���� ��ǥ�� Ÿ��(Hero) ����(Instantiate)
        BuildAble b_mathod = FindObjectOfType<BuildAble>();
        b_mathod.build(temp1);
    }
    void lev_2() 
    {
        Debug.Log("����2 Ÿ������");
        GameObject temp2 = Instantiate(this.hero_prefab2, heropos, Quaternion.identity);
        BuildAble b_mathod = FindObjectOfType<BuildAble>();
        b_mathod.build(temp2);
    }
    void Hero_Create()  //Hero ����
    {

        if (Input.GetMouseButtonDown(0) == true)
        //laycast ����� -�ش� ���� ��ǥ ����(print)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            
            if (Physics.Raycast(ray, out hit) == true)
            {
                heropos = hit.transform.position; //�浹 ������ heropos�� ����
                print("Ŭ�� ��ġ" + heropos);
                //����Ʈ ����
                if (hit.transform.gameObject.layer == 10)
                //layer 10���� ��ġŸ�Ϸ� ����
                {
                    BuildAble tile_build = hit.collider.GetComponent<BuildAble>();
                    Gold_Check(tile_build);
                }
            }
        }
    }

    
}
