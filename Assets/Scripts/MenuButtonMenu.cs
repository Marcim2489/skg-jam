using UnityEngine;

public class MenuButtonMenu : MenuButton
{
    public override void Press()
    {
        SceneTransitionManager.Instance.ChangeScene("Menu");
    }
}
