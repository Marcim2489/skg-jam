using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    public static BackgroundMusicPlayer Instance {get; private set;}

    public AudioSource MusicPlayer => musicPlayer;
    [SerializeField]AudioSource musicPlayer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        if (Instance != this && Instance != null && Instance.MusicPlayer.clip.Equals(musicPlayer.clip))
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // SoundSettings.Instance.volumeUpdated+=UpdateVolume;
        // UpdateVolume();
    }

    void UpdateVolume()
    {
        // musicPlayer.volume = SoundSettings.Instance.VolumeValues[SoundSettings.Instance.VolumeLevel];
    }

    void OnDestroy()
    {
        // SoundSettings.Instance.volumeUpdated-=UpdateVolume;
    }
}
