using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    void Start()
    {
        Invoke("TrocarCena", 5f); 
    }

    void TrocarCena()
    {
        SceneManager.LoadScene("jogo"); 
    }
}