using UnityEngine;

public class SayuranTidakSehat : MonoBehaviour
{
    // Fungsi ini akan dipanggil ketika sayuran diklik
    private void OnMouseDown()
    {
        FoodManager.Instance.UpdateScore(-5); // Kurangi skor

        // Hancurkan objek setelah diklik
        Destroy(gameObject);
    }
}
