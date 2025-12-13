using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    
    public void UpdateTimer(float timeRemaining)
    {
        if (timeRemaining > 0)
        {
            int seconds = Mathf.CeilToInt(timeRemaining);
            timerText.text = "Próxima Wave em: " + seconds;
        }
        else
        {
            timerText.text = "";
        }
    }
}