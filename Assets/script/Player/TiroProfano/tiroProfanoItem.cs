using UnityEngine;

public class tiroProfanoItem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        TiroProfano habilidade = other.GetComponent<TiroProfano>();
        if (habilidade != null)
        {
            habilidade.Desbloquear();
            Debug.Log("Player coletou Tiro Profano! Habilidade passiva ativada.");
            Destroy(gameObject);
        }
    }
}