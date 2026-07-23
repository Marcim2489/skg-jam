using UnityEngine;

public class SwitchCharacterButton : MenuButton
{
    [SerializeField]int characterId = 0;

    public override void Press()
    {
        GameManager.Instance.SwitchCharacter(characterId);
    }
}
