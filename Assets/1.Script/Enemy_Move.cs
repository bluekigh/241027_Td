using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;


public class Enemy_Move : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 500f;
    private int HP = 10;

    public Vector3[] target = new Vector3[] { };
    

    private int waypointIndex = 1;

    private void Start()
    {
        //target = waypoints.points[0];
        target = App.Instance.Waypoint;
    }
    private void Update()
    {
        Vector3 dir = target[waypointIndex]- this.transform.position  ;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        if (dir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, target[waypointIndex]) <= 0.1f)
        {
            GetNextwayPoint();
        }    
    }
    private void GetNextwayPoint()
    {
        if (waypointIndex >= waypoints.points.Length -1)
        {
            Destroy(gameObject);
            return;
        }
        waypointIndex++;
        //target = App.Instance.Waypoint[waypointIndex];
    }
    
}
