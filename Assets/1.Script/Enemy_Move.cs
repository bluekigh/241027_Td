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
        //방향계산
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World); 
        //이동처리
        if (dir != Vector3.zero)
            //회전처리
        {
            Quaternion toRotation = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, target[waypointIndex]) <= 0.1f)
            //현재 웨이포인트에 거의 도착했는지 체크
        {
            GetNextwayPoint();  
        }    
    }
    private void GetNextwayPoint()
    {
        if (waypointIndex >= App.Instance.Waypoint.Length -1)
        {
            App.Instance.ReachDestination(1); //데미지함수
            Destroy(gameObject);
            return;
        }
        waypointIndex++;
        //target = App.Instance.Waypoint[waypointIndex];
    }
    
}
