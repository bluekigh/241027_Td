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
    public LayerMask layermask;      //Ÿ�� ���̾� �޾ƿ���
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

    void Gold_Check(BuildAble tile_build)   //�� üũ - ���� ����ϴٸ� Ÿ�� ����
    {
        int num = tile_build.BuildLV();
        if (num == 0)
        {
            
            if (App.Instance.BuyTower(1) == true) //Ÿ�� ��� �Ҹ�
            {
                lev_1(tile_build); //���� 1 Ÿ�� ����
            }


        }
        else if (num == 1)   //����2 Ÿ�� ����
        {
           
            if (App.Instance.BuyTower(1) == true)
            {
                lev_2(tile_build);
            }

        }
        else if (num == 2)
        {
            Debug.Log("�ش� ������ ��ġ�� �� �����ϴ�.");
            return;
        }
    }
    void lev_1(BuildAble tile_build)
    {
        Debug.Log("����1 Ÿ������");
        //Instantiate(this.hero_prefab1, heropos, Quaternion.identity);  
        GameObject temp1 = Instantiate(this.hero_prefab1, heropos, Quaternion.identity); //�ش� ���� ��ǥ�� Ÿ��(Hero) ����(Instantiate)
        //BuildAble b_mathod = FindObjectOfType<BuildAble>();
        tile_build.build(temp1);
        Towers.Add(temp1);
    }
    void lev_2(BuildAble tile_build)
    {
        Debug.Log("����2 Ÿ������");
        GameObject temp2 = Instantiate(this.hero_prefab2, heropos, Quaternion.identity);
        //BuildAble b_mathod = FindObjectOfType<BuildAble>();
        tile_build.build(temp2);
        Towers.Add(temp2);
    }
    void Hero_Create()  //Hero ����
    {

        if (Input.GetMouseButtonDown(0) == true)
        //laycast ����� -�ش� ���� ��ǥ ����(print)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();


            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask) == true) //Ư�� ���̾��� ������Ʈ�� ã�� �������� ����
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
    public void Destory_All()
    {
        foreach (GameObject Tower in Towers)
        {
            Destroy(Tower);
        }
    }

}
