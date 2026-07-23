using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;


public class SoundSettings : MonoBehaviour
{
    public static SoundSettings Instance {get; private set;}

    [Header("Mixer")]
    [SerializeField] private AudioMixer mixer;

    private int volumeLevel = 3;
    public int VolumeLevel => volumeLevel;

    private float[] volumeValues =
    {
        -80f,
        -24f,
        -16f,
        -10f,
        -5f,
        0f
    };
    public float[] VolumeValues => volumeValues;

    public event UnityAction volumeUpdated = delegate {};

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    void Start()
    {
        UpdateVolume();
    }

    public void IncreaseVolume()
    {
        if(volumeLevel < 5)
        {
            volumeLevel++;
            UpdateVolume();
        }
    }

    public void DecreaseVolume()
    {
        if(volumeLevel > 0)
        {
            volumeLevel--;
            UpdateVolume();
        }
    }

    private void UpdateVolume()
    {
        mixer.SetFloat(
            "MasterVolume",
            volumeValues[volumeLevel]
        );
        volumeUpdated.Invoke();
    }

}