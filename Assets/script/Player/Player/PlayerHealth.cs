using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Stats do Player")]
    public int maxHealth = 100;
    public int currentHealth = 100;
    public int damage = 20;
    public float moveSpeed = 5f;
    public float velocidadeProjetil = 12f;

    [Header("XP e Nível")]
    public int currentXP = 0;
    public int currentLevel = 1;
    public int xpToNextLevel = 250;
    public static int mobBuffCount = 0;

    [Header("Projéteis")]
    public float projectileSpeedMultiplier = 1f;

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

        damage = Mathf.RoundToInt(damage * 1.15f);
        maxHealth = Mathf.RoundToInt(maxHealth * 1.15f);
        currentHealth = maxHealth;

        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 3f);

        BuffMobs();
        BuffPet();

        if (Random.value <= 0.1f)
        {
            projectileSpeedMultiplier *= 1.03f;
            Debug.Log("Buff de projétil: Velocidade aumentada em 3%. Novo multiplicador: " + projectileSpeedMultiplier);
        }
    }

    void BuffMobs()
    {
        GameManager.Instance.mobBuffCount++;

        MobHealth[] mobs = Object.FindObjectsByType<MobHealth>(FindObjectsSortMode.None);
        foreach (MobHealth mob in mobs)
        {
            mob.IncreaseStatsPercent(0.15f, 0.15f);
        }
    }

    void BuffPet()
    {
        Pet pet = Object.FindFirstObjectByType<Pet>();
        if (pet != null)
        {
            pet.maxHealth = Mathf.RoundToInt(pet.maxHealth * 1.15f);
            pet.BaseHealth = pet.maxHealth;
            pet.damage = Mathf.RoundToInt(pet.damage * 1.15f);
            Debug.Log("Pet buffado: Vida " + pet.BaseHealth + " | Dano " + pet.damage);
        }
    }
}