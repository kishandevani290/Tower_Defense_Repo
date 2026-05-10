using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform Target;

    public float speed = 70f;
    public float damage = 80f;

    public GameObject ImpactEffect;

    public void Fire(Transform _target)
    {
        Target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
        { 
            Destroy(gameObject);
            return;
        }

        Vector3 dir = Target.position - transform.position;
        float distancethisframe = speed * Time.deltaTime;

        if (dir.magnitude <= distancethisframe)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distancethisframe,Space.World);
        transform.LookAt(Target);
    }

    void HitTarget()
    {
        GameObject Effect = (GameObject)Instantiate(ImpactEffect,transform.position,transform.rotation);
        if (Setting.settings.getsetSound == 0)
        {
            Effect.GetComponent<AudioSource>().enabled = false;
            Debug.Log("Turn off Sound");
        }
        else if (Setting.settings.getsetSound == 1)
        {
            Effect.GetComponent<AudioSource>().enabled = true;
            Effect.GetComponent<AudioSource>().Play();
        }
        Destroy(Effect,2f);

        Enemy enemy = Target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage((int)damage);
        }
        Destroy(gameObject);
    }
}
