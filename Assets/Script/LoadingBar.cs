using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingBar : MonoBehaviour
{
    public Image targetImage;
    public float duration = 3.0f;

    // Delegate untuk callback saat pengisian selesai
    public delegate void FillComplete();
    public event FillComplete OnFillComplete;

    void Start()
    {
        StartCoroutine(FillImageOverTime(targetImage, duration));
    }

    IEnumerator FillImageOverTime(Image image, float duration)
    {
        // Tunggu 1 detik sebelum memulai pengisian
        yield return new WaitForSeconds(1f);

        float elapsedTime = 0f;
        float startFillAmount = image.fillAmount;
        float endFillAmount = 1f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            image.fillAmount = Mathf.Lerp(startFillAmount, endFillAmount, elapsedTime / duration);
            yield return null;
        }

        // Pastikan fill amount disetel ke 1 pada akhir
        image.fillAmount = endFillAmount;

        // Panggil callback saat pengisian selesai
        OnFillComplete?.Invoke();
    }
}
