using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonPlay : MenuButton
{
    public override void Press()
    {
        Debug.Log("BOTÃO PRESSIONADO");

        GameManager.Instance.StartRun(0);
    }
}