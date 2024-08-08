using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditTextDisplay : MonoBehaviour
{
    public TextMeshProUGUI introText;
    public float fadeDuration = 1.0f;
    public float[] displayDurations; // Durasi setiap kalimat ditampilkan diatur melalui Inspector
    public string[] storyTexts; // Kalimat-kalimat yang akan ditampilkan diatur melalui Inspector
    public GameObject buttonSkip;
    private const string dialogCreditKey = "DialogCreditPassed"; // Key untuk PlayerPrefs

    private Coroutine currentFadeCoroutine;
    private bool isSkipped = false;

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

        for (int i = 0; i < storyTexts.Length; i++)
        {
            if (isSkipped)
            {
                break;
            }

            string storyText = storyTexts[i];
            float displayDuration = displayDurations[i];

            // Set the text and fade in
            introText.text = storyText;
            yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 0, 1, fadeDuration));

            // Wait for the display duration minus fade durations
            yield return new WaitForSeconds(displayDuration - fadeDuration * 2);

            // Fade out
            yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 1, 0, fadeDuration));
        }

        // If skipped, directly display "T  A  M  A  T"
        if (isSkipped)
        {
            introText.text = ".............................T  A  M  A  T.............................";
            yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 0, 1, fadeDuration));
            yield return new WaitForSeconds(displayDurations[displayDurations.Length - 1]);
        }

        // Simpan status dialog ke PlayerPrefs
        PlayerPrefs.SetInt(dialogCreditKey, 1);
        SceneManager.LoadScene("Menu");
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
        if (currentFadeCoroutine != null)
        {
            StopCoroutine(currentFadeCoroutine);
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

    public void SkipToEnd()
    {
        isSkipped = true;

        if (currentFadeCoroutine != null)
        {
            StopCoroutine(currentFadeCoroutine);
        }

        StartCoroutine(DisplayStoryText());
    }
}
