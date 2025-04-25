using UnityEngine;

public class ShowingVehicle : MonoBehaviour
{
    public float rotationSpeed = 10f; // 회전 속도
    // Update is called once per frame
    void Update()
    {
        // y축을 중심으로 천천히 회전한다.
        transform.Rotate(0, 1 * rotationSpeed * Time.deltaTime, 0);
    }
}
