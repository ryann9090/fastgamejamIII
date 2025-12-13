using UnityEngine;
using TMPro; // se estiver usando TextMeshPro

public class WaveNumber : MonoBehaviour
{
    public TextMeshProUGUI waveText; // arraste o objeto de texto no Inspector
    private int waveAtual = 1;
    private int waveMaxima = 10;

    void Start()
    {
        AtualizarWaveUI();
    }

    // ? Chame este método quando começar uma nova wave
    public void ProximaWave()
    {
        if (waveAtual < waveMaxima)
        {
            waveAtual++;
            AtualizarWaveUI();
        }
    }

    private void AtualizarWaveUI()
    {
        waveText.text = "Wave " + waveAtual;
    }
}