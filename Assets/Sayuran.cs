using UnityEngine;
using UnityEngine.UI;

public class Sayuran : MonoBehaviour
{
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
            checkImage.gameObject.SetActive(true); // Aktifkan gambar check
        }
        FoodManager.Instance.UpdateScore(10); // Tambah skor

        // Hancurkan objek setelah diklik
        Destroy(gameObject);
    }
}
