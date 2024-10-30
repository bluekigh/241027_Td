using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;


public class Enemy_Move : MonoBehaviour
{
    public float speed = 1f;
    public float rotationSpeed = 500f;
   

    public Vector3[] target = new Vector3[] { };
    

    private int waypointIndex = 1;

    private void Start()
    {
        target = App.Instance.Waypoint;
    }
    private void Update()
    {
        if (waypointIndex >= target.Length) return;

        Vector3 dir = target[waypointIndex]- this.transform.position  ; 
        //������
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World); 
        //�̵�ó��
        if (dir != Vector3.zero)
            //ȸ��ó��
        {
            Quaternion toRotation = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, target[waypointIndex]) <= 0.1f)
            //���� ��������Ʈ�� ���� �����ߴ��� üũ
        {
            GetNextwayPoint();  
        }    
    }
    private void GetNextwayPoint()
    {
        if (waypointIndex >= App.Instance.Waypoint.Length -1)
        {
            App.Instance.ReachDestination(1); //�������Լ�
            Destroy(gameObject);
            return;
        }
        waypointIndex++;
        //target = App.Instance.Waypoint[waypointIndex];
    }
    
}
