using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ia_monstros : MonoBehaviour
{
    public float speed = 3f;
    public Transform target;
    public float chaseRange = 15f;
    public float stopDistance = 2f;
    public bool debugLogs = false;
    private Rigidbody2D rb;

    public int vida = 100;
    public int dano = 10;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
                if (debugLogs) Debug.Log(name + ": alvo inicial encontrado -> " + target.name);
            }
            else if (debugLogs)
            {
                Debug.Log(name + ": Player não encontrado no Start (tag 'Player').");
            }
        }
    }

    void Update()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
                if (debugLogs) Debug.Log(name + ": alvo atribuído em Update -> " + target.name);
            }
        }
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector2 currentPos = rb.position;
        Vector2 targetPos = target.position;
        float dist = Vector2.Distance(currentPos, targetPos);

        if (dist <= chaseRange)
        {
            if (dist > stopDistance)
            {
                Vector2 direction = (targetPos - currentPos).normalized;
                rb.MovePosition(currentPos + direction * speed * Time.fixedDeltaTime);
                if (debugLogs) Debug.Log(name + ": perseguindo player. dist=" + dist.ToString("F2"));
            }
            else
            {
                if (debugLogs) Debug.Log(name + ": dentro da distância de parada. dist=" + dist.ToString("F2"));
            }
        }
        else if (debugLogs)
        {
            Debug.Log(name + ": player fora do chaseRange. dist=" + dist.ToString("F2") + " range=" + chaseRange);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(dano);
                if (debugLogs) Debug.Log(name + ": causou " + dano + " de dano ao Player.");
            }
        }
    }

    public void TakeDamage(int amount)
    {
        vida -= amount;
        if (debugLogs) Debug.Log(name + ": recebeu " + amount + " de dano. Vida restante = " + vida);

        if (vida <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (debugLogs) Debug.Log(name + ": morreu.");
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}