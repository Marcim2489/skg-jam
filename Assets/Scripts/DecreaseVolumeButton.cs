using UnityEngine;

public class DecreaseVolumeButton : MenuButton
{
    [SerializeField] private SoundSettings soundSettings;

    public override void Press()
    {
        soundSettings.DecreaseVolume();
    }
}