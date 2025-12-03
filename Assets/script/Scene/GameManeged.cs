using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int mobBuffCount = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
    }

    public void ResetGame()
    {
        mobBuffCount = 0; // resetar buffs dos mobs
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Jogo reiniciado! Buffs dos mobs resetados.");
    }
}