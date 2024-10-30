using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;


public enum Hero_State //���� ����
{
    FindEnemy,
    Attack
}
public class hero_prefab : MonoBehaviour
{
    Hero_State h_state;                  //���� ���� �ޱ�


    Vector3 mypos;                       //������ ���� ��ġ(������ ����)
    Animator animator;                   //�ִϸ����� ����
    Transform closestenemy;              //���� ����� ���� ��ü�� ������ ����

    public LayerMask layerMask;          //���̾ �޴� ����
    //public GameObject Enemy;           //������ ������ �ޱ�
    //float lefttime = 0f;               //���� �������� ���� �ð�
    float e_distance;                    //���������� �Ÿ��� �޴� ����

    public float Range = 1.0f;           //������ ��Ÿ�
    public float attack_delay ;    //������ ������
    public int attack_damage = 1;        //������ ���ݷ�
    //float elapseTime = 0;              //�ð� ���� �޴� ����
    //float attackDelay = 2f;            //������ ������

    Coroutine a_coroutine;                      //���� �ڷ�ƾ
    void Start()
    {
        mypos = this.transform.position;
        animator = this.GetComponentInChildren<Animator>();
        animator.SetTrigger("Start");
        h_state = Hero_State.FindEnemy; //������ ù ����
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(closestenemy); //���� �������� �Ĵٺ���
        Hero_Proc();
    }

    void Hero_Proc()    //Ÿ���� ����(���º� �ִϸ��̼� �ʿ�)
    {
        //print("���� ����" + h_state);
        switch (h_state)
        {
            case Hero_State.FindEnemy: //��������(FindEnemy)
                animator.SetTrigger("Idle");
                FindEnemy();
                break;
            case Hero_State.Attack: //���ݻ���(Attack)
                Attack();
                break;
        }
    }

    

    void Attack()
    {
        //print(" attack.0");
        //StartCoroutine(Attack_motion());

        //lefttime += Time.deltaTime;

        //if(lefttime >= attack_delay)
        //{
        //    animator.SetTrigger("Attack");
        //    closestenemy.GetComponent<Enemy_Damage>().TakeDamage(attack_damage);  //������ �ִ� �Լ�
        //    lefttime = 0f; 
        //}

        if (a_coroutine == null) { a_coroutine = StartCoroutine(Attack_routine()); }
        
       
        
        
    }
    IEnumerator Attack_routine() //Ÿ���� ������ ���� ���� �ð� �Ŀ� ��Ÿ��
    {

        //First_Attack();
        //while (e_distance <= Range)
        
            First_Attack();
            yield return new WaitForSeconds(attack_delay);
            Debug.Log("����");
            h_state = Hero_State.FindEnemy;
            a_coroutine = null;
        
    }
    void First_Attack()
    {
        if (closestenemy != null) 
        {
            closestenemy.GetComponent<Enemy_Damage>().TakeDamage(attack_damage);
            animator.SetTrigger("Attack");
        }
        
    }


    void FindEnemy()  //��ó�� �� �˻�Ž��
    {
        closestenemy =null;
        Collider[] hitcolliders = Physics.OverlapSphere(mypos, Range, layerMask);  //12���� Enemy ��� ������  //����(range) ����
        //print("��ó ���� ��" + hitcolliders.Length);


        if (hitcolliders.Length > 0) //Ž�� ���� ���� ������
        {

            foreach (Collider collider in hitcolliders)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                e_distance = distance;
                closestenemy = collider.transform;  //���� ����� �� ����
                
                if (distance <= Range)  //�� Ÿ��
                {
                    h_state = Hero_State.Attack;
                }
                else
                {
                    h_state = Hero_State.FindEnemy; //Ÿ�� ����� ���ٸ� ��������(coroutine)
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
 * 






���ݿ� �Ӽ��� �� - ������ Ÿ������ ������ �ɷ�ġ�� ��ũ���带 ���ο� �����鿡 ����

*/