using UnityEngine;

public class Project_damage : MonoBehaviour
{
    public int damage = 20;
    public GameObject attacker;

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Mobs"))
        {
            MobHealth hp = other.GetComponent<MobHealth>();
            if (hp != null)
            {
                hp.TakeDamage(damage, attacker);

            }
            gameObject.SetActive(false);
        }
    }
}