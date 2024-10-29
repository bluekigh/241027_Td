using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypoints : MonoBehaviour
{
    public static Vector3[] points = new Vector3[] {};
    
    
    void Start()
    {
        //points = new Transform[transform.childCount];
        //for (int i = 0; i < points.Length; i++)
        //{
        //    points[i] = transform.GetChild(i);
        //}

        points = new Vector3[App.Instance.Waypoint.Length];
        for (int i = 0; i < points.Length; i++)
        {
           points[i]= App.Instance.Waypoint[i];
        }
    }
}
