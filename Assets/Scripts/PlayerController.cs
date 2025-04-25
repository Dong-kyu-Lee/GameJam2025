using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// �ڵ��� �÷��̾� ��Ʈ�ѷ�
public class PlayerController : MonoBehaviour
{
    public int HP = 100; // �÷��̾� HP
    public int MaxHP = 100; // �ִ� HP
    public float acceleration = 10f;   // ����/���� ���ӵ�
    public float maxSpeed = 20f;       // �ִ� �ӵ�
    public float turnSpeed = 100f;     // ȸ�� �ӵ�
    public float attackcooldown = 1f; // ���� ��Ÿ��
    private bool isAttacking = false; // ���� ������ ����

    public Camera cam; // ī�޶�
    public GameObject deadExplosion; // ��� ����Ʈ ������
    private Rigidbody rb;
    [SerializeField]
    private List<GameObject> bulletPrefabs = new List<GameObject>(); // �Ѿ� ������
    public GameObject bulletPrefab; // �Ѿ� ������
    private int index = 0;

    void Start()
    {
        HP = MaxHP; // HP �ʱ�ȭ
        rb = GetComponent<Rigidbody>();
        for(int i = 0; i < 6; i++)
        {
            bulletPrefabs.Add(Instantiate(bulletPrefab, transform.position, Quaternion.identity));
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            StartCoroutine(Attack());
        }
    }

    void FixedUpdate()
    {
        // �Է� �ޱ�
        float moveInput = Input.GetAxis("Vertical");   // W/S �Ǵ� ��/��
        float turnInput = Input.GetAxis("Horizontal"); // A/D �Ǵ� ��/��

        // ���� �ӵ� ���ϱ� (���� ���� ����)
        Vector3 forwardVelocity = transform.forward * moveInput * acceleration;

        // �ӵ� ����
        Vector3 desiredVelocity = rb.linearVelocity + forwardVelocity * Time.fixedDeltaTime;
        if (desiredVelocity.magnitude > maxSpeed)
            desiredVelocity = desiredVelocity.normalized * maxSpeed;

        // �ӵ� ����
        rb.linearVelocity = new Vector3(desiredVelocity.x, rb.linearVelocity.y, desiredVelocity.z);

        // ȸ��
        float turn = turnInput * turnSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation;
        if (moveInput > 0) turnRotation = Quaternion.Euler(0f, turn, 0f);
        else turnRotation = Quaternion.Euler(0f, -turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    IEnumerator Attack()
    {
        // ��ũ�� ��ǥ�� ���� ��ǥ�� ��ȯ
        Ray ray = cam.ScreenPointToRay(Input.mousePosition); // ���콺 ��ġ���� ���� ����
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f)) // ���̰� �浹�� ��ü�� �ִ��� Ȯ��
        {
            bulletPrefabs[index].transform.position = transform.position;
            bulletPrefabs[index].GetComponent<Bullet>().destination = hit.point; // �Ѿ� �߻� ���� ����
            bulletPrefabs[index].SetActive(true); // �Ѿ� Ȱ��ȭ
        }
        
        index = (index + 1) % bulletPrefabs.Count; // ���� �Ѿ� �ε��� ����
        yield return new WaitForSeconds(attackcooldown); // ���� ��Ÿ�� ���
        isAttacking = false; // ���� ���� ����
    }

    IEnumerator Dead()
    {
        // ��� ����Ʈ ����
        GameObject explosion = Instantiate(deadExplosion, transform.position, Quaternion.identity);
        explosion.SetActive(true); // ����Ʈ Ȱ��ȭ
        yield return new WaitForSeconds(2f); // ����Ʈ ���� �ð� ���
        gameObject.SetActive(false); // �÷��̾� ��Ȱ��ȭ
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bound"))
        {
            Debug.Log("Player collided with another player!");
        }
        if(collision.gameObject.CompareTag("Police"))
        {
            
            HP -= collision.gameObject.GetComponent<Enemy>().damage; // HP ����
            if (HP <= 0)
            {
                StartCoroutine(Dead()); // ��� ó��
            }
        }
    }
}
