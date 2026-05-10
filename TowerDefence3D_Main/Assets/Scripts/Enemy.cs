using UnityEngine;
using UnityEngine.UI;

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
    public float startHealth = 100;
    private float health;
    public float speed = 5f;
    public int DamageRate = 1;
    public int coinvalue = 5;

    private Transform targetWaypoint;
    private int waypointIndex = 0;

    private bool isDead = false;

    public Image healthBar;

    public GameObject DieEffect;

    private void Start()
    {
        if (WayPoints.Path_waypoints == null || WayPoints.Path_waypoints.Length == 0)
        {
            return;
        }

        health = startHealth;

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
        if (GameManager.instance != null)
        {
            GameManager.instance.TakeDamage(DamageRate);
        }
        WaveSpwaner.EnemiesAlive--;
        Destroy(gameObject);
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        health -= amount;

        healthBar.fillAmount = (float)health / startHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        GameManager.instance.EnemyScrore++;
        UIManager.Instance.UpdatetotalCoins(coinvalue);
        GameObject effect = (GameObject)Instantiate(DieEffect, transform.position, Quaternion.identity);
        if (Setting.settings.getsetSound == 0)
        {
            effect.GetComponent<AudioSource>().enabled = false;
        }
        else if (Setting.settings.getsetSound == 1)
        {
            effect.GetComponent<AudioSource>().enabled = true;
            effect.GetComponent<AudioSource>().Play();
        }
        Destroy(effect, 5f);
        WaveSpwaner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
