using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SoundType
{
    SHOOT, ENEMYHIT, ENEMYDEAD
}

public enum MusicType
{
    MAINMENU, GAMEPLAY
}

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundClip[] soundClips;
    [SerializeField] AudioClip mainMenuMusic;
    [SerializeField] AudioClip gamePlayMusic;

    string mainMenuScene = "Main_Menu";
    string gameplayScene = "Gameplay";

    private static SoundManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == mainMenuScene)
        {
            audioSource.clip = mainMenuMusic;
        }
        else if (scene.name == gameplayScene)
        {
            audioSource.clip = gamePlayMusic;
        }
            audioSource.loop = true;
            audioSource.Play();
    }

    public static void PlaySound(SoundType sound, float volume = 1f)
    {
        if (instance == null)
            return;

        AudioClip[] clips = instance.soundClips[(int)sound].Sounds;

        if (clips == null || clips.Length == 0)
            return;

        AudioClip randomClip =
            clips[UnityEngine.Random.Range(0, clips.Length)];

        instance.audioSource.PlayOneShot(randomClip, volume);
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        
#if UNITY_EDITOR
        string[] names = Enum.GetNames(typeof(SoundType));

        Array.Resize(ref soundClips, names.Length);

        for (int i = 0; i < soundClips.Length; i++)
        {
            soundClips[i].name = names[i];
        }
    }
#endif
}

[Serializable]
public struct SoundClip
{
    [HideInInspector]
    [SerializeField]
    public string name;

    [SerializeField]
    private AudioClip[] clip;

    public AudioClip[] Sounds => clip;
}