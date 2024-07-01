using UnityEngine;
using TMPro;
using System.Collections;

public class FoodManager : MonoBehaviour
{
    public static FoodManager Instance;

    public TMP_Text timerText;
    public TMP_Text resultWinText;
    public GameObject[] health;
    public GameObject winPanel;
    public GameObject losePanel;
    public Transform sayuranParent;

    private float timer = 120f; 
    private bool gameActive = true;
    private const string HighScoreKey = "HighScore3";

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
        StartCoroutine(StartTimer());
    }

    public void UpdateHealth(int value)
    {
        for (int i = 0; i < Mathf.Abs(value); i++)
        {
            if (value < 0)
            {
                // Kurangi nyawa
                RemoveHealth();
            }
        }

        if (CountActiveHealth() <= 0)
        {
            GameLose();
        }
    }

    private void RemoveHealth()
    {
        for (int i = health.Length - 1; i >= 0; i--)
        {
            if (health[i].activeSelf)
            {
                health[i].SetActive(false);
                break;
            }
        }
    }

    private int CountActiveHealth()
    {
        int activeCount = 0;
        foreach (GameObject heart in health)
        {
            if (heart.activeSelf)
            {
                activeCount++;
            }
        }
        return activeCount;
    }

    private void Update()
    {
        if (sayuranParent.childCount == 0)
        {
            GameWin();
        }
    }

    private void GameWin()
    {
        gameActive = false;
        winPanel.SetActive(true);
        int finalScore = CalculateFinalScore();

        int highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        if (finalScore > highScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, finalScore);
        }

        resultWinText.text = $"Skor: {finalScore}";
        StopCoroutine(StartTimer());
    }

    private void GameLose()
    {
        gameActive = false;
        losePanel.SetActive(true);
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
        timerText.text = Mathf.Ceil(timer).ToString();

        // Ubah warna teks berdasarkan waktu yang tersisa
        if (timer < 30)
        {
            timerText.color = Color.red;
        }
        else if (timer < 60)
        {
            timerText.color = Color.yellow;
        }
        else
        {
            timerText.color = Color.white; // Warna default
        }
    }

    private int CalculateFinalScore()
    {
        float timeRemainingPercentage = (timer / 100f) * 100;
        int finalScore = Mathf.RoundToInt(timeRemainingPercentage);
        return Mathf.Min(finalScore, 100); 
    }

    public void ReduceTimer(float amount)
    {
        timer -= amount;
        if (timer < 0)
        {
            timer = 0;
        }
        UpdateTimerUI();
    }
}
