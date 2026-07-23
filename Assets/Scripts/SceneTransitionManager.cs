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

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(ChangeSceneCoroutine(sceneName));
    }

    public IEnumerator ChangeSceneCoroutine(string sceneName)
    {
        if (duringTransition)
        {
            yield break;
        }
        float time = Time.deltaTime;
        Time.timeScale = 0;
        duringTransition = true;
        enterTransition.gameObject.SetActive(true);
        enterTransition.localScale = Vector3.zero;
        while(enterTransition.localScale.x < 25)
        {
            enterTransition.localScale += new Vector3(delta, delta, delta) * time;
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
        while(enterTransition.localScale.x >= 0.1)
        {
            enterTransition.localScale -= new Vector3(delta, delta, delta) * time;
            if (enterTransition.localScale.x < 0)
            {
                break;
            }
            yield return null;
        }
        Time.timeScale = 1;
        enterTransition.localScale = Vector3.zero;
        enterTransition.gameObject.SetActive(false);
        duringTransition = false;
    }
}
