using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip buttonClickClip;

    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("SoundManager");
                instance = obj.AddComponent<SoundManager>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = GetComponent<SoundManager>();
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlaySound(AudioClip clip, bool loop = false)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
        audioSource.loop = loop;
        Destroy(audioSource, clip.length); // Destroy the AudioSource after the clip has finished playing
    }

    public void ButtonClickSound(AudioClip clip)
    {

    }
}
