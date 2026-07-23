using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance {get; private set;}

    [SerializeField]Transform enterTransition;
    [SerializeField]float delta = 1f;
    // [SerializeField]Animator leaveTransition;
    bool duringTransition = false;
    public bool DuringTransition => duringTransition;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    public IEnumerator ChangeScene(string sceneName)
    {
        if (duringTransition)
        {
            yield break;
        }
        duringTransition = true;
        enterTransition.gameObject.SetActive(true);
        enterTransition.localScale = Vector3.zero;
        while(enterTransition.localScale.x < 25)
        {
            enterTransition.localScale += new Vector3(delta, delta, delta);
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
        while(enterTransition.localScale.x >= 0.1)
        {
            enterTransition.localScale -= new Vector3(delta, delta, delta);
            if (enterTransition.localScale.x < 0)
            {
                break;
            }
            yield return null;
        }
        enterTransition.localScale = Vector3.zero;
        enterTransition.gameObject.SetActive(false);
        duringTransition = false;
    }
}
