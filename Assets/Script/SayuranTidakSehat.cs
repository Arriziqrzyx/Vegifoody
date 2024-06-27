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
        }

        // Hancurkan objek setelah diklik
        Destroy(gameObject);

        // Periksa apakah semua sayuran telah dikumpulkan
        if (FoodManager.Instance != null)
        {
            FoodManager.Instance.SayuranCollected(); // Panggil untuk memeriksa apakah permainan menang
        }
    }
}