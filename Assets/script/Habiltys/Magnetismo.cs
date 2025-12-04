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
                    // ✅ Ajuste: se tiver Rigidbody2D, usa física
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

        
        if (!magnetismActive && !onCooldown)
        {
            StartCoroutine(MagnetismCycle());
        }
    }

    private IEnumerator MagnetismCycle()
    {
        magnetismActive = true;
        Debug.Log("Magnetismo ativado!");
        yield return new WaitForSeconds(buffDuration);

        magnetismActive = false;
        Debug.Log("Magnetismo desativado!");

        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }

    // chama o item para desbloquear a habilidade
    public void DesbloquearHabilidade()
    {
        if (!habilidadeDesbloqueada)
        {
            habilidadeDesbloqueada = true;
            Debug.Log("Habilidade de Magnetismo desbloqueada!");
        }
    }
}