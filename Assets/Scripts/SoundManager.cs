using System;
using UnityEngine;

public enum SoundType
{
    SHOOT, ENEMYHIT, ENEMYDEAD
}

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundClip[] soundClips;

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
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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

#if UNITY_EDITOR
    private void OnEnable()
    {
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