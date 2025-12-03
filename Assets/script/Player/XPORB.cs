using UnityEngine;

public class XPORB : MonoBehaviour
{
    public int xpValue = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.AddXP(xpValue);
            Destroy(gameObject);
        }
    }
}