using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform Target;

    public float speed = 70f;
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
        Destroy(Effect,2f);

        Destroy(Target.gameObject);
        Destroy(gameObject);
    }

}
