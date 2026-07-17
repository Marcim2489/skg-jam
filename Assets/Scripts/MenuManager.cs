using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void Play()
    {
        GameManager.Instance.StartRun();
    }
}
