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
    private GameObject explosionPrefab = null; // ���� ����Ʈ ������
    private bool isExploding = false; // ���� ����

    public AudioClip explosionClip;

    void Awake()
    {
        gameObject.SetActive(false); // ��Ȱ��ȭ ���·� ����
    }


    void Update()
    {
        if (isExploding == true) return;
        // ���� ��ġ���� destination �������� �̵�
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
        SoundManager.Instance.PlaySound(explosionClip); // ���� �Ҹ� ���
        Collider[] hitTargets = Physics.OverlapSphere(
            transform.position,
            attackRange,
            targetLayer
        );
        foreach (Collider target in hitTargets)
        {
            target.GetComponent<Enemy>()?.TakeDamage(damage);
            Debug.Log($"{target.name}���� ������ {damage} ����");
        }

        explosionPrefab.SetActive(true); // ���� ����Ʈ Ȱ��ȭ
        isExploding = true; // ���� ���·� ����
        StartCoroutine(DestroyCoroutine());
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(2f);
        isExploding = false; // ���� ���� ����
        explosionPrefab.SetActive(false); // ���� ����Ʈ ��Ȱ��ȭ
        gameObject.SetActive(false); // ��Ȱ��ȭ
    }
}
