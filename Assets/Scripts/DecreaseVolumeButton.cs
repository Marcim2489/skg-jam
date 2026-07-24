using Unity.VisualScripting;
using UnityEngine;

public class DecreaseVolumeButton : MenuButton
{
    [SerializeField]AudioClip buttonSound;
    public override void Press()
    {
        SoundSettings.Instance.DecreaseVolume();
        SFXManager.Instance.PlaySound(buttonSound, 1f, false);
    }
}