using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Damage : MonoBehaviour
{
    private float max_HP = 10f;    //�ִ�ü��
    private float cur_HP;          //����ü��
    private Animator animator;     //�ִϸ��̼� ������Ʈ
    private Enemy_Spawn spawner;   

    private bool isDead = false;   //������� Ȯ��
    private Enemy_Move enemy_Move; //���̵� ��ũ��Ʈ

    public Slider HP_Slider;       //ü�¹� UI

    private void Start()
    {
        cur_HP = max_HP;
        spawner = FindObjectOfType<Enemy_Spawn>();
        animator = GetComponent<Animator>();
        enemy_Move = GetComponent<Enemy_Move>();
        UpdateHpUI();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        Debug.Log("���� �������� �Ծ����ϴ�. ����ü��:" + cur_HP);
        cur_HP -= damage;
        UpdateHpUI();
        animator.SetTrigger("HIt"); //�ǰ� �ִϸ��̼� Ʈ����

        if (cur_HP <= 0)
        {
            enemy_Move.enabled = false;   //�̵���ũ��Ʈ ��Ȱ��ȭ
            Die();
        }
    }
   
    private void Die()
    {
        spawner.OnEnemyDeath();
        isDead = true;
        //���ó��
        animator.SetTrigger("isDead");
        //�ִϸ������� isDeadƮ���� Ȱ��ȭ
        StartCoroutine(DestroyAfterAnimation());
        //�����ð��� ������Ʈ �ı�
    }
    private void UpdateHpUI()
    {
        if (HP_Slider != null)
        {
            HP_Slider.value = cur_HP;
            //ü�º����� �����̴� ����
        }
    }
    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        //��� �ִϸ��̼��� ���̸�ŭ ���
        Destroy(gameObject);
        //�� ������Ʈ �ı�
        App.Instance.Gold++;
        //�ý��ۿ� ��� �߰�
    }
}
