using UnityEngine;

public class MenuButtonMenu : MenuButton
{
    public override void Press()
    {
        GameManager.Instance.StopAllMusic();
        SceneTransitionManager.Instance.ChangeScene("Menu");
    }
}
