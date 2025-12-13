using UnityEngine;

public class MobHealth : MonoBehaviour
{
    [Header("Stats Base")]
    public int baseHealth = 100;
    public int baseDamage = 20;
    public GameObject xpPrefab;
    [HideInInspector] public int maxHealth = 100;

    [Header("Itens por Raridade")]
    public GameObject ObbLifeItemPrefab;
    [Header("Itens Especiais")]
    public GameObject[] habilityItemPrefabs;

    
    public static bool magnetDropped = false;
    public static bool tiroProfanoDropped = false;

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
            //Die(attacker);
        }
    }

    //void Die(GameObject attacker)
    //{
    //    Vampirismo vamp = attacker.GetComponent<Vampirismo>();
    //    if (vamp != null)
    //    {
    //        vamp.RoubarVida(this);
    //    }

    //    int xpAmount = Random.Range(0, 3);
    //    Drop_XP.Drop(xpAmount, transform.position, xpPrefab, orbValue);

    //    float chance = Random.Range(0f, 100f);
    //    if (chance <= 100f)
    //    {
    //        DropItem();
    //    }

    //    if (spawner != null)
    //    {
    //        spawner.MobMorto();
    //    }

    //    Destroy(gameObject);
    //}

    void DropItem()
    {
        float rarityRoll = Random.Range(0f, 100f);

        if (rarityRoll > 97f && rarityRoll <= 99f && ObbLifeItemPrefab != null)
        {
            Instantiate(ObbLifeItemPrefab, transform.position, Quaternion.identity);
        }
        else if (rarityRoll > 99.8f && habilityItemPrefabs.Length > 0)
        {
            int index = Random.Range(0, habilityItemPrefabs.Length);
            GameObject chosenPrefab = habilityItemPrefabs[index];

            if (chosenPrefab.name.Contains("Magnet"))
            {
                Debug.Log(gameObject.name + " dropou Habilidade: Magnet");
            }

            GameObject item = Instantiate(chosenPrefab, transform.position, Quaternion.identity);
            Debug.Log(gameObject.name + " dropou Habilidade: " + chosenPrefab.name);

            if (chosenPrefab.name.Contains("TiroProfano") && tiroProfanoDropped)
            {
                Debug.Log("Tiro Profano já foi dropado, não dropa novamente.");
                return;
            }

            if (chosenPrefab.name.Contains("TiroProfano"))
            {
                tiroProfanoDropped = true;
            }
            if (chosenPrefab.name.Contains("DebufMobs"))
            {
                Debug.Log(gameObject.name + " dropou Habilidade: DebufMobs");
            }
        }
    }

    public void IncreaseStatsPercent(float healthPercent, float damagePercent)
    {
        maxHealth = Mathf.RoundToInt(maxHealth * (1f + healthPercent));
        baseHealth = maxHealth;
        baseDamage = Mathf.RoundToInt(baseDamage * (1f + damagePercent));
        orbValue = Mathf.RoundToInt(orbValue * 1.5f);
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