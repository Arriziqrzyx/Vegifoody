using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Pintu : MonoBehaviour
{
    public string sceneName; 
    public ImageTransparencyController crossFade;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Memastikan objek yang masuk memiliki tag "Player"
        if (other.CompareTag("Player"))
        {
            DelayedSceneLoader();
            crossFade.ResetTransparency();
        }
    }

    private void DelayedSceneLoader()
    {
        StartCoroutine(DelayAndLoad());
    }

    private IEnumerator DelayAndLoad()
    {
        yield return new WaitForSeconds(2.5f);

        SceneLoader();
    }

    private void SceneLoader()
    {
        SceneManager.LoadScene(sceneName); 
    }
}
