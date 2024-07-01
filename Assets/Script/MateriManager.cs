using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MateriManager : MonoBehaviour
{
    [SerializeField] private string sceneName;
    private GameObject targetObject;
    private ImageTransparencyController targetScript;

    void Start()
    {
        // Cari objek GameObject di scene "2d platformer" menggunakan tag
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
        SceneManager.UnloadSceneAsync(sceneName);
    }
}
