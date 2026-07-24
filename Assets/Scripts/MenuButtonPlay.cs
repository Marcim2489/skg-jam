using UnityEngine;

public class MenuButtonPlay : MenuButton
{
    [SerializeField]AudioClip buttonSound;
    public override void Press()
    {
        // GameManager.Instance.StopAllMusic();
        SFXManager.Instance.PlaySound(buttonSound, 1f, true);
        GameManager.Instance.StartRun();
        
    }
}