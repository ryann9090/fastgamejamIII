using UnityEngine;

public class Pet : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public Transform player;
    public float moveSpeed = 3f;
    public float followDistance = 2f;

    [Header("Configurações de Vida")]
    public int maxHealth = 100;
    public int BaseHealth = 100;

    [Header("Configurações de Ataque")]
    public int damage = 10;
    public float attackRange = 5f;
    public float attackCooldown = 3f;
    public GameObject projectilePrefab;

    private float lastAttackTime = 0f;
    private Transform target;

    void Start()
    {
        BaseHealth = maxHealth;
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        Vector3 followPos = player.position + Vector3.right * followDistance;
        transform.position = Vector2.MoveTowards(transform.position, followPos, moveSpeed * Time.deltaTime);

        GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Mobs");
        float menorDistancia = Mathf.Infinity;
        target = null;

        foreach (GameObject inimigo in inimigos)
        {
            float dist = Vector2.Distance(transform.position, inimigo.transform.position);
            if (dist < menorDistancia)
            {
                menorDistancia = dist;
                target = inimigo.transform;
            }
        }

        if (target != null && Time.time - lastAttackTime >= attackCooldown)
        {
            float dist = Vector2.Distance(transform.position, target.position);
            if (dist <= attackRange)
            {
                DispararRajadaWifi();
                lastAttackTime = Time.time;
            }
        }
    }

    void DispararRajadaWifi()
    {
        if (projectilePrefab == null || target == null) return;

        int numProjeteis = 6;
        float anguloInicial = -45f;
        float anguloFinal = 45f;
        float passo = (anguloFinal - anguloInicial) / (numProjeteis - 1);

        for (int i = 0; i < numProjeteis; i++)
        {
            float angulo = anguloInicial + passo * i;
            Quaternion rotacao = Quaternion.Euler(0, 0, angulo);

            GameObject proj = Instantiate(projectilePrefab, transform.position, rotacao);
            PetProjectile projScript = proj.GetComponent<PetProjectile>();
            if (projScript != null)
            {
                projScript.SetTarget(target, damage, player.gameObject);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        BaseHealth -= amount;
        if (BaseHealth <= 0)
        {
            BaseHealth = 0;
            Die();
        }
    }

    void Die()
    {
        InvocarCompanheiro invocador = Object.FindFirstObjectByType<InvocarCompanheiro>();
        if (invocador != null)
        {
            invocador.PetMorreu();
        }
        Destroy(gameObject);
    }
}