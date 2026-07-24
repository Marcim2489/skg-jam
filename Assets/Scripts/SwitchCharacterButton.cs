using UnityEngine;

public class SwitchCharacterButton : MenuButton
{
    [SerializeField]int characterId = 0;
    [SerializeField]AudioClip buttonSound;

    public override void Press()
    {
        GameManager.Instance.SwitchCharacter(characterId);
        SFXManager.Instance.PlaySound(buttonSound, 1f, false);
    }
}
