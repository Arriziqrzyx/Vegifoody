using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MateriManager : MonoBehaviour
{
    [SerializeField] private string sceneName;
    private GameObject targetObject;
    private ImageTransparencyController targetScript;
    
    // Button dan TMP Text untuk timer
    [SerializeField] private Button targetButton;
    [SerializeField] private TMP_Text timerText;

    public float timerDuration = 91f; // 90 detik
    private const string materi1Key = "Materi1Passed";

    void Start()
    {
        // Disable the button at start
        if (targetButton != null)
        {
            targetButton.interactable = false;
        }

        // Cari objek GameObject di scene "2d platformer" menggunakan tag
        targetObject = GameObject.FindWithTag("CrossFadePlat");
        if (targetObject != null)
        {
            targetScript = targetObject.GetComponent<ImageTransparencyController>();
            if (targetScript == null)
            {
                Debug.LogError("ImageTransparencyController tidak ditemukan pada GameObject dengan tag 'CrossFadePlat'.");
            }
        }
        else
        {
            Debug.LogError("GameObject dengan tag 'CrossFadePlat' tidak ditemukan.");
        }
    }

    public void DelayedSceneLoader()
    {
        PlayerPrefs.SetInt(materi1Key, 1);
        StartCoroutine(DelayAndLoad());
    }

    private IEnumerator DelayAndLoad()
    {
        yield return new WaitForSeconds(2.5f);

        BackToGameplay();
    }

    private void BackToGameplay()
    {
        if (targetScript != null)
        {
            targetScript.Start();
            Debug.Log("Reset transparency success");
        }
        SceneManager.UnloadSceneAsync(sceneName);
    }

    private IEnumerator StartTimer(float duration)
    {
        float remainingTime = duration;

        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);

            timerText.text = $"{minutes} menit {seconds} detik";

            yield return null;
        }

        timerText.text = "Jika sudah selesai membaca, kamu bisa melanjutkan petualangan.";

        // Enable the button after the timer ends
        if (targetButton != null)
        {
            targetButton.interactable = true;
        }
    }

    public void StartTimer()
    {
        if (PlayerPrefs.GetInt(materi1Key, 0) == 1)
        {
            // Timer selesai jika dialog sudah pernah diselesaikan
            timerText.text = "Jika sudah selesai membaca, kamu bisa melanjutkan petualangan.";
            if (targetButton != null)
            {
                targetButton.interactable = true;
            }
        }
        else
        {
            // Start the timer
            StartCoroutine(StartTimer(timerDuration));
        }
        
    }
}
