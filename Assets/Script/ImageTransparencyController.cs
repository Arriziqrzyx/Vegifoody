using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageTransparencyController : MonoBehaviour
{
    public Image imageToFade; // Assign image yang akan diubah transparansinya
    public float fadeDuration = 1.0f; // Durasi transisi dalam detik
    private Coroutine currentFadeCoroutine;
    private PlayerController playerController;
    private PlayerController2 playerController2;

    public void Start()
    {
        playerController = FindObjectOfType<PlayerController>(); 
        playerController2 = FindObjectOfType<PlayerController2>();
        SetImageAlpha(imageToFade, 1f);
        // Memulai coroutine untuk mengubah transparansi dari 1 ke 0
        StartCoroutine(FadeImage(imageToFade, 1f, 0f, fadeDuration));
    }

    private IEnumerator FadeImage(Image image, float startAlpha, float targetAlpha, float duration)
    {
        yield return new WaitForSeconds(0.3f);
        float currentTime = 0f;
        Color currentColor = image.color;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, currentTime / duration);
            currentColor.a = alpha;
            image.color = currentColor;
            yield return null;
        }

        // Pastikan alpha mencapai nilai target tepat pada akhir coroutine
        currentColor.a = targetAlpha;
        image.color = currentColor;
    }

    private void SetImageAlpha(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }

    public void ResetDelay()
    {
        StartCoroutine(DelayedReset());
        imageToFade.raycastTarget = true;
    }
    
    private IEnumerator DelayedReset()
    {
        yield return new WaitForSeconds(1.0f);
        ResetTransparency();
    }

    public void ResetTransparency()
    {
        if (currentFadeCoroutine != null)
        {
            StopCoroutine(currentFadeCoroutine);
        }
        imageToFade.raycastTarget = true;
        if (playerController != null)
        {
            playerController.enabled = false;
        }
        if (playerController2 != null)
        {
            playerController2.enabled = false;
        }
        StartCoroutine(FadeAndReset());
    }

    private IEnumerator FadeAndReset()
    {
        yield return FadeImage(imageToFade, imageToFade.color.a, 1f, 1.0f);
        yield return new WaitForSeconds(1.0f); 
        StartCoroutine(FadeImage(imageToFade, imageToFade.color.a, 0f, 0f));
        imageToFade.raycastTarget = false;
        if (playerController != null)
        {
            playerController.enabled = true;
        }
        if (playerController2 != null)
        {
            playerController2.enabled = true;
        }
    }
}
