using UnityEngine;

public class ObbLife : MonoBehaviour
{
    public int healAmount = 7;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.Heal(healAmount); 
            Destroy(gameObject);      
        }
    }
}