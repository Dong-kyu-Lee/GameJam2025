using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneUI : MonoBehaviour
{
    public AudioClip startSceneClip;

    private void Start()
    {
        SoundManager.Instance.PlaySound(startSceneClip);
    }
    public void MoveScene()
    {
        SceneManager.LoadScene("PlayScene");
    }
}
