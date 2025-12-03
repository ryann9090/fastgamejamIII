using UnityEngine;

public class InvocarCompanheiro : HabilidadeBase
{
    public GameObject companheiroPrefab;

    
    private bool habilidadeDesbloqueada = false;

    void Update()
    {
        // só permite invocar se a habilidade estiver desbloqueada
        if (habilidadeDesbloqueada && Input.GetKeyDown(KeyCode.U))
        {
            ExecutarEfeito();
        }
    }

    
    public void DesbloquearHabilidade()
    {
        habilidadeDesbloqueada = true;
        Debug.Log("Você desbloqueou a habilidade de invocar companheiro! Pressione U para usar.");
    }

    public override void ExecutarEfeito()
    {
        if (companheiroPrefab != null)
        {
            GameObject aliado = Instantiate(
                companheiroPrefab,
                transform.position + Vector3.right * 2,
                Quaternion.identity
            );

            Destroy(aliado, buffDuration);
            Debug.Log("Companheiro invocado!");
        }
    }
}