using UnityEngine;
using UnityEngine.UI;

public class BookManager : MonoBehaviour
{
    [SerializeField] private Button bookButton1;
    [SerializeField] private Button bookButton2;

    private const string materi1Key = "Materi1Passed";
    private const string materi2Key = "Materi2Passed";

    private void Start()
    {
        // Menonaktifkan kedua button pada Start
        bookButton1.interactable = false;
        bookButton2.interactable = false;
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt(materi1Key, 0) == 1)
        {
            // Mengaktifkan button 1 jika playerpref bernilai 1
            bookButton1.interactable = true;
        }

        // Mengecek PlayerPrefs untuk materi2Key
        if (PlayerPrefs.GetInt(materi2Key, 0) == 1)
        {
            // Mengaktifkan button 2 jika playerpref bernilai 1
            bookButton2.interactable = true;
        }
    }
}
