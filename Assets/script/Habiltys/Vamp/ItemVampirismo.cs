using UnityEngine;

public class ItemVampirismo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GunAndProjectile vamp = other.GetComponent<GunAndProjectile>();
            vamp.VampirismoAtivo = true;
            Debug.Log("Player coletou o item de GunAndProjectile!");
            Destroy(gameObject);
        }
    }
}