using UnityEngine;

public class ParallaxFollow : MonoBehaviour
{
    public Transform target; 
    public float smoothSpeedX = 0.125f; 
    public float smoothSpeedY = 0.125f; 
    public Vector3 offset; 

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;

            // Apply smoothing separately for x and y
            float smoothedPositionX = Mathf.Lerp(transform.position.x, desiredPosition.x, smoothSpeedX);
            float smoothedPositionY = Mathf.Lerp(transform.position.y, desiredPosition.y, smoothSpeedY);
            float smoothedPositionZ = Mathf.Lerp(transform.position.z, desiredPosition.z, smoothSpeedX); // Assuming Z-axis smoothing uses smoothSpeedX for simplicity

            transform.position = new Vector3(smoothedPositionX, smoothedPositionY, smoothedPositionZ);
        }
    }
}
