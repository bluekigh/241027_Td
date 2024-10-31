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

    //public AudioSource locationsound;  //Ÿ�� ��ġ ����� �޴� ����
    public AudioSource AttackSound;      //����� �ҽ� �޴� ����
    public AudioClip Hitsound;           //���� ����� Ŭ�� �޴� ����
    public GameObject Shootprefab;       //Ÿ�� ����Ʈ

    Vector3 mypos;                       //������ ���� ��ġ(������ ����)
    Vector3 enemyvector;                 //���� ����� ���� vector���� �޴� ����
    Animator animator;                   //�ִϸ����� ����
    Transform closestenemy;              //���� ����� ���� ��ü�� ������ ����

    Coroutine a_coroutine;               //���� �ڷ�ƾ

    public LayerMask layerMask;          //���̾ �޴� ����

    float e_distance;                    //���������� �Ÿ��� �޴� ����
    public float Range = 1.0f;           //������ ��Ÿ�
    public float attack_delay;           //������ ������
    public int attack_damage = 1;        //������ ���ݷ�

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
        switch (h_state)
        {
            case Hero_State.FindEnemy: //��������(FindEnemy)
                animator.SetTrigger("Idle");
                FindEnemy();
                break;
            case Hero_State.Attack: //���ݻ���(Attack)
                if (a_coroutine == null)
                {
                    a_coroutine = StartCoroutine(Attack_routine());
                }
                break;
        }
    }

   
    IEnumerator Attack_routine() //Ÿ���� ������ ���� ���� �ð� �Ŀ� ��Ÿ��
    {
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
            Instantiate(Shootprefab, enemyvector, Quaternion.identity);
            //AttackSound.Play();
            AttackSound.PlayOneShot(Hitsound);
            animator.SetTrigger("Attack");
            Invoke("Add_Damage", 0.3f);

        }
    }
    void Add_Damage()
    {
        if (closestenemy != null)
        {
            closestenemy.GetComponent<Enemy_Damage>().TakeDamage(attack_damage);

        }
    }

    void FindEnemy()  //��ó�� �� �˻�Ž��
    {
        closestenemy = null;
        Collider[] hitcolliders = Physics.OverlapSphere(mypos, Range, layerMask);  //12���� Enemy ��� ������  //����(range) ����

        if (hitcolliders.Length > 0) //Ž�� ���� ���� ������
        {
            foreach (Collider collider in hitcolliders)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                e_distance = distance;
                closestenemy = collider.transform;  //���� ����� �� ��ü ����
                enemyvector = collider.transform.position;  //���� ����� ���� vector��

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
}
