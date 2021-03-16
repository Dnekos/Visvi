using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    static public AudioSource sfxSource;
    public AudioSource musicSource;

    [SerializeField]
    AudioClip MenuMusic;
    [SerializeField]
    AudioClip LevelMusic;

    [SerializeField]
    static AudioClip ButtonSFX;
    [SerializeField]
    static AudioClip PickupSFX;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        sfxSource = GetComponentInChildren<AudioSource>();
        DontDestroyOnLoad(transform.gameObject);
        SceneManager.activeSceneChanged += ChangedActiveScene;

        PickupSFX = Resources.Load<AudioClip>("Sounds/pickup_sfx");
        ButtonSFX = Resources.Load<AudioClip>("Sounds/click_sfx");
    }

    public void PlayPickupSFX()
    {
        //if (!Application.isEditor)
            sfxSource.PlayOneShot(PickupSFX);
    }

    public void PlayButtonSFX()
    {
        //if (!Application.isEditor)
            sfxSource.PlayOneShot(ButtonSFX);
    }


    private void ChangedActiveScene(Scene current, Scene next)
    {

        switch (next.buildIndex)
        {
            case 0: // main menu
            case 1: // controls
            case 3: // credits
                if (!(musicSource.isPlaying && musicSource.clip == MenuMusic))
                {
                    musicSource.clip = MenuMusic;
                    musicSource.Play();
                }
                break;
            case 2: // main game
                musicSource.clip = LevelMusic;
                musicSource.Play();
                break;
        };

    }
}
