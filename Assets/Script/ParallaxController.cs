using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public Transform player; // Referensi ke transform player

    [SerializeField, Range(0f, 0.5f)]
    private float[] speeds; // Array kecepatan parallax untuk setiap layer

    private Material[] mats; // Array material untuk setiap layer
    private Vector3 lastPlayerPosition; // Posisi terakhir player

    void Start()
    {
        // Mendapatkan komponen material dari setiap layer parallax
        mats = new Material[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            mats[i] = transform.GetChild(i).GetComponent<Renderer>().material;
        }

        lastPlayerPosition = player.position; // Inisialisasi posisi terakhir player
    }

    void Update()
    {
        // Hitung pergerakan pemain dari posisi terakhir
        float playerMovement = player.position.x - lastPlayerPosition.x;
        
        // Update posisi terakhir pemain
        lastPlayerPosition = player.position;

        // Update parallax untuk setiap layer
        for (int i = 0; i < mats.Length; i++)
        {
            float parallaxMovement = playerMovement * speeds[i]; // Hitung pergerakan parallax
            float newOffsetX = mats[i].mainTextureOffset.x + parallaxMovement; // Hitung offset baru
            Vector2 newOffset = new Vector2(newOffsetX, mats[i].mainTextureOffset.y);
            mats[i].mainTextureOffset = newOffset; // Set offset baru ke material
        }
    }
}
