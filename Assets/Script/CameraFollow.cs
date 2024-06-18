using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Transform target yang akan diikuti kamera
    public float smoothSpeed = 0.125f; // Kecepatan perpindahan kamera
    public Vector3 offset; // Jarak antara kamera dan target

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
