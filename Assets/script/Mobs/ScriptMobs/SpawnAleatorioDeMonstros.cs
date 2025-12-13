using UnityEngine;
using System.Collections;
using TMPro;

public class SpawnAleatorioDeMonstros : MonoBehaviour
{
    public GameObject[] monstros;
    public Transform[] pontosDeSpawn;

    public float intervaloDeSpawn = 1f;
    private float tempoDesdeUltimoSpawn = 0f;

    public int mobsPorWave = 10;
    private int mobsRestantesNaWave;
    private int mobsVivos = 0;

    private int waveAtual = 1;
    private int waveMaxima = 10;
    private bool waveEmAndamento = false;

    public float tempoEntreWaves = 10f;
    private float contadorWave = 0f;
    private bool contandoWave = false;

    public TextMeshProUGUI proximaWaveText;

    void Start()
    {
        StartCoroutine(IniciarWaveComDelay(5f));
        proximaWaveText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (waveEmAndamento)
        {
            tempoDesdeUltimoSpawn += Time.deltaTime;

            if (tempoDesdeUltimoSpawn >= intervaloDeSpawn && mobsRestantesNaWave > 0)
            {
                SpawnMonstro();
                tempoDesdeUltimoSpawn = 0f;
            }
        }

        if (waveAtual >= 1 && mobsRestantesNaWave <= 0 && mobsVivos == 0 && waveAtual < waveMaxima && !contandoWave && waveEmAndamento)
        {
            contandoWave = true;
            contadorWave = tempoEntreWaves;
            proximaWaveText.gameObject.SetActive(true);
        }

        if (contandoWave)
        {
            contadorWave -= Time.deltaTime;
            int segundos = Mathf.CeilToInt(contadorWave);
            proximaWaveText.text = "Próxima Wave em: " + segundos;

            if (contadorWave <= 0f)
            {
                contandoWave = false;
                proximaWaveText.gameObject.SetActive(false);
                StartCoroutine(PausaEntreWaves());
            }
        }
    }

    void SpawnMonstro()
    {
        int indiceMonstro = Random.Range(0, monstros.Length);
        int indicePontoDeSpawn = Random.Range(0, pontosDeSpawn.Length);

        GameObject mob = Instantiate(monstros[indiceMonstro], pontosDeSpawn[indicePontoDeSpawn].position, Quaternion.identity);

        mobsVivos++;
        mobsRestantesNaWave--;

        MobHealth mobHealth = mob.GetComponent<MobHealth>();
        if (mobHealth != null)
        {
            mobHealth.spawner = this;
        }
    }

    public void MobMorto()
    {
        mobsVivos--;
    }

    IEnumerator IniciarWaveComDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        IniciarWave();
    }

    void IniciarWave()
    {
        waveEmAndamento = true;
        mobsRestantesNaWave = mobsPorWave;
        tempoDesdeUltimoSpawn = intervaloDeSpawn;
        proximaWaveText.gameObject.SetActive(false);
    }

    IEnumerator PausaEntreWaves()
    {
        waveEmAndamento = false;
        yield return new WaitForSeconds(0.5f);

        waveAtual++;
        mobsPorWave = Mathf.RoundToInt(mobsPorWave * 1.57f);
        intervaloDeSpawn *= 0.94f;

        if (waveAtual <= waveMaxima)
        {
            IniciarWave();
        }
    }
}