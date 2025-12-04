using UnityEngine;

public class PetProjectile : MonoBehaviour
{
    public float speed = 10f;
    private Transform target;
    public int damage = 20;
    private GameObject owner;

    public void SetTarget(Transform t, int dmg, GameObject petOwner)
    {
        target = t;
        damage = dmg;
        owner = petOwner;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        MobHealth mob = other.GetComponent<MobHealth>();
        if (mob != null)
        {
            mob.TakeDamage(damage, owner);
            Debug.Log("Projétil do Pet causou " + damage + " de dano ao mob!");
            Destroy(gameObject);
        }
    }
}