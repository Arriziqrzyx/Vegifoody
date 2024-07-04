using UnityEngine;
using TMPro;
using System.Collections;

public class TextLoading : MonoBehaviour
{
    public TMP_Text loadingText;
    public float blinkDuration = 1.0f;
    private bool isBlinking = true;

    void Start()
    {
        if (loadingText != null)
        {
            StartCoroutine(BlinkText());
        }
    }

    IEnumerator BlinkText()
    {
        while (isBlinking)
        {
            // Fade out
            yield return StartCoroutine(FadeTextToZeroAlpha(blinkDuration / 2));
            // Fade in
            yield return StartCoroutine(FadeTextToFullAlpha(blinkDuration / 2));
        }
    }

    public IEnumerator FadeTextToFullAlpha(float t)
    {
        loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, 0);
        while (loadingText.color.a < 1.0f)
        {
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, loadingText.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t)
    {
        loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, 1);
        while (loadingText.color.a > 0.0f)
        {
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, loadingText.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
