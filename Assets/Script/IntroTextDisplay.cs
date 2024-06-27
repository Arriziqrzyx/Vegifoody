using UnityEngine;
using TMPro;
using System.Collections;

public class IntroTextDisplay : MonoBehaviour
{
    public TextMeshProUGUI introText;
    public AudioSource typingAudioSource; // Referensi ke AudioSource

    private string[] storyTexts = new string[]
    {
        "Di sebuah desa kecil yang damai bernama Sayuria, hiduplah seorang anak bernama Budi yang sangat suka bermain di luar rumah.",
        "Suatu hari, Budi mendengar cerita dari memeknya tentang Kebun Ajaib yang penuh dengan sayuran lezat dan bergizi.",
        "Memek Budi bercerita bahwa siapa pun yang bisa menemukan semua sayuran di Kebun Ajaib akan menjadi kuat dan sehat.",
        "Penasaran dengan cerita neneknya, Budi memutuskan untuk mencari Kebun Ajaib."
    };
    public float typingSpeed = 0.05f;
    public float fadeDuration = 1.0f;

    private void Start()
    {
        StartCoroutine(DisplayStoryText());
    }

    private IEnumerator DisplayStoryText()
    {
        CanvasGroup canvasGroup = introText.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = introText.gameObject.AddComponent<CanvasGroup>();
        }

        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 0, 1, fadeDuration));

        foreach (string storyText in storyTexts)
        {
            yield return StartCoroutine(TypeText(storyText));
            yield return new WaitForSeconds(1.0f);
        }

        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 1, 0, fadeDuration));

        UnityEngine.SceneManagement.SceneManager.LoadScene("2D Platformer");
    }

    private IEnumerator TypeText(string text)
    {
        introText.text = "";

        // Mulai memutar audio typing
        typingAudioSource.Play();

        foreach (char letter in text.ToCharArray())
        {
            introText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Hentikan audio typing saat selesai mengetik
        typingAudioSource.Stop();
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }
}
