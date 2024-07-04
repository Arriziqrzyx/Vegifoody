using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public GameObject buttonSkip;
    private const string dialogCreditKey = "DialogCreditPassed"; // Key untuk PlayerPrefs

    private Coroutine currentTypingCoroutine;
    private bool skipTyping = false;

    private void Start()
    {
        buttonSkip.SetActive(false);

        // Cek PlayerPrefs untuk status dialog
        if (PlayerPrefs.GetInt(dialogCreditKey, 0) == 1)
        {
            buttonSkip.SetActive(true); // Jika dialog sudah pernah dilakukan, aktifkan tombol skip
        }

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
            if (skipTyping)
            {
                introText.text = storyText;
                break;
            }

            currentTypingCoroutine = StartCoroutine(TypeText(storyText));
            yield return currentTypingCoroutine;
            yield return new WaitForSeconds(1.0f);
        }

        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 1, 0, fadeDuration));

        // Simpan status dialog ke PlayerPrefs
        PlayerPrefs.SetInt(dialogCreditKey, 1);
        SceneManager.LoadScene("Menu");
    }

    private IEnumerator TypeText(string text)
    {
        introText.text = "";

        // Mulai memutar audio typing
        typingAudioSource.Play();

        foreach (char letter in text.ToCharArray())
        {
            if (skipTyping)
            {
                introText.text = text;
                typingAudioSource.Stop();
                yield break;
            }

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

    public void SceneLoad(string sceneName)
    {
        skipTyping = true;

        if (currentTypingCoroutine != null)
        {
            StopCoroutine(currentTypingCoroutine);
            introText.text = storyTexts[storyTexts.Length - 1];
            typingAudioSource.Stop();
        }

        Button buttonComponent = buttonSkip.GetComponent<Button>();

        // Memeriksa apakah GameObject memiliki komponen Button
        if (buttonComponent != null)
        {
            // Hapus komponen Button dari GameObject
            Destroy(buttonComponent);
        }

        StartCoroutine(LoadSceneDelayed(sceneName));
    }

    private IEnumerator LoadSceneDelayed(string sceneName)
    {
        yield return new WaitForSeconds(2.5f); // Tunggu selama 2.5 detik
        SceneManager.LoadScene(sceneName); // Muat scene dengan nama yang diberikan
    }
}
