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
    }

    void Update()
    {
        
    }
}
