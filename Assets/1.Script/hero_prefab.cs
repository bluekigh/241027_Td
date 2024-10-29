using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;


public enum Hero_State //���� ����
{
    FindEnemy,
    Attack
}
public class hero_prefab : MonoBehaviour
{
    Hero_State h_state;             //���� ���� �ޱ�


    Vector3 mypos;                  //������ ���� ��ġ(������ ����)
    Animator animator;              //�ִϸ����� ����
    Transform closestenemy;         //���� ����� ���� ��ü�� ������ ����

    public GameObject Enemy;        //������ ������ �ޱ�
    public float Range = 3.0f;      //������ ��Ÿ�
    float elapseTime = 0;           //�ð� ���� �޴� ����
    float attackDelay = 2f;         //������ ������

    void Start()
    {
        mypos = this.transform.position;
        animator = this.GetComponentInChildren<Animator>();
        h_state = Hero_State.FindEnemy; //������ ù ����
    }

    // Update is called once per frame
    void Update()
    {
        Hero_Proc();


    }

    void Hero_Proc()
    {
        print("���� ����" + h_state);
        switch (h_state)
        {
            case Hero_State.FindEnemy:
                animator.SetTrigger("Idle");
                FindEnemy();
                break;
            case Hero_State.Attack:
                Attack();
                break;
        }
    }

    /*IEnumerator Attack_Delay()
    {
        //�ִϸ��̼� ����
        Attack();
        yield return new WaitForSeconds(2f);
    }*/

    void Attack()
    {
        elapseTime += Time.deltaTime;
        if (elapseTime > attackDelay)
        {
            // Enemy.GetComponent<Enemy_Move>().Damage(1f);  ������ �ִ� �Լ�
        }
        animator.SetTrigger("Attack");
        h_state = Hero_State.FindEnemy;
    }


    void FindEnemy()  //��ó�� �� �˻�
    {
        Collider[] hitcolliders = Physics.OverlapSphere(mypos, Range, 12);  //12���� Enemy ��� ������
        print("��ó ���� ��" + hitcolliders.Length);


        if (hitcolliders.Length > 0)
        {

            foreach (Collider collider in hitcolliders)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                closestenemy = collider.transform;

                if (distance <= Range)
                {
                    Attack();
                }
                else
                {
                    h_state = Hero_State.FindEnemy;
                }
            }

        }
        else
        {
            h_state = Hero_State.FindEnemy;
        }
    }
    //void searching_enemy()
    //{
    //    if (Enemy != null)
    //    {
    //        mypos = this.transform.position;
    //        float search_dist = Vector3.Distance(mypos, Enemy.transform.position);

    //        if (search_dist <= Range)   //Ž���Ÿ� �̳���
    //        {
    //            h_state = Hero_State.Attack;
    //        }
    //        else
    //        {
    //            h_state = Hero_State.FindEnemy;
    //        }
    //    }
    //    else 
    //    {
    //        h_state = Hero_State.FindEnemy;
    //    }
    //}
}
/*
 * �� üũ - ���� ����ϴٸ� Ÿ�� ����

Ÿ���� ����(���º� �ִϸ��̼� �ʿ�) -��������(FindEnemy) / ���ݻ���(Attack)
�� ���� -Ž�� ����(range) ����, Ž�� ���� ���� ������ �� Ÿ��, Ÿ���� ������ ���� ���� �ð� �Ŀ� ��Ÿ��, Ÿ�� ����� ���ٸ� ��������(coroutine)
Ÿ���� �����ൿ ���� => �� ������(Damage), �� HP(HP)�ս�
Ÿ���� ����������.
���ݿ� �Ӽ��� ��

*/