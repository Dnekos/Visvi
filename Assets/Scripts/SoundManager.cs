using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{

    static public AudioSource sfxSource;
    public AudioSource musicSource;

    [SerializeField]
    AudioClip MenuMusic;
    [SerializeField]
    AudioClip LevelMusic;

  //  [SerializeField]
   // static AudioClip ButtonSFX;
    [SerializeField]
    static AudioClip PickupSFX;

    private void Awake()
    {

        sfxSource = GetComponentInChildren<AudioSource>();
        DontDestroyOnLoad(transform.gameObject);
        SceneManager.activeSceneChanged += ChangedActiveScene;

        PickupSFX = Resources.Load<AudioClip>("Sounds/pickup_sfx");
      //  ButtonSFX = Resources.Load<AudioClip>("Sounds/button_sfx");
    }

    static public void PlayPickupSFX()
    {
        sfxSource.PlayOneShot(PickupSFX);
    }

    static public void PlayButtonSFX()
    {
    //    sfxSource.PlayOneShot(ButtonSFX);
    }


    private void ChangedActiveScene(Scene current, Scene next)
    {

        switch (next.buildIndex)
        {
            case 0: // main menu
            case 2: // credits
                if (!(musicSource.isPlaying && musicSource.clip == MenuMusic))
                {
                    musicSource.clip = MenuMusic;
                    musicSource.Play();
                }
                break;
            case 1:
                musicSource.clip = LevelMusic;
                musicSource.Play();
                break;
        };

    }
}
