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
        // �̱��� ������ ����Ͽ� GameManager �ν��Ͻ� ����
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
        // �÷��̾� ������Ʈ ã��
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
        // ���� �ʱ�ȭ
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
        // ���� Ŭ���� ó��
        SceneManager.LoadScene("ClearScene");
    }
}
