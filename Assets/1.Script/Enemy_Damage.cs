using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Damage : MonoBehaviour
{
    private float max_HP = 10f;    //최대체력
    private float cur_HP;          //현재체력
    private Animator animator;     //애니메이션 컴포넌트
    private Enemy_Spawn spawner;   

    private bool isDead = false;   //사망여부 확인
    private Enemy_Move enemy_Move; //적이동 스크립트

    public Slider HP_Slider;       //체력바 UI

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
        Debug.Log("적이 데미지를 입었습니다. 남은체력:" + cur_HP);
        cur_HP -= damage;
        UpdateHpUI();
        animator.SetTrigger("HIt"); //피격 애니메이션 트리거

        if (cur_HP <= 0)
        {
            enemy_Move.enabled = false;   //이동스크립트 비활성화
            Die();
        }
    }
   
    private void Die()
    {
        spawner.OnEnemyDeath();
        isDead = true;
        //사망처리
        animator.SetTrigger("isDead");
        //애니메이터의 isDead트리거 활성화
        StartCoroutine(DestroyAfterAnimation());
        //일정시간후 오브젝트 파괴
    }
    private void UpdateHpUI()
    {
        if (HP_Slider != null)
        {
            HP_Slider.value = cur_HP;
            //체력비율로 슬라이더 조정
        }
    }
    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        //사망 애니메이션의 길이만큼 대기
        Destroy(gameObject);
        //적 오브젝트 파괴
        App.Instance.Gold++;
        //시스템에 골드 추가
    }
}
