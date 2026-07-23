
public class MenuButtonPlay : MenuButton
{
    public override void Press()
    {
        // GameManager.Instance.StopAllMusic();
        GameManager.Instance.StartRun();
    }
}