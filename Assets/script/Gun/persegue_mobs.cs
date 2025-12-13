using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public PlayerHealth playerHealth;
    public int quantidadePorRajada = 1;
    public float fireRate = 0.3f;

    public float speed = 10f;
    public float rotateSpeed = 200f;
    public float lifeTime = 5f;

    private TiroProfano tiroProfano;
    private float nextFireTime;
    private bool autoFire = true;

    private int tirosRestantes = 5;
    private bool recarregando = false;

    void Start()
    {
        tiroProfano = GetComponent<TiroProfano>();
        if (playerHealth == null)
        {
            playerHealth = GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            autoFire = !autoFire;
        }

        if (!recarregando)
        {
            if (autoFire && Time.time >= nextFireTime)
            {
                DispararRajada(false);
                nextFireTime = Time.time + fireRate;
            }

            if (!autoFire && Input.GetKeyDown(KeyCode.Space))
            {
                DispararRajada(false);
            }

            if (Input.GetKeyDown(KeyCode.B) && tiroProfano != null && tiroProfano.Pronta)
            {
                DispararRajada(true);
                tiroProfano.IniciarCooldown();
            }
        }
    }

    void DispararRajada(bool profano)
    {
        if (tirosRestantes <= 0)
        {
            StartCoroutine(Recarregar());
            return;
        }

        int numProjeteis = profano ? 6 : quantidadePorRajada;
        float anguloInicial = profano ? -45f : 0f;
        float anguloFinal = profano ? 45f : 0f;
        float passo = (numProjeteis > 1) ? (anguloFinal - anguloInicial) / (numProjeteis - 1) : 0f;

        int danoAtual = playerHealth != null ? playerHealth.damage : 10;

        for (int i = 0; i < numProjeteis; i++)
        {
            float angulo = anguloInicial + passo * i;
            Quaternion rotacao = firePoint.rotation * Quaternion.Euler(0, 0, angulo);

            GameObject proj = Instantiate(projectilePrefab, firePoint.position, rotacao);

            PersegueMobs projScript = proj.AddComponent<PersegueMobs>();
            projScript.speed = speed;
            projScript.rotateSpeed = rotateSpeed;
            projScript.lifeTime = lifeTime;
            projScript.attacker = gameObject;

            SpriteRenderer sr = proj.GetComponent<SpriteRenderer>();

            if (profano)
            {
                proj.transform.localScale *= 2f;
                projScript.damage = danoAtual * 2;
                if (sr != null) sr.color = Color.magenta;
            }
            else
            {
                projScript.damage = danoAtual;
                if (sr != null) sr.color = Color.red;
            }
        }

        tirosRestantes--;
        if (tirosRestantes <= 0)
        {
            StartCoroutine(Recarregar());
        }
    }

    IEnumerator Recarregar()
    {
        recarregando = true;
        yield return new WaitForSeconds(1f); // tempo de recarga
        tirosRestantes = 5; // recarrega 5 tiros
        recarregando = false;
    }
}

public class PersegueMobs : MonoBehaviour
{
    public float speed = 10f;
    public float rotateSpeed = 200f;
    public float lifeTime = 5f;

    public int damage;
    public GameObject attacker;
    private Transform target;

    void Start()
    {
        Destroy(gameObject, lifeTime);
        FindClosestTarget();
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