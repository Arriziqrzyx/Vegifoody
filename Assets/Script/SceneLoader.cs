using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void SceneLoad(string sceneName)
    {
        StartCoroutine(LoadSceneDelayed(sceneName));
    }

    private IEnumerator LoadSceneDelayed(string sceneName)
    {
        yield return new WaitForSeconds(2.5f); // Tunggu selama 1.5 detik
        SceneManager.LoadScene(sceneName); // Muat scene dengan nama yang diberikan
    }

    public void Keluar()
    {
        Debug.Log ("KAMU TELAH KELUAR!");
        Application.Quit();
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
    }
    public void PlayGame()
    {
        Time.timeScale = 1;
    }
   
}
