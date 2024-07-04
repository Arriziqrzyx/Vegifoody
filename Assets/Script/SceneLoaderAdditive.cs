using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderAdditive : MonoBehaviour
{
    [SerializeField] private string materi1;
    [SerializeField] private string materi2;
    public void DelayedSceneLoader1()
    {
        StartCoroutine(DelayAndLoad1());
    }

    private IEnumerator DelayAndLoad1()
    {
        yield return new WaitForSeconds(2.5f);

        SceneLoader1();
    }

    private void SceneLoader1()
    {
        StartCoroutine(loadMiniGames1(materi1));
    }

    private IEnumerator loadMiniGames1(string Name)
    {
        SceneManager.LoadScene(Name, LoadSceneMode.Additive);
        yield return new WaitUntil(() => SceneManager.GetSceneByName(Name).isLoaded);
    }

    
    
    public void DelayedSceneLoader2()
    {
        StartCoroutine(DelayAndLoad2());
    }

    private IEnumerator DelayAndLoad2()
    {
        yield return new WaitForSeconds(2.5f);

        SceneLoader2();
    }

    private void SceneLoader2()
    {
        StartCoroutine(loadMiniGames2(materi2));
    }

    private IEnumerator loadMiniGames2(string Name)
    {
        SceneManager.LoadScene(Name, LoadSceneMode.Additive);
        yield return new WaitUntil(() => SceneManager.GetSceneByName(Name).isLoaded);
    }
    
}
