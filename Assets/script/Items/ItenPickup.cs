using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string itemName = "Item";
    public string rarity = "Comum";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            Debug.Log("Player coletou: " + itemName + " (" + rarity + ")");
            Destroy(gameObject);
        }
    }
}