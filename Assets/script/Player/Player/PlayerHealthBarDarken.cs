using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarDarken : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image healthBarFill;

    private Color originalColor;

    void Start()
    {
        if (healthBarFill != null)
        {
            originalColor = healthBarFill.color;
        }
    }

    void Update()
    {
        if (playerHealth != null && healthBarFill != null)
        {
            float healthPercent = (float)playerHealth.currentHealth / playerHealth.maxHealth;

            Color darkened = originalColor * healthPercent;
            darkened.a = 1f; // mantém opacidade total

            healthBarFill.color = darkened;
        }
    }
}