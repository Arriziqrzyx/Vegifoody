using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public float smoothSpeedX = 0.125f; // Smooth speed untuk sumbu X
    public float smoothSpeedY = 0.125f; // Smooth speed untuk sumbu Y
    public Vector3 offset; 

    private void LateUpdate()
    {
        if (target != null)
        {
            // Menghitung posisi yang diinginkan dengan offset
            Vector3 desiredPosition = target.position + offset;

            // Menghitung posisi smoothed terpisah untuk sumbu X dan Y
            Vector3 smoothedPosition = new Vector3(
                Mathf.Lerp(transform.position.x, desiredPosition.x, smoothSpeedX),
                Mathf.Lerp(transform.position.y, desiredPosition.y, smoothSpeedY),
                transform.position.z // Tidak perlu smoothing untuk sumbu Z, jika ingin mengikutinya tambahkan juga di sini
            );

            // Mengatur posisi kamera
            transform.position = smoothedPosition;
        }
    }
}
