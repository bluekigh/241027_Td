using UnityEngine;

public class StageWayPoint : MonoBehaviour
{
    public Vector3[] waypoint = new Vector3[] { };
    
    private void OnEnable()
    {
        App.Instance.Waypoint = waypoint;
    }
}
