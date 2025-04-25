using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 destination;
    public float speed = 2f;
    public int damage = 10;
    public float attackRange = 3f;
    public LayerMask targetLayer;
    [SerializeField]
    private GameObject explosionPrefab = null; // 폭발 이펙트 프리팹
    private bool isExploding = false; // 폭발 여부

    public AudioClip explosionClip;

    void Awake()
    {
        gameObject.SetActive(false); // 비활성화 상태로 시작
    }


    void Update()
    {
        if (isExploding == true) return;
        // 현재 위치에서 destination 방향으로 이동
        if ((transform.position - destination).magnitude > 0.3f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * 10f * speed);
        }
        else
        {
            Explosion();
        }
    }

    void Explosion()
    {
        SoundManager.Instance.PlaySound(explosionClip); // 폭발 소리 재생
        Collider[] hitTargets = Physics.OverlapSphere(
            transform.position,
            attackRange,
            targetLayer
        );
        foreach (Collider target in hitTargets)
        {
            target.GetComponent<Enemy>()?.TakeDamage(damage);
            Debug.Log($"{target.name}에게 데미지 {damage} 적용");
        }

        explosionPrefab.SetActive(true); // 폭발 이펙트 활성화
        isExploding = true; // 폭발 상태로 변경
        StartCoroutine(DestroyCoroutine());
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(2f);
        isExploding = false; // 폭발 상태 해제
        explosionPrefab.SetActive(false); // 폭발 이펙트 비활성화
        gameObject.SetActive(false); // 비활성화
    }
}
