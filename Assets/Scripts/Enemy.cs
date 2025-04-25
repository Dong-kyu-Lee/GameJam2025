using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int HP = 100; // 적 HP
    public int MaxHP = 100; // 최대 HP
    public int damage = 10; // 적 공격력
    public Transform target = null;
    public float moveSpeed = 8f;
    public float rotationSpeed = 120f;

    [SerializeField]
    private EnemySpawner spawner = null;
    private NavMeshAgent agent;

    void Start()
    {
        target = GameManager.Instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.angularSpeed = rotationSpeed;
        agent.acceleration = 15f; // 가속도 설정
        agent.stoppingDistance = 1f;
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);

            // 수동으로 회전 각도 조정(옵션)
            if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
            {
                Quaternion targetRotation = Quaternion.LookRotation(agent.velocity.normalized);
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(EnemySpawner spawner)
    {
        this.spawner = spawner;
    }

    void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.OnEnemyDestroyed();
        }
    }
}
