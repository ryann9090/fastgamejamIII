using UnityEngine;

public class PetStatus : MonoBehaviour
{
    public Pet pet;

    public int VidaAtual
    {
        get
        {
            if (pet != null)
                return pet.BaseHealth;
            return 0;
        }
    }

    public int DanoAtual
    {
        get
        {
            if (pet != null)
                return pet.damage;
            return 0;
        }
    }

    public float VelocidadeProjAtual
    {
        get
        {
            if (pet != null)
                return pet.velocidadeProjetil;
            return 0f;
        }
    }
}