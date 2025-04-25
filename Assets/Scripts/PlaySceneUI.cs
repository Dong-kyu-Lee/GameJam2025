using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlaySceneUI : MonoBehaviour
{
    public Image hpBar;
    public TextMeshProUGUI scoreText;
    public AudioClip playSceneClip;

    void Start()
    {
        
    }

    public void UpdateHPBar(float hp)
    {
        hpBar.fillAmount = hp;
    }

    public void UpdateScoreText(int score)
    {
        if (scoreText == null)
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        }
        scoreText.text = "Score: " + score.ToString();
    }
}
