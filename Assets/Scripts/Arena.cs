using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Arena : MonoBehaviour
{
    public void StartFight()
    {
        SceneManager.LoadScene(0);
    }
}
