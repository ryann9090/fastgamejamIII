using UnityEngine;
using System.Collections;

public class Magnetismo : MonoBehaviour
{
    [Header("Configurações de Magnetismo")]
    public float attractionRadius = 5f;
    public float attractionSpeed = 5f;
    public float buffDuration = 15f;
    public float cooldown = 25f;

    private bool magnetismActive = false;
    private bool onCooldown = false;
    public bool habilidadeDesbloqueada = false;

    private float remainingBuffTime = 0f;

    void Update()
    {
        if (!habilidadeDesbloqueada) return;

        if (magnetismActive)
        {
            GameObject[] itens = GameObject.FindGameObjectsWithTag("Items");
            foreach (GameObject item in itens)
            {
                float distance = Vector2.Distance(item.transform.position, transform.position);
                if (distance <= attractionRadius)
                {
                    Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        Vector2 direction = (transform.position - item.transform.position).normalized;
                        rb.MovePosition(rb.position + direction * attractionSpeed * Time.deltaTime);
                    }
                    else
                    {
                        item.transform.position = Vector2.MoveTowards(
                            item.transform.position,
                            transform.position,
                            attractionSpeed * Time.deltaTime
                        );
                    }
                }
            }
        }

        if (!magnetismActive && !onCooldown && habilidadeDesbloqueada)
        {
            StartCoroutine(MagnetismCycle());
        }
    }

    private IEnumerator MagnetismCycle()
    {
        magnetismActive = true;
        remainingBuffTime = buffDuration;

        while (remainingBuffTime > 0f)
        {
            remainingBuffTime -= Time.deltaTime;
            yield return null;
        }

        magnetismActive = false;
        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }

    public void DesbloquearHabilidade()
    {
        if (!habilidadeDesbloqueada)
        {
            habilidadeDesbloqueada = true;
        }
    }

    public void AumentarDuracao()
    {
        if (magnetismActive)
        {
            remainingBuffTime += 10f;
        }
    }
}