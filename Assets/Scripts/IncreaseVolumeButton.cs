using UnityEngine;

public class IncreaseVolumeButton : MenuButton
{

    public override void Press()
    {
        SoundSettings.Instance.IncreaseVolume();
    }
}