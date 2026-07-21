using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [SerializeField]InputAction pauseInput;
    [SerializeField]GameObject pausePanel;
    bool paused = false;

    void Start()
    {
        pauseInput.Enable();
        pauseInput.started+=PauseGame;
        LevelManager.Instance.timeUp+=DisablePanel;
        DisablePanel();
    }

    void DisablePanel()
    {
        pausePanel.SetActive(false);
    }

    public void GiveUp()
    {
        Time.timeScale = 1;
        GameManager.Instance.EndRun();
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        paused = false;
        DisablePanel();
    }

    void PauseGame(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 0 && paused == false)
        {
            return;
        }
        if (paused)
        {
            Unpause();
        }
        else
        {
            Time.timeScale = 0;
            paused = true;
            pausePanel.SetActive(true);
        }
    }

    void OnDestroy()
    {
        pauseInput.started-=PauseGame;
        // LevelManager.Instance.timeUp-=DisablePanel;
    }
}
