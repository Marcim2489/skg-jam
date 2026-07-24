using UnityEngine;

public class IncreaseVolumeButton : MenuButton
{
    [SerializeField]AudioClip buttonSound;
    public override void Press()
    {
        SoundSettings.Instance.IncreaseVolume();
        SFXManager.Instance.PlaySound(buttonSound, 1f, false);
    }
}