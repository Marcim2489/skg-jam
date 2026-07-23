using UnityEngine;

public class SoundDotsManager : MonoBehaviour
{
    [SerializeField] private VolumeDot[] dots;

    void Start()
    {
        SoundSettings.Instance.volumeUpdated+=UpdateDots;
        UpdateDots();
    }

    private void UpdateDots()
    {
        for(int i = 0; i < dots.Length; i++)
        {
            if(i < SoundSettings.Instance.VolumeLevel)
                dots[i].SetState(true);
            else
                dots[i].SetState(false);
        }
    }

    void OnDestroy()
    {
        SoundSettings.Instance.volumeUpdated-=UpdateDots;
    }
}
