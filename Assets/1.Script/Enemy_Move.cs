using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Move : MonoBehaviour
{
    public float speed = 10f;

    private int HP = 10;
    private Transform target;
    private int wavepointIndex = 0;

    void Start()
    {
        target = waypoints.points[0];

    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }
    private void GetNextWaypoint()
    {
        if(wavepointIndex >= waypoints.points.Length -1)
        {
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        target = waypoints.points[wavepointIndex];
    }
    
}
