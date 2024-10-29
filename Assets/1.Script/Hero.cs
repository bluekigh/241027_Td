using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class Hero : MonoBehaviour
{
    Vector3 heropos;                //������ ���� ��ġ(Ŭ�� ��ġ)

    public GameObject hero_prefab;  //���� ������ �޾ƿ��� ����



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Hero_Create();
    }

    void Gold_Check()   //�� üũ - ���� ����ϴٸ� Ÿ�� ����
    {

        if (App.Instance.BuyTower(1) == true)
        {
            Instantiate(this.hero_prefab, heropos, Quaternion.identity);  //�ش� ���� ��ǥ�� Ÿ��(Hero) ����(Instantiate)
        }
        else
        {
            print("���� �����մϴ�");
        }

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
                    Gold_Check();
                }
            }
        }
    }

    void Level_Up() //Ÿ���� ���� ��
    {
        //�� �ִ��� Ȯ��
        //������ �Ұ�
        //���� �� �ϸ� ���ο� ������ �ҷ�����
    }
}
