using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageTransparencyController2 : MonoBehaviour
{
    public Image imageToFade; // Assign image yang akan diubah transparansinya
    private Coroutine currentFadeCoroutine;

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
    public void ResetTransparency()
    {
        if (currentFadeCoroutine != null)
        {
            StopCoroutine(currentFadeCoroutine);
        }
        StartCoroutine(FadeAndReset());
    }

    private IEnumerator FadeAndReset()
    {
        yield return FadeImage(imageToFade, imageToFade.color.a, 1f, 1.0f);
        yield return new WaitForSeconds(1.0f); 
        StartCoroutine(FadeImage(imageToFade, imageToFade.color.a, 0f, 0f));
    }
}
