using UnityEngine;
using UnityEngine.SceneManagement;

public class Arena : MonoBehaviour
{
    public void StartFight()
    {
        SceneManager.LoadSceneAsync("CombatScene", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("TownScene");
    }
}