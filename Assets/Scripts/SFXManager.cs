using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance {get; private set;}

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

    public void PlaySound(AudioClip clip, float volume, bool sceneIndependent)
    {
        PlayClipAtPoint(clip, Vector2.zero, volume, sceneIndependent, 0f);
    }

    public void PlaySound(AudioClip clip, Vector2 position, float volume, bool sceneIndependent)
    {
        PlayClipAtPoint(clip, position, volume, sceneIndependent, 1f);
    }

    void PlayClipAtPoint(AudioClip clip, Vector3 position, float volume, bool sceneIndependent, float spatialBlend)
    {
        GameObject gameObject = new GameObject("One shot audio");
        gameObject.transform.position = position;
        AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
        audioSource.clip = clip;
        audioSource.spatialBlend = spatialBlend;
        audioSource.volume = volume;
        audioSource.Play();
        if (sceneIndependent)
        {
            DontDestroyOnLoad(gameObject);
        }
        Destroy(gameObject, clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale));
    }
}
