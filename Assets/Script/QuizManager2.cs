using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class QuizManager2 : MonoBehaviour
{
    public Question[] questions; 
    private int currentQuestionIndex; 
    public TMP_Text questionText; 
    public Button[] answerButtons; 
    public GameObject successPanel; 
    public GameObject failPanel; 
    public TMP_Text resultWinText; 
    public TMP_Text resultLoseText; 
    [SerializeField] private string Scene;
    private int correctAnswersCount; 

    private void Start()
    {
        
        currentQuestionIndex = 0;
        correctAnswersCount = 0;
        LoadQuestion(currentQuestionIndex);
    }

    private void LoadQuestion(int questionIndex)
    {
        
        questionText.text = questions[questionIndex].question;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TMP_Text>().text = questions[questionIndex].answers[i];
        }
    }

    public void AnswerButtonClicked(int buttonIndex)
    {
        
        if (buttonIndex == questions[currentQuestionIndex].correctAnswerIndex)
        {
            Debug.Log("Jawaban benar!");
            correctAnswersCount++;
        }
        else
        {
            Debug.Log("Jawaban salah!");
        }

        
        currentQuestionIndex++;

        if (currentQuestionIndex < questions.Length)
        {
            LoadQuestion(currentQuestionIndex);
        }
        else
        {
            ShowResult();
        }
    }

    private void ShowResult()
    {
        
        int score = (int)(((float)correctAnswersCount / questions.Length) * 100);

        if (correctAnswersCount >= 5)
        {
            successPanel.SetActive(true); 
            resultWinText.text = "Menang! Skor: " + score;
        }
        else
        {
            failPanel.SetActive(true); 
            resultLoseText.text = "Kalah! Skor: " + score;
        }
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
