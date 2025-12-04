using UnityEngine;

public class MagnetItem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Magnetismo magnetismo = other.GetComponent<Magnetismo>();
        if (magnetismo != null)
        {
            magnetismo.DesbloquearHabilidade();
            Debug.Log("Player coletou Magnetismo e desbloqueou a habilidade!");
            Destroy(gameObject);
        }
    }
}