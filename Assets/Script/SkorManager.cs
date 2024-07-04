using UnityEngine;
using TMPro;

public class SkorManager : MonoBehaviour
{
    public TMP_Text scoreText1;
    public TMP_Text scoreText2;
    public TMP_Text scoreText3;

    private void Start()
    {
        // Menampilkan skor dari PlayerPrefs
        scoreText1.text = PlayerPrefs.GetInt("HighScore1", 0).ToString();
        scoreText2.text = PlayerPrefs.GetInt("HighScore2", 0).ToString();
        scoreText3.text = PlayerPrefs.GetInt("HighScore3", 0).ToString();
    }
}
