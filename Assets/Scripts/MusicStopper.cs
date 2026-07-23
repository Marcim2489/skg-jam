using UnityEngine;

public class MusicStopper : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.StopAllMusic();
    }
}
