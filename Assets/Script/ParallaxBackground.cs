using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public MeshRenderer[] backgrounds;   // Array of all the mesh renderers for parallax
    private float[] parallaxScales;      // The proportion of the camera's movement to move the backgrounds by
    public float smoothing = 1f;         // How smooth the parallax is going to be. Make sure to set this above 0

    private Transform cam;               // Reference to the main camera's transform
    private Vector3 previousCamPos;      // The position of the camera in the previous frame

    void Awake()
    {
        // Set up the camera reference
        cam = Camera.main.transform;
    }

    void Start()
    {
        // The previous frame had the current frame's camera position
        previousCamPos = cam.position;

        // Assigning corresponding parallaxScales
        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // Assuming the pivot point is centered, you may need to adjust if it's different
            parallaxScales[i] = backgrounds[i].sharedMaterial.mainTexture.width / backgrounds[i].transform.localScale.x * backgrounds[i].sharedMaterial.mainTexture.width * -1;
        }
    }

    void Update()
    {
        // For each background
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // The parallax is the opposite of the camera movement because the previous frame is multiplied by the scale
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            // Calculate target x position which is the current position plus the parallax
            float backgroundTargetPosX = backgrounds[i].transform.position.x + parallax;

            // Create a target position which is the background's current position with its target x position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].transform.position.y, backgrounds[i].transform.position.z);

            // Fade between current position and the target position using Lerp
            backgrounds[i].transform.position = Vector3.Lerp(backgrounds[i].transform.position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        // Set the previousCamPos to the camera's position at the end of the frame
        previousCamPos = cam.position;
    }
}
