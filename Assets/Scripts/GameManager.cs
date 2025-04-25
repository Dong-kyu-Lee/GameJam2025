using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    static private GameManager instance;
    static public GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("GameManager");
                instance = go.AddComponent<GameManager>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    public GameObject player;
    public PlaySceneUI playSceneUI;
    public int maxScore;
    public int currentScore;

    void Awake()
    {
        currentScore = 0;
        // 싱글톤 패턴을 사용하여 GameManager 인스턴스 생성
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 플레이어 오브젝트 찾기
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player = new GameObject("Player");
            player.tag = "Player";
            player.AddComponent<PlayerController>();
        }
        else
        {
            playSceneUI = player.GetComponent<PlayerController>().playSceneUI;
        }
    }

    public void ResetGame()
    {
        // 게임 초기화
        currentScore = 0;
        playSceneUI.UpdateScoreText(currentScore);
        SceneManager.LoadScene("StartScene");
    }

    public void PlayerDead()
    {
        SceneManager.LoadScene("EndScene");
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        playSceneUI.UpdateScoreText(currentScore);
    }

    public void DestroySelf()
    {
        Destroy(instance.gameObject);
    }

    public void GameClear()
    {
        // 게임 클리어 처리
        SceneManager.LoadScene("ClearScene");
    }
}
