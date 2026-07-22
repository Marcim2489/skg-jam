using UnityEngine;
using UnityEngine.Audio;


public class SoundSettings : MonoBehaviour
{

    [Header("Mixer")]
    [SerializeField] private AudioMixer mixer;


    [Header("Dots")]
    [SerializeField] private VolumeDot[] dots;


    private int volumeLevel = 5;


    private float[] volumeValues =
    {
        -80f,
        -24f,
        -16f,
        -10f,
        -5f,
        0f
    };


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


        UpdateDots();
    }



    private void UpdateDots()
    {

        for(int i = 0; i < dots.Length; i++)
        {

            if(i < volumeLevel)
                dots[i].SetState(true);

            else
                dots[i].SetState(false);

        }

    }

}