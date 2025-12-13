using TMPro;
using UnityEngine;

public class StatusPanelController : MonoBehaviour
{
    public GameObject painelStatus;

    [Header("Player")]
    public PlayerHealth playerHealth;
    public TextMeshProUGUI playerVidaText;
    public TextMeshProUGUI playerDanoText;
    public TextMeshProUGUI playerProjVelText;

    [Header("Pet")]
    public Pet pet; 
    public TextMeshProUGUI petVidaText;
    public TextMeshProUGUI petDanoText;
    public TextMeshProUGUI petProjVelText;

    private bool painelAtivo = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            painelAtivo = !painelAtivo;
            painelStatus.SetActive(painelAtivo);

            if (painelAtivo)
            {
                AtualizarStatus();
            }
        }
    }

    void AtualizarStatus()
    {
        if (playerHealth != null)
        {
            playerVidaText.text = "Vida: " + playerHealth.currentHealth;
            playerDanoText.text = "Dano: " + playerHealth.damage;
            playerProjVelText.text = "Velocidade Projétil: " + playerHealth.velocidadeProjetil.ToString("F1");
        }

        if (pet != null)
        {
            petVidaText.text = "Vida: " + pet.BaseHealth;
            petDanoText.text = "Dano: " + pet.DanoAtual;
            petProjVelText.text = "Velocidade Projétil: " + pet.VelocidadeProjAtual.ToString("F1");
        }
    }
}