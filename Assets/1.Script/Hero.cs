using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum Hero_State //���� ����
{
    FindEnemy,
    Attack
}
public class Hero : MonoBehaviour
{
    Hero_State h_state;             //���� ���� �ޱ�

    Transform Enemy;                //������ ������ �ޱ�

    Vector3 heropos;                //������ ���� ��ġ(Ŭ�� ��ġ)
    Vector3 mypos;                  //������ ���� ��ġ(������ ����)
    
    public float Range = 3.0f;      //������ ��Ÿ�

    public GameObject hero_prefab;  //���� ������ �޾ƿ��� ����

    float elapseTime = 0;           //�ð� ���� �޴� ����
    float attackDelay = 2f;         //������ ������

    void Start()
    {
        h_state = Hero_State.FindEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        Hero_Create();
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
                heropos = hit.point; //�浹 ������ heropos�� ����
                print("Ŭ�� ��ġ" + heropos);
                //����Ʈ ����
                if (hit.transform.gameObject.layer == 10)
                //layer 10���� ��ġŸ�Ϸ� ����
                {
                    Instantiate(this.hero_prefab, heropos, Quaternion.identity);  //�ش� ���� ��ǥ�� Ÿ��(Hero) ����(Instantiate)
                }
            }
        }
    }

    void Hero_Proc()
    {
        print("���� ����" + h_state);
        switch (h_state)
        {
            case Hero_State.FindEnemy:
                FindEnemy();
                break;
            case Hero_State.Attack:
                Attack();
                break;
        }



    }

    void FindEnemy()
    {
        if (Enemy != null)
        {
            mypos =this.transform.position;
            float search_dist = Vector3.Distance(mypos,Enemy.transform.position);

            if (search_dist <= Range)   //Ž���Ÿ� �̳���
            {
                h_state = Hero_State.Attack;
            }
        }
    }

    void Attack()
    {
        mypos = this.transform.position;
        float search_dist = Vector3.Distance(mypos, Enemy.transform.position);

        if (search_dist <= Range)
        {
            elapseTime += Time.deltaTime;
            if (elapseTime > attackDelay)
            {
                Attack_func();
            }
        }
        else
        {
            h_state= Hero_State.FindEnemy;
        }

    }
    void Attack_func()
    {
        //Enemy.GetComponent<Enemy>().Damage(1f);
    }

}
/*
 * �� üũ - ���� ����ϴٸ� Ÿ�� ����

Ÿ���� ����(���º� �ִϸ��̼� �ʿ�) -��������(FindEnemy) / ���ݻ���(Attack)
�� ���� -Ž�� ����(range) ����, Ž�� ���� ���� ������ �� Ÿ��, Ÿ���� ������ ���� ���� �ð� �Ŀ� ��Ÿ��, Ÿ�� ����� ���ٸ� ��������(coroutine)
Ÿ���� �����ൿ ���� => �� ������(Damage), �� HP(HP)�ս�
*/