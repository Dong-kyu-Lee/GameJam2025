using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneUI : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.PlayBGM(0);
    }
    public void MoveScene()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
