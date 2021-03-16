using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenu;
    PlayerActions inputs;
    

    private void Awake()
    {
        inputs = new PlayerActions();
        inputs.Move.Pause.performed += ctx => PauseScene();
    }

    public void GoToScene(int scene)
    {
        SoundManager.instance.PlayButtonSFX();
        SceneManager.LoadScene(scene);
    }

    public void ExitScene()
    {
        Application.Quit();
    }

    public void PauseScene()
    {
        if (!Application.isEditor)
            SoundManager.instance.PlayButtonSFX();
        if (PlayerController.State != GameState.Pause)
            pauseMenu.SetActive(true);
        else 
            pauseMenu.SetActive(false);
    }


    //these two are needed for the inputs to work
    private void OnEnable()
    {
        inputs.Move.Enable();
    }

    private void OnDisable()
    {
        inputs.Move.Disable();
    }
}
