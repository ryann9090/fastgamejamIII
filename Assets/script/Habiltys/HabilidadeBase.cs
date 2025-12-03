using UnityEngine;
using System.Collections;

public abstract class HabilidadeBase : MonoBehaviour
{
    [Header("Configurações Gerais da Habilidade")]
    [Tooltip("Tempo que a habilidade fica ativa.")]
    public float buffDuration = 15f;

    [Tooltip("Tempo de recarga após uso.")]
    public float cooldown = 0f;
    public MobHealth mobHealth;

    protected bool desbloqueada = false;
    protected bool ativa = false;
    protected bool emCooldown = false;

    // Método para desbloquear a habilidade
    public void Desbloquear()
    {
        // if (!desbloqueada)
        {
            desbloqueada = true;
            Debug.Log($"Habilidade desbloqueada: {GetType().Name}");
        }
    }

    // Método para ativar manualmente a habilidade
    public void Ativar()
    {
        // if (ativa) return;
        Ciclo();
    }

    // Ciclo completo da habilidade (ativa -> duração -> cooldown)
    private void Ciclo()
    {
        ativa = true;
        Debug.Log($"{GetType().Name} ativada!");
        ExecutarEfeito();

        // yield return new WaitForSeconds(buffDuration);

        ativa = false;
        Debug.Log($"{GetType().Name} desativada!");

        emCooldown = true;
        // yield return new WaitForSeconds(cooldown);
        emCooldown = false;
    }

    
    public abstract void ExecutarEfeito();

    
    public bool EstaAtiva() => ativa;

    
    public bool EstaEmCooldown() => emCooldown;

    
    public bool EstaDesbloqueada() => desbloqueada;
}