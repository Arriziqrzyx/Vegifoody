using UnityEngine;
using TMPro;
using System.Collections;

public class FoodManager : MonoBehaviour
{
    public static FoodManager Instance;

    public TMP_Text timerText;
    public TMP_Text resultWinText;
    public TMP_Text healthText;
    public GameObject winPanel;
    public GameObject losePanel;
    public Transform sayuranParent;

    private int health = 5; // Set health awal
    private float timer = 120f; // Timer diatur menjadi 2 menit
    private bool gameActive = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        UpdateHealthUI(); // Update tampilan health
        StartCoroutine(StartTimer());
    }

    public void UpdateHealth(int value)
    {
        health += value;
        UpdateHealthUI();

        if (health <= 0)
        {
            GameLose();
        }
    }

    private void UpdateHealthUI()
    {
        healthText.text = "Health: " + health.ToString();
    }

    private bool AllSayuranCollected()
    {
        return sayuranParent.childCount == 1;
    }

    private void GameWin()
    {
        gameActive = false;
        winPanel.SetActive(true);
        int finalScore = CalculateFinalScore();
        resultWinText.text = finalScore.ToString();
        StopCoroutine(StartTimer());
    }

    private void GameLose()
    {
        gameActive = false;
        losePanel.SetActive(true);
        int finalScore = CalculateFinalScore();
        StopCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        while (timer > 0)
        {
            if (!gameActive)
            {
                yield break;
            }

            timer -= Time.deltaTime;
            UpdateTimerUI();
            yield return null;
        }

        GameLose();
    }

    private void UpdateTimerUI()
    {
        timerText.text = "Waktu: " + Mathf.Ceil(timer).ToString() + "s";
    }

    private int CalculateFinalScore()
    {
        float timeRemainingPercentage = (timer / 120f) * 100;
        return Mathf.RoundToInt(timeRemainingPercentage);
    }

    public void SayuranCollected()
    {
        if (AllSayuranCollected())
        {
            GameWin();
        }
    }
}
