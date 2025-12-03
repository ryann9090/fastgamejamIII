using UnityEngine;

public class persegue_mobs : MonoBehaviour
{
    public float speed = 10f;
    public float rotateSpeed = 200f;
    public float lifeTime = 5f;

    private Transform target;
    private int damage;
    public GameObject attacker; // quem disparou
    public HabilidadeBase habilidadeBase;

    void Start()
    {
        Destroy(gameObject, lifeTime);
        FindClosestTarget();

        PlayerHealth playerHealth = Object.FindFirstObjectByType<PlayerHealth>();
        if (playerHealth != null)
        {
            damage = playerHealth.damage;
            attacker = playerHealth.gameObject; // guarda o Player como atacante
        }
    }

    void Update()
    {
        if (target == null)
        {
            FindClosestTarget();
            return;
        }

        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        transform.Rotate(0, 0, -rotateAmount * rotateSpeed * Time.deltaTime);

        transform.position += (Vector3)(transform.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mobs"))
        {
            MobHealth hp = other.GetComponent<MobHealth>();
            if (hp != null)
            {
                hp.TakeDamage(damage, attacker);

                if (hp.baseHealth <= 0)
                {
                    Vampirismo vamp = attacker.GetComponent<Vampirismo>();
                    if (vamp != null)
                    {
                        vamp.RoubarVida(hp);
                    }
                }
            }
            Destroy(gameObject);
        }
    }

    void FindClosestTarget()
    {
        GameObject[] mobs = GameObject.FindGameObjectsWithTag("Mobs");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestMob = null;

        foreach (GameObject mob in mobs)
        {
            float distance = Vector2.Distance(transform.position, mob.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestMob = mob;
            }
        }

        if (nearestMob != null)
        {
            target = nearestMob.transform;
        }
    }
}