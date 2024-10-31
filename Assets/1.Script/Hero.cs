using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    Vector3 heropos;                                      //������ ���� ��ġ(Ŭ�� ��ġ)

    public GameObject hero_prefab1;                       //���� ����1 ������ �޾ƿ��� ����
    public GameObject hero_prefab2;                       //����2 ������ �޾ƿ��� ����

    public LayerMask layermask;                           //Ÿ�� ���̾� �޾ƿ���

    List<GameObject> Towers = new List<GameObject>();     //������ Ÿ�� �������� ������ ����Ʈ

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
                lev(tile_build, this.hero_prefab1); //���� 1 Ÿ�� ����
            }
        }
        else if (num == 1)   //����2 Ÿ�� ����
        {
            if (App.Instance.BuyTower(1) == true)
            {
                lev(tile_build, this.hero_prefab2);
            }
        }
        else if (num == 2)
        {
            return;
        }
    }
    
    void lev(BuildAble tile_build, GameObject heroprefab)
    {
        GameObject temp = Instantiate(heroprefab, heropos, Quaternion.identity);
        tile_build.build(temp);
        Towers.Add(temp);
    }
    void Hero_Create()  //Hero ����
    {
        if (Input.GetMouseButtonDown(0) == true)        //laycast ����� -�ش� ���� ��ǥ ����(print)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask) == true) //Ư�� ���̾��� ������Ʈ�� ã�� �������� ����
            {
                heropos = hit.transform.position;           //�浹 ������ heropos�� ����
                
                if (hit.transform.gameObject.layer == 10)   //layer 10���� ��ġŸ�Ϸ� ����
                {
                    BuildAble tile_build = hit.collider.GetComponent<BuildAble>();
                    Gold_Check(tile_build);
                }
            }
        }
    }

    void Hero_Create_Mobile() //����� �� Ÿ�� ��ġ �Լ�
    {
        if (Input.touchCount > 0)        //laycast ����� -�ش� ���� ��ǥ ����(print)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit = new RaycastHit();

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask) == true) //Ư�� ���̾��� ������Ʈ�� ã�� �������� ����
                {
                    heropos = hit.transform.position;           //�浹 ������ heropos�� ����
                                                               
                    if (hit.transform.gameObject.layer == 10)   //layer 10���� ��ġŸ�Ϸ� ����`
                    {
                        BuildAble tile_build = hit.collider.GetComponent<BuildAble>();
                        Gold_Check(tile_build);
                    }
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
