using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public static Transform[] Path_waypoints;

    private void Awake()
    {
        Path_waypoints = new Transform[transform.childCount];
        for (int i=0; i<Path_waypoints.Length; i++)
        {
            Path_waypoints[i] = transform.GetChild(i);
        }
    }
}
