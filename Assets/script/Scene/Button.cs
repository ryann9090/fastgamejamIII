using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnButton : MonoBehaviour
{
    public string sceneName = "NomeDaCena"; 

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}