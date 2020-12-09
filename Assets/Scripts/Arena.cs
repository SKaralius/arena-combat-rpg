using UnityEngine;
using UnityEngine.SceneManagement;

public class Arena : MonoBehaviour
{
    public void StartFight()
    {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(1);
    }
}