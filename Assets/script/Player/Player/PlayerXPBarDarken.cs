using UnityEngine;
using UnityEngine.UI;

public class PlayerXPBarDarken : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image xpBarFill;

    private Color originalColor;

    void Start()
    {
        if (xpBarFill != null)
        {
            originalColor = xpBarFill.color;
        }
    }

    void Update()
    {
        if (playerHealth != null && xpBarFill != null)
        {
            float xpPercent = (float)playerHealth.currentXP / playerHealth.xpToNextLevel;

            // Escurece a barra conforme o XP está baixo, clareia conforme enche
            Color darkened = originalColor * xpPercent;
            darkened.a = 1f; // mantém opacidade total

            xpBarFill.color = darkened;
        }
    }
}