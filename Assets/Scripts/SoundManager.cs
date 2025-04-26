using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource bgmSource;
    public AudioClip[] bgmClips; // 0: StartSccene, EndScene, ClearScene �� BGM, 1: PlayScene�� BGM ��

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(int bgmIndex)
    {
        if (bgmSource != null)
        {
            if (bgmSource.clip == bgmClips[bgmIndex] && bgmSource.isPlaying)
                return; // �̹� ��� ���̸� �н�
        }
        StopBGM();

        bgmSource.clip = bgmClips[bgmIndex];
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        if(bgmSource != null && bgmSource.isPlaying)
            bgmSource.Stop();
    }

    public void PlaySound(AudioClip clip)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(audioSource, clip.length);
    }
}
