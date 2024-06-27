using UnityEngine;
using UnityEngine.UI;

public class Sayuran : MonoBehaviour
{
    [SerializeField] AudioSource pencetAudio;
    public Image checkImage; // Gambar check yang akan diaktifkan jika sayuran sehat

    private void Start()
    {
        // Pastikan checkImage tidak aktif pada awalnya
        if (checkImage != null)
        {
            checkImage.gameObject.SetActive(false);
        }
    }

    // Fungsi ini akan dipanggil ketika sayuran diklik
    private void OnMouseDown()
    {
        if (checkImage != null)
        {
            pencetAudio.Play();
            checkImage.gameObject.SetActive(true); // Aktifkan gambar check
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
