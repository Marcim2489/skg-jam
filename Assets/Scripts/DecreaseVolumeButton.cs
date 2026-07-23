using UnityEngine;

public class DecreaseVolumeButton : MenuButton
{

    public override void Press()
    {
        SoundSettings.Instance.DecreaseVolume();
    }
}