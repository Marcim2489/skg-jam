using UnityEngine;

public class MenuButtonMenu : MenuButton
{
    [SerializeField]AudioClip buttonSound;
    public override void Press()
    {
        GameManager.Instance.StopAllMusic();
        SFXManager.Instance.PlaySound(buttonSound, 1f, true);
        SceneTransitionManager.Instance.ChangeScene("Menu");
    }
}
