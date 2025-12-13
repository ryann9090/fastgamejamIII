using UnityEngine;
using System.Collections;

public class InvocarCompanheiro : MonoBehaviour
{
    [Header("Configurações da Habilidade")]
    public GameObject companheiroPrefab;
    private GameObject companheiroInstanciado;
    public bool habilidadeDesbloqueada = false;

    private PlayerHealth playerHealth;
    private bool podeInvocar = true; // controla cooldown

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        
        if (playerHealth != null && playerHealth.currentLevel >= 5)
        {
            habilidadeDesbloqueada = true;
        }

        if (habilidadeDesbloqueada && podeInvocar && Input.GetKeyDown(KeyCode.U))
        {
            AlternarCompanheiro();
        }
    }

    void AlternarCompanheiro()
    {
        
        if (playerHealth == null || playerHealth.currentLevel < 3)
        {
            Debug.Log("Companheiro só pode ser invocado a partir do nível 3!");
            return;
        }

        if (companheiroInstanciado == null)
        {
            companheiroInstanciado = Instantiate(
                companheiroPrefab,
                transform.position + Vector3.right * 2,
                Quaternion.identity
            );

            Pet pet = companheiroInstanciado.GetComponent<Pet>();
            if (pet != null)
            {
                pet.player = this.transform;
            }

            Debug.Log("Companheiro invocado!");
        }
        else
        {
            Destroy(companheiroInstanciado);
            companheiroInstanciado = null;
            StartCoroutine(CooldownInvocacao()); // cooldown se remover manualmente
            Debug.Log("Companheiro desapareceu!");
        }
    }

    // chamado pelo Pet quando morre
    public void PetMorreu()
    {
        companheiroInstanciado = null;
        StartCoroutine(CooldownInvocacao());
    }

    private IEnumerator CooldownInvocacao()
    {
        podeInvocar = false;
        Debug.Log("Esperando 10 segundos para invocar novamente...");
        yield return new WaitForSeconds(10f);
        podeInvocar = true;
        Debug.Log("Agora você pode invocar o companheiro novamente!");
    }
}