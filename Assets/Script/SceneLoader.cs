using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    private const string SFXVolKey = "SFXVol";
    private float previousSFXVolume;

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

    public void PauseGame()
    {
        StartCoroutine(PauseGameWithDelay());
    }

    private IEnumerator PauseGameWithDelay()
    {
        yield return new WaitForSeconds(0.2f);

        Time.timeScale = 0;
        // Simpan volume SFX saat ini
        audioMixer.GetFloat(SFXVolKey, out previousSFXVolume);
        // Setel volume SFX menjadi -80
        audioMixer.SetFloat(SFXVolKey, -80f);
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        // Kembalikan volume SFX ke nilai sebelumnya
        audioMixer.SetFloat(SFXVolKey, previousSFXVolume);
    }
   
}
