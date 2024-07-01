using UnityEngine;

public class SayuranTidakSehat : MonoBehaviour
{
    [SerializeField] AudioSource pencetAudio;

    // Fungsi ini akan dipanggil ketika sayuran diklik
    private void OnMouseDown()
    {
        if (FoodManager.Instance != null)
        {
            pencetAudio.Play();
            FoodManager.Instance.UpdateHealth(-1); // Kurangi kesehatan
            FoodManager.Instance.ReduceTimer(5f);
        }

        // Hancurkan objek setelah diklik
        Destroy(gameObject);
    }
}
