using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    void Start()
    {
        SoundManager.Instance.PlayBGM(0);
        scoreText.text = $"Score: {GameManager.Instance.currentScore}";
    }

    public void MoveScene()
    {
        GameManager.Instance.DestroySelf();
        SceneManager.LoadScene("StartScene");
    }
}
