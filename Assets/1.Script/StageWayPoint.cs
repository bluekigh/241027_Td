using UnityEngine;

public class StageWayPoint : MonoBehaviour
{
    public Vector3[] waypoint = new Vector3[] { };
    private void Start() {
        App.Instance.Waypoint = waypoint;
    }
}
