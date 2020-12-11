using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Load town
        SceneManager.LoadScene("TownScene");
        // Load player and player systems
        SceneManager.LoadSceneAsync("NeverUnload", LoadSceneMode.Additive);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}