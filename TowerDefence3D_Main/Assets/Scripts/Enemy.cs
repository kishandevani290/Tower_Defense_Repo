using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        Standard,
        Fast,
        Heavy,
        Boss
    }

    [Header("EnemyType")]
    public EnemyType typeOfEnemy;

    [Tooltip("Enemy Movment Speed")]
    public float speed = 5f;

    private Transform targetWaypoint;
    private int waypointIndex = 0;

    private void Start()
    {
        if (WayPoints.Path_waypoints == null || WayPoints.Path_waypoints.Length == 0)
        {
            return;
        }
        
        targetWaypoint = WayPoints.Path_waypoints[waypointIndex];
    }

    private void Update()
    {
        if (targetWaypoint == null) return;
        
        Vector3 direction = targetWaypoint.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        if (Vector3.Distance(transform.position, targetWaypoint.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        waypointIndex++;

        if (waypointIndex >= WayPoints.Path_waypoints.Length)
        {
            OnReachEndOfPath();
            return;
        }

        targetWaypoint = WayPoints.Path_waypoints[waypointIndex];
    }

    private void OnReachEndOfPath()
    {
        Destroy(gameObject);
    }
}
