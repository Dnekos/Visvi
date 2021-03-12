using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameState state = PlayerController.State;
    public GameObject pauseMenu;
    PlayerActions inputs;
    

    private void Awake()
    {
        inputs = new PlayerActions();
        inputs.Move.Pause.performed += ctx => PauseScene();
    }

    public void GoToScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ExitScene()
    {
        Application.Quit();
    }

    public void PauseScene()
    {
        if (state == GameState.Pause)
        {
            state = GameState.Pause;
            pauseMenu.SetActive(true);
        }
        else 
        {
            state = GameState.Play;
            pauseMenu.SetActive(false);
        }
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
