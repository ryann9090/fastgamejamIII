using UnityEngine;
using UnityEngine.SceneManagement;

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
        BuffPet();
    }

    void BuffMobs()
    {
        GameManager.Instance.mobBuffCount++;

        MobHealth[] mobs = Object.FindObjectsByType<MobHealth>(FindObjectsSortMode.None);
        foreach (MobHealth mob in mobs)
        {
            mob.IncreaseStats(100, 15);
        }
    }

    void BuffPet()
    {
        Pet pet = Object.FindFirstObjectByType<Pet>();
        if (pet != null)
        {
            pet.maxHealth += 50;   // mesmo buff de vida que o Player
            pet.BaseHealth = pet.maxHealth;
            pet.damage += 15;      // buff de dano proporcional
            Debug.Log("Pet buffado: Vida " + pet.BaseHealth + " | Dano " + pet.damage);
        }
    }
}