using UnityEngine;

public class MagnetismoItem : MonoBehaviour
{
    private static bool jaColetado = false; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !jaColetado)
        {
            Magnetismo magnetismo = other.GetComponent<Magnetismo>();
            if (magnetismo != null)
            {
                magnetismo.DesbloquearHabilidade();
                jaColetado = true; // não permite pegar novamente
            }

            Debug.Log("Player coletou o ITEM DE MAGNETISMO!");
            Destroy(gameObject);
        }
    }
}