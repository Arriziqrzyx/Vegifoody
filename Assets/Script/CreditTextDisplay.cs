using UnityEngine;
using TMPro;
using System.Collections;

public class CreditTextDisplay : MonoBehaviour
{
    public TextMeshProUGUI introText; 
    public AudioSource typingAudioSource; // Referensi ke AudioSource
    private string[] storyTexts = new string[]
    {
        "Setelah melewati berbagai rintangan dan tantangan, Budi akhirnya berhasil mengumpulkan semua sayuran sehat di kebun ajaib.",
        "Sayuran-sehat itu tidak hanya memberikan kekuatan, tetapi juga wawasan baru tentang pentingnya kesehatan dan gizi.",
        "Dengan ketekunan dan ketelitiannya, dia mampu menyelesaikan ujian dari Mewing & Skibidi dan mendapatkan kepercayaan dari Alok.",
        "Alok, Mewing, dan Skibidi tersenyum bangga melihat perjuangan Budi. Kebun ajaib kini menjadi tempat yang lebih hidup dan harmonis berkat usaha keras Budi serta kehadiran sayuran-sehat yang membawa energi baru.",
        "Di akhir petualangannya, Budi belajar bahwa dengan kerja keras, keberanian, dan ketekunan, tidak ada rintangan yang terlalu besar untuk diatasi.",
        "Kebun ajaib pun kini menjadi simbol keberhasilan dan ketekunan, yang akan selalu menginspirasi Budi dalam setiap langkah kehidupannya.",
        ".............................T  A  M  A  T............................."
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

        
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
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
