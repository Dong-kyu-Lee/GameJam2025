using UnityEngine;

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

    void Awake()
    {
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
    }

    void Update()
    {
        
    }
}
