using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void GoToScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ExitScene()
    {
        Application.Quit();
    }
}
