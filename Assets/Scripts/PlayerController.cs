using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// 자동차 플레이어 컨트롤러
public class PlayerController : MonoBehaviour
{
    public int HP = 100; // 플레이어 HP
    public int MaxHP = 100; // 최대 HP
    public float acceleration = 10f;   // 전진/후진 가속도
    public float maxSpeed = 20f;       // 최대 속도
    public float turnSpeed = 100f;     // 회전 속도
    public float attackcooldown = 1f; // 공격 쿨타임
    private bool isAttacking = false; // 공격 중인지 여부

    public Camera cam; // 카메라
    public GameObject deadExplosion; // 사망 이펙트 프리팹
    private Rigidbody rb;
    [SerializeField]
    private List<GameObject> bulletPrefabs = new List<GameObject>(); // 총알 프리팹
    public GameObject bulletPrefab; // 총알 프리팹
    private int index = 0;

    void Start()
    {
        HP = MaxHP; // HP 초기화
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
        // 입력 받기
        float moveInput = Input.GetAxis("Vertical");   // W/S 또는 ↑/↓
        float turnInput = Input.GetAxis("Horizontal"); // A/D 또는 ←/→

        // 현재 속도 구하기 (전진 방향 기준)
        Vector3 forwardVelocity = transform.forward * moveInput * acceleration;

        // 속도 제한
        Vector3 desiredVelocity = rb.linearVelocity + forwardVelocity * Time.fixedDeltaTime;
        if (desiredVelocity.magnitude > maxSpeed)
            desiredVelocity = desiredVelocity.normalized * maxSpeed;

        // 속도 적용
        rb.linearVelocity = new Vector3(desiredVelocity.x, rb.linearVelocity.y, desiredVelocity.z);

        // 회전
        float turn = turnInput * turnSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation;
        if (moveInput > 0) turnRotation = Quaternion.Euler(0f, turn, 0f);
        else turnRotation = Quaternion.Euler(0f, -turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    IEnumerator Attack()
    {
        // 스크린 좌표를 월드 좌표로 변환
        Ray ray = cam.ScreenPointToRay(Input.mousePosition); // 마우스 위치에서 레이 생성
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f)) // 레이가 충돌한 물체가 있는지 확인
        {
            bulletPrefabs[index].transform.position = transform.position;
            bulletPrefabs[index].GetComponent<Bullet>().destination = hit.point; // 총알 발사 방향 설정
            bulletPrefabs[index].SetActive(true); // 총알 활성화
        }
        
        index = (index + 1) % bulletPrefabs.Count; // 다음 총알 인덱스 설정
        yield return new WaitForSeconds(attackcooldown); // 공격 쿨타임 대기
        isAttacking = false; // 공격 상태 해제
    }

    IEnumerator Dead()
    {
        // 사망 이펙트 생성
        GameObject explosion = Instantiate(deadExplosion, transform.position, Quaternion.identity);
        explosion.SetActive(true); // 이펙트 활성화
        yield return new WaitForSeconds(2f); // 이펙트 지속 시간 대기
        gameObject.SetActive(false); // 플레이어 비활성화
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bound"))
        {
            Debug.Log("Player collided with another player!");
        }
        if(collision.gameObject.CompareTag("Police"))
        {
            
            HP -= collision.gameObject.GetComponent<Enemy>().damage; // HP 감소
            if (HP <= 0)
            {
                StartCoroutine(Dead()); // 사망 처리
            }
        }
    }
}
