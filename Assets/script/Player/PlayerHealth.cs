using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Stats do Player")]
    public int maxHealth = 100;
    public int currentHealth = 100;
    public int damage = 20;
    public float moveSpeed = 5f;

    [Header("XP e Nível")]
    public int currentXP = 0;
    public int currentLevel = 1;
    public int xpToNextLevel = 250;
    public static int mobBuffCount = 0;

    void Start()
    {
        currentHealth = maxHealth;
    }

       
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player morreu!");
        gameObject.SetActive(false);
    }

    
    public void AddXP(int amount)
    {
        currentXP += amount;
        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        currentXP -= xpToNextLevel;
        currentLevel++;

        damage += 30;
        maxHealth += 50;
        currentHealth = maxHealth;

        xpToNextLevel *= 5;

        BuffMobs();
    
    }

  
    void BuffMobs()
    {
        GameManager.Instance.mobBuffCount++;

        MobHealth[] mobs = FindObjectsOfType<MobHealth>();
        foreach (MobHealth mob in mobs)
        {
            mob.IncreaseStats(100, 15);
        }
    }
}