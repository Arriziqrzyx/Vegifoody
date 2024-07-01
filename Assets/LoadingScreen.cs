using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public LoadingBar smoothFill;
    public string sceneToLoad;

    void Start()
    {
        if (smoothFill != null)
        {
            // Berlangganan event OnFillComplete
            smoothFill.OnFillComplete += HandleFillComplete;
        }
    }

    void HandleFillComplete()
    {
        // Pindah ke scene yang ditentukan
        SceneManager.LoadScene(sceneToLoad);
    }

    void OnDestroy()
    {
        if (smoothFill != null)
        {
            // Berhenti berlangganan event OnFillComplete untuk menghindari kebocoran memori
            smoothFill.OnFillComplete -= HandleFillComplete;
        }
    }
}
