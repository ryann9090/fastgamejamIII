using UnityEngine;

public class MobHealth : MonoBehaviour
{
    public int baseHealth = 100;
    public int baseDamage = 20;
    public GameObject xpPrefab;
    [HideInInspector] public int maxHealth = 100;

    [Header("Itens por Raridade")]
    public GameObject GoldItemPrefab;

    [Header("Itens Especiais")]
    public GameObject[] habilityItemPrefabs;

    
    public static bool magnetDropped = false;

    [Header("dano ao mob")]
    public GameObject attacker;

    private int currentHealth;
    private int currentDamage;

    public static int orbValue = 1;

    [HideInInspector] public SpawnAleatorioDeMonstros spawner;

    void Awake()
    {
        int buffCount = GameManager.Instance != null ? GameManager.Instance.mobBuffCount : 0;

        baseHealth += buffCount * 100;
        baseDamage += buffCount * 15;
        orbValue = 1 + buffCount * 2;

        currentHealth = baseHealth;
        currentDamage = baseDamage;
        maxHealth = currentHealth;
    }

    public void TakeDamage(int amount, GameObject attacker)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die(attacker);
        }
    }

    void Die(GameObject attacker)
    {
        Vampirismo vamp = attacker.GetComponent<Vampirismo>();
        if (vamp != null)
        {
            vamp.RoubarVida(this);
        }

        int xpAmount = Random.Range(3, 6);
        Drop_XP.Drop(xpAmount, transform.position, xpPrefab, orbValue);

        float chance = Random.Range(0f, 100f);
        if (chance <= 100f)
        {
            DropItem();
        }

        if (spawner != null)
        {
            spawner.MobMorto();
        }

        Destroy(gameObject);
    }

    void DropItem()
    {
        float rarityRoll = Random.Range(0f, 100f);

        if (rarityRoll <= 50f && GoldItemPrefab != null)
        {
            Instantiate(GoldItemPrefab, transform.position, Quaternion.identity);
            Debug.Log(gameObject.name + " dropou Gold!!!");
        }
        else if (rarityRoll <= 100f && habilityItemPrefabs.Length > 0)
        {
            int index = Random.Range(0, habilityItemPrefabs.Length);
            GameObject chosenPrefab = habilityItemPrefabs[index];

            
            if (chosenPrefab.name.Contains("Magnet") && magnetDropped)
            {
                Debug.Log("Magnetismo já foi dropado, não dropa novamente.");
                return;
            }

            
            GameObject item = Instantiate(chosenPrefab, transform.position, Quaternion.identity);
            Debug.Log(gameObject.name + " dropou Habilidade: " + chosenPrefab.name);

            
            if (chosenPrefab.name.Contains("Magnet"))
            {
                magnetDropped = true;
            }
        }
    }

    public void IncreaseStats(int healthBonus, int damageBonus)
    {
        baseHealth += healthBonus;
        baseDamage += damageBonus;

        currentHealth = baseHealth;
        currentDamage = baseDamage;

        orbValue += 2;

        Debug.Log(gameObject.name + " buffado: Vida " + currentHealth +
                  " | Dano " + currentDamage +
                  " | Valor dos orbes: " + orbValue);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Pet pet = other.GetComponent<Pet>();
        if (pet != null)
        {
            pet.TakeDamage(baseDamage);
            Debug.Log("Mob causou " + baseDamage + " de dano ao Pet!");
        }

        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(baseDamage);
            Debug.Log("Mob causou " + baseDamage + " de dano ao Player!");
        }
    }
}