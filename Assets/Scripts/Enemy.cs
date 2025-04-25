using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 8f;
    public float rotationSpeed = 120f;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.angularSpeed = rotationSpeed;
        agent.acceleration = 15f; // ���ӵ� ����
        agent.stoppingDistance = 1f;
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);

            // �������� ȸ�� ���� ����(�ɼ�)
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
}
