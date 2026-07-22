using UnityEngine;

public class IncreaseVolumeButton : MenuButton
{
    [SerializeField] private SoundSettings soundSettings;

    public override void Press()
    {
        soundSettings.IncreaseVolume();
    }
}