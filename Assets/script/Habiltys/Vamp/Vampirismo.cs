using UnityEngine;

public class Vampirismo : HabilidadeBase
{
    public override void ExecutarEfeito()
    {
        Debug.Log("Vampirismo ativado! Durante o buff, o jogador rouba vida dos mobs.");
        RoubarVida(mobHealth);
    }

    public void RoubarVida(MobHealth mob)
    {
        if (!EstaAtiva()) return;
        if (mob == null) return;
        int roubo = (int)(mob.baseHealth * 0.1f);
        PlayerHealth health = GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.Heal(roubo);
            Debug.Log($"Player recuperou {roubo} de vida com Vampirismo!");
        }
    }

    void Update()
    {
        if (EstaDesbloqueada() && Input.GetKeyDown(KeyCode.V))
        {
            Ativar();
        }
    }
}