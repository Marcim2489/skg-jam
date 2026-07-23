using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonRankings : MenuButton
{
    public override void Press()
    {
      SceneManager.LoadScene("Leaderboard");
    }
}
