using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestart : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    void RestartGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Jogo reiniciado!");
    }
}