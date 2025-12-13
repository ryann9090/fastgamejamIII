using UnityEngine;
using System.Collections;

public class TiroProfano : MonoBehaviour
{
    public float cooldown = 10f;
    public bool Pronta { get; private set; } = true;
    private bool desbloqueada = false;

    [Header("Referências")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public PlayerHealth playerHealth;
    public float speed = 10f;
    public float rotateSpeed = 200f;
    public float lifeTime = 5f;

    void Start()
    {
        if (playerHealth == null)
        {
            playerHealth = GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        if (!desbloqueada) return;

        if (Input.GetKeyDown(KeyCode.B) && Pronta)
        {
            DispararRajadaProfana();
            IniciarCooldown();
        }
    }

    public void IniciarCooldown()
    {
        if (!Pronta) return;
        Pronta = false;
        StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(cooldown);
        Pronta = true;
    }

    void DispararRajadaProfana()
    {
        int numProjeteis = 6;
        float anguloInicial = -45f;
        float anguloFinal = 45f;
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
            projScript.damage = danoAtual * 2;

            proj.transform.localScale *= 2f;
            SpriteRenderer sr = proj.GetComponent<SpriteRenderer>();
            if (sr != null) sr.color = Color.magenta;
        }
    }

    public void Desbloquear()
    {
        desbloqueada = true;
    }
}