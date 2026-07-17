using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void Retry()
    {
        GameManager.Instance.StartRun();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
