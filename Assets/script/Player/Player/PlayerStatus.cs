using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Text statusText;

    void Update()
    {
        if (playerHealth == null) return;

        string statusInfo =
            "Nível:  " + playerHealth.currentLevel +
            "\nXP: " + playerHealth.currentXP + "/" + playerHealth.xpToNextLevel +
            " Vida:  " + playerHealth.currentHealth + "/" + playerHealth.maxHealth +
            " Dano:  " + playerHealth.damage +
            " Velocidade:  " + playerHealth.moveSpeed;

        if (statusText != null)
        {
            statusText.text = statusInfo;
        }
        else
        {
            Debug.Log(statusInfo);
        }
    }
    public void RoubarVida(MobHealth mob)
    {
        int roubo = (int)(mob.baseHealth * 0.1f); // 10% da vida máxima

        PlayerHealth health = GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.Heal(roubo);
        }

        Debug.Log("Player roubou " + roubo + " de vida com Vampirismo!");
    }
}