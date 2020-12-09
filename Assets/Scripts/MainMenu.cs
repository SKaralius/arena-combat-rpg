using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Load town
        SceneManager.LoadScene(1);
        // Load player and player systems
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}