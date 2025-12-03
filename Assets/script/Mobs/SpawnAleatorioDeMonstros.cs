using UnityEngine;

public class SpawnAleatorioDeMonstros : MonoBehaviour
{
    public GameObject[] monstros; 
    public Transform[] pontosDeSpawn;
    public float intervaloDeSpawn = 5f; 
    private float tempoDesdeUltimoSpawn = 0f;

    public Color gizmoColor = Color.cyan;
    public float gizmoRadius = 0.25f;

    // Controle de waves
    public int mobsPorWave = 100;   
    private int mobsRestantesNaWave; 
    private int mobsVivos = 0;     

    void Start()
    {
        mobsRestantesNaWave = mobsPorWave;
    }

    void Update()
    {
        tempoDesdeUltimoSpawn += Time.deltaTime;

       
        if (tempoDesdeUltimoSpawn >= intervaloDeSpawn && mobsVivos < mobsPorWave)
        {
            SpawnMonstro();
            tempoDesdeUltimoSpawn = 0f;
        }

        
        if (mobsRestantesNaWave <= 0 && mobsVivos == 0)
        {
            NovaWave();
        }
    }

    void SpawnMonstro()
    {
        int indiceMonstro = Random.Range(0, monstros.Length);
        int indicePontoDeSpawn = Random.Range(0, pontosDeSpawn.Length);

        GameObject mob = Instantiate(monstros[indiceMonstro], pontosDeSpawn[indicePontoDeSpawn].position, Quaternion.identity);

        // registra que há mais um mob vivo
        mobsVivos++;

        // avisa o MobHealth quem controla o spawn
        MobHealth mobHealth = mob.GetComponent<MobHealth>();
        if (mobHealth != null)
        {
            mobHealth.spawner = this;
        }
    }

    public void MobMorto()
    {
        mobsVivos--;
        mobsRestantesNaWave--;
    }

    void NovaWave()
    {
        mobsPorWave += 50; 
        mobsRestantesNaWave = mobsPorWave;
        Debug.Log("Nova wave iniciada! Quantidade de mobs: " + mobsPorWave);
    }

    void OnDrawGizmosSelected()
    {
        if (pontosDeSpawn == null || pontosDeSpawn.Length == 0)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(transform.position, gizmoRadius);
            return;
        }

        Gizmos.color = gizmoColor;
        for (int i = 0; i < pontosDeSpawn.Length; i++)
        {
            Transform p = pontosDeSpawn[i];
            if (p == null) continue;
            Gizmos.DrawWireSphere(p.position, gizmoRadius);
            Gizmos.DrawLine(transform.position, p.position);
        }
    }
}