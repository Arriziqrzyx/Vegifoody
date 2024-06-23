using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FoundManager : MonoBehaviour
{
    public static FoundManager instance; // Singleton instance
    private int score = 0; // Initial score
    public TMP_Text scoreText; // Drag and drop your TMP_Text element for displaying score here in the Inspector
    public GameObject winPanel; // Drag and drop the win panel GameObject here in the Inspector
    public TMP_Text winPanelText; // Drag and drop the TMP_Text element from win panel here in the Inspector
    public GameObject losePanel; // Drag and drop the lose panel GameObject here in the Inspector
    public TMP_Text timerText; // Drag and drop the TMP_Text element for displaying the timer
    public int totalVegetables; // Set the total number of vegetables in the scene
    public float gameTime = 60f; // Total game time in seconds

    private int vegetablesFound = 0; // Counter for found vegetables
    private float currentTime; // Current time left
    [SerializeField] private string Scene;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        winPanel.SetActive(false); // Ensure the win panel is initially hidden
        losePanel.SetActive(false); // Ensure the lose panel is initially hidden
        currentTime = gameTime;
        UpdateScoreUI();
        UpdateTimerUI();
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI();

            if (currentTime <= 0)
            {
                currentTime = 0;
                ShowLosePanel();
            }
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    public void VegetableFound()
    {
        vegetablesFound++;
        if (vegetablesFound >= totalVegetables)
        {
            ShowWinPanel();
        }
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    private void UpdateTimerUI()
    {
        timerText.text = "Time: " + Mathf.Ceil(currentTime).ToString() + "s";
    }

    private void ShowWinPanel()
    {
        winPanel.SetActive(true); // Show the win panel
        winPanelText.text = "Kamu Menang. Skor kamu: " + score.ToString(); // Update the win panel text with score
    }

    private void ShowLosePanel()
    {
        losePanel.SetActive(true); // Show the lose panel
    }

    IEnumerator LoadGame(string Name)
    {
        SceneManager.LoadScene(Name, LoadSceneMode.Additive);
        yield return new WaitUntil(() => SceneManager.GetSceneByName(Name).isLoaded);
    }

    public void PlayAgain()
    {
        StartCoroutine(LoadGame(Scene));
    }


    public void BackToGameplay()
    {
        SceneManager.UnloadSceneAsync(Scene);
    }
}
