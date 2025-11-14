using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    [SerializeField] private LayerMask collisionMask;
    private float speed = 10f;
    private float damage = 1;

    private float lifeTime= 1.5f;
    private float skinWith = .1f;

    void Start()
    {
        Destroy(gameObject, lifeTime);

        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, .1f, collisionMask);
        if(initialCollisions.Length > 0)
        {
            OnHitObject(initialCollisions[0]);
        }
    }

    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance + skinWith, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
        }
    }
    
    private void OnHitObject(RaycastHit hit)
    {
        //print(hit.collider.gameObject.name);
        IDamageable damageable = hit.collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeHit(damage, hit);
        }
        GameObject.Destroy(gameObject);
    }

    private void OnHitObject(Collider c)
    {
        IDamageable damageable = c.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
        GameObject.Destroy(gameObject);
    }
}
