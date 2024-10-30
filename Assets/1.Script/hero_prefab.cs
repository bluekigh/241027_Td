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

    public LayerMask layerMask;     //���̾ �޴� ����
    //public GameObject Enemy;        //������ ������ �ޱ�
    float lefttime = 0f;

    public float Range = 1.0f;      //������ ��Ÿ�
    public float attack_delay = 0.1f; //������ ������
    //float elapseTime = 0;         //�ð� ���� �޴� ����
    //float attackDelay = 2f;       //������ ������

    void Start()
    {
        mypos = this.transform.position;
        animator = this.GetComponentInChildren<Animator>();
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
        print("���� ����" + h_state);
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
        
        lefttime += Time.deltaTime;
        
        if(lefttime >= attack_delay)
        {

            print(" attack.1");
            animator.SetTrigger("Attack");
            closestenemy.GetComponent<Enemy_Damage>().TakeDamage(1);  //������ �ִ� �Լ�
            lefttime = 0f; 
            
        }
        h_state = Hero_State.FindEnemy;
    }
    //IEnumerator Attack_motion() //Ÿ���� ������ ���� ���� �ð� �Ŀ� ��Ÿ��
    //{
       
       
    //    yield return new WaitForSeconds(1f);
        
    //}


    void FindEnemy()  //��ó�� �� �˻�Ž��
    {

        Collider[] hitcolliders = Physics.OverlapSphere(mypos, Range, layerMask);  //12���� Enemy ��� ������  //����(range) ����
        print("��ó ���� ��" + hitcolliders.Length);


        if (hitcolliders.Length > 0) //Ž�� ���� ���� ������
        {

            foreach (Collider collider in hitcolliders)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
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