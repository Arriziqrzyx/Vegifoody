using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FoundManager : MonoBehaviour
{
    public static FoundManager instance; 
    private int score = 0; 
    public TMP_Text scoreText; 
    public GameObject winPanel; 
    public TMP_Text winPanelText; 
    public GameObject losePanel; 
    public TMP_Text timerText; 
    public int totalVegetables; 
    public float gameTime = 60f; 

    private int vegetablesFound = 0; 
    private float currentTime; 
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
        winPanel.SetActive(false); 
        losePanel.SetActive(false); 
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
        winPanel.SetActive(true); 
        winPanelText.text = "Kamu Menang. Skor kamu: " + score.ToString(); 
    }

    private void ShowLosePanel()
    {
        losePanel.SetActive(true); 
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
