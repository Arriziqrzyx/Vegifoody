using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class FoodManager : MonoBehaviour
{
    public static FoodManager Instance;

    public TMP_Text scoreText; 
    public TMP_Text timerText; 
    public GameObject winPanel; 
    public GameObject losePanel; 
    public Transform sayuranParent; 

    private int score; 
    private float timer = 10f; 
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
        score = 0;
        UpdateScoreUI();
        winPanel.SetActive(false); 
        losePanel.SetActive(false); 

        
        StartCoroutine(StartTimer());
    }

    
    public void UpdateScore(int value)
    {
        score += value;
        UpdateScoreUI();

        
        if (AllSayuranCollected())
        {
            GameWin();
        }
    }

    
    private void UpdateScoreUI()
    {
        scoreText.text = "Skor: " + score.ToString();
    }

    
    private bool AllSayuranCollected()
    {
        return sayuranParent.childCount == 1;
    }

    
    private void GameWin()
    {
        gameActive = false; 
        winPanel.SetActive(true); 
        StopCoroutine(StartTimer()); 
    }

    
    private void GameLose()
    {
        gameActive = false; 
        losePanel.SetActive(true); 
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
}
