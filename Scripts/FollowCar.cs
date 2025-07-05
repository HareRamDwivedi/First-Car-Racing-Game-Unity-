using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // Car Transform
    public Transform cameraPosition; // Camera holder/position reference

    [Header("Smooth Follow Settings")]
    public float followSpeed = 10f;
    public float rotationSpeed = 5f;

    [Header("Offset & LookAt")]
    public Vector3 offset = new Vector3(0f, 3f, -6f);
    public bool lookAtTarget = true;

    private Vector3 currentVelocity;

    void LateUpdate()
    {
        if (target == null || cameraPosition == null)
            return;

        // Smooth Position
        Vector3 desiredPosition = cameraPosition.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, 1f / followSpeed);

        // Smooth Rotation
        if (lookAtTarget)
        {
            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
