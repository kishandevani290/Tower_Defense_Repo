using UnityEngine;

public class Turnet : MonoBehaviour
{
    [Header("Fire Attributes")]
    public float fireRate = 1f;
    private float fireCountDown = 0f;
    public float Range = 15f;

    private Transform target;

    [Header("Set up Things")]
    public string enemyTag = "Enemy";
    public float TurnSpeed = 10f;
    public Transform PartToRotate;
    public GameObject BulletPrefab;
    public Transform firepoint;
    
    private void Start()
    {
        InvokeRepeating(nameof(UpdateTarget),0f,0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemis = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemis)
        {
            float distanceToenemy = Vector3.Distance(transform.position,enemy.transform.position);
            if (distanceToenemy < shortestDistance)
            { 
                shortestDistance = distanceToenemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= Range)
        {
            target = nearestEnemy.transform;
        }
        else
        { 
            target = null;
        }
    }

    private void Update()
    {
        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation,lookrotation,Time.deltaTime * TurnSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0f,rotation.y,0f);

        if(fireCountDown <= 0f)
        {
            shoot();
            fireCountDown = 1f / fireRate;
        }

        fireCountDown -= Time.deltaTime;
    }

    void shoot()
    {
        GameObject BulletGo = (GameObject)Instantiate(BulletPrefab,firepoint.position,firepoint.rotation);
        Bullet bullet = BulletGo.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Fire(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
