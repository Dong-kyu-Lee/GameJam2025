using UnityEngine;

// �ڵ��� �÷��̾� ��Ʈ�ѷ�
public class PlayerController : MonoBehaviour
{
    public int HP = 100; // �÷��̾� HP
    public int MaxHP = 100;

    // �̵� �ӵ� �� ȸ�� �ӵ�
    public Rigidbody rb;
    public float speed = 10f;
    public float turnSpeed = 50f;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float move = Input.GetAxis("Vertical") * speed;
        float turn = Input.GetAxis("Horizontal") * turnSpeed;

        // �ڵ��� ����/����
        rb.MovePosition(rb.position + transform.forward * move * Time.fixedDeltaTime);

        // �ڵ��� ȸ��
        if(Mathf.Abs(move) > 0.01f) // �ڵ����� ������ ���� ȸ��
        {
            // ���� �� ȸ�� ���� ����
            float direction = move >= 0 ? 1f : -1f;
            Quaternion turnRotation = Quaternion.Euler(0f, turn * direction * Time.fixedDeltaTime, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }
}
