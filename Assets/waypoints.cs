using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypoints : MonoBehaviour
{
    public static Vector3[] points = new Vector3[] {};
    
    
    void Start()
    {
      if(App.Instance.Waypoint != null)
        {
            points = new Vector3[App.Instance.Waypoint.Length];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = App.Instance.Waypoint[i];
            }
        }  
    }
}
