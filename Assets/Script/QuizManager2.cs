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
    public TMP_Text correctAnswersText; // Untuk panel menang
    public TMP_Text correctAnswersTextFail; // Untuk panel kalah
    [SerializeField] private string Scene;
    private int correctAnswersCount;
    [SerializeField] AudioSource benarAudio;
    [SerializeField] AudioSource SalahAudio;
    private const string HighScoreKey = "HighScore2";
    private GameObject targetObject;
    private ImageTransparencyController targetScript;

    private void Start()
    {
        currentQuestionIndex = 0;
        correctAnswersCount = 0;
        UpdateCorrectAnswersUI();
        LoadQuestion(currentQuestionIndex);

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
            benarAudio.Play();
            correctAnswersCount++;
        }
        else
        {
            Debug.Log("Jawaban salah!");
            SalahAudio.Play();
        }

        UpdateCorrectAnswersUI();

        // Pindah ke pertanyaan selanjutnya
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

        // Simpan skor tertinggi jika lebih besar
        int highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, score);
        }

        if (correctAnswersCount >= 5)
        {
            successPanel.SetActive(true);
            resultWinText.text = score.ToString();
        }
        else
        {
            failPanel.SetActive(true);
            resultLoseText.text = score.ToString();
        }
    }

    IEnumerator LoadGame(string Name)
    {
        SceneManager.LoadScene(Name, LoadSceneMode.Additive);
        yield return new WaitUntil(() => SceneManager.GetSceneByName(Name).isLoaded);
    }

    public void PlayAgain()
    {
        SceneManager.UnloadSceneAsync(Scene);
        StartCoroutine(LoadGame(Scene));
    }

    public void DelayedSceneLoader()
    {
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
            Debug.Log("cross start success");
        }
        SceneManager.UnloadSceneAsync(Scene);
    }

    private void UpdateCorrectAnswersUI()
    {
        correctAnswersText.text = $"{correctAnswersCount}/{questions.Length}";
        correctAnswersTextFail.text = $"{correctAnswersCount}/{questions.Length}";
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HighScoreKey, 0);
    }
}
