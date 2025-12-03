using UnityEngine;

public class PlayerRegen : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public int regenAmount = 2;
    public float regenInterval = 1f;
    public float regenDelayOnDamage = 3f;

    private float timer;
    private float damageCooldown;

    void Update()
    {
        if (playerHealth == null) return;

        if (damageCooldown > 0)
        {
            damageCooldown -= Time.deltaTime;
            return;
        }

        timer += Time.deltaTime;

        if (timer >= regenInterval)
        {
            playerHealth.Heal(regenAmount);
            timer = 0f;
            Debug.Log("Player regenerou " + regenAmount + " vida. Vida atual: " + playerHealth.currentHealth);
        }
    }

    public void OnPlayerDamaged()
    {
        damageCooldown = regenDelayOnDamage;
    }
}