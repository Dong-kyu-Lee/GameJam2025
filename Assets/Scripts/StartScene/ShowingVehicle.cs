using UnityEngine;

public class ShowingVehicle : MonoBehaviour
{
    public float rotationSpeed = 10f; // ȸ�� �ӵ�
    // Update is called once per frame
    void Update()
    {
        // y���� �߽����� õõ�� ȸ���Ѵ�.
        transform.Rotate(0, 1 * rotationSpeed * Time.deltaTime, 0);
    }
}
