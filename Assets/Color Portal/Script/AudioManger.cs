using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource BackGround;
    public AudioSource SoundEffects;

    // Background Music
    public AudioClip bgMusic;

    // Sound Effects
    public AudioClip portalEnter;
    public AudioClip crystalCollected;
    public AudioClip playerWalk;
    public AudioClip playerJump;
    public AudioClip explosion;
    public AudioClip buttonClick;

    void Awake()
    {
        // Singleton pattern - only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persists across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate
        }
    }

    void Start()
    {
        PlayBGM(bgMusic);
    }

    public void PlayBGM(AudioClip clip)
    {
        if (clip == null) return;

        BackGround.clip = clip;
        BackGround.loop = true;
        BackGround.Play();
    }

    public void StopBGM()
    {
        BackGround.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        SoundEffects.PlayOneShot(clip);
    }

    // Convenient methods for specific sounds
    public void PlayPortalEnter()
    {
        PlaySFX(portalEnter);
    }

    public void PlayCrystalCollected()
    {
        PlaySFX(crystalCollected);
    }

    public void PlayPlayerWalk()
    {
        PlaySFX(playerWalk);
    }

    public void PlayPlayerJump()
    {
        PlaySFX(playerJump);
    }

    public void PlayExplosion()
    {
        PlaySFX(explosion);
    }

    public void PlayButtonClick()
    {
        PlaySFX(buttonClick);
    }
}