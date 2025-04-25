using UnityEngine;

// 자동차 플레이어 컨트롤러
public class PlayerController : MonoBehaviour
{
    public int HP = 100; // 플레이어 HP
    public int MaxHP = 100;

    // 이동 속도 및 회전 속도
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

        // 자동차 전진/후진
        rb.MovePosition(rb.position + transform.forward * move * Time.fixedDeltaTime);

        // 자동차 회전
        if(Mathf.Abs(move) > 0.01f) // 자동차가 움직일 때만 회전
        {
            // 후진 시 회전 방향 반전
            float direction = move >= 0 ? 1f : -1f;
            Quaternion turnRotation = Quaternion.Euler(0f, turn * direction * Time.fixedDeltaTime, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }
}
