using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SenInteraction : MonoBehaviour
{
    public PlayerController playerController;
    private Animator playerAnimator;
    public GameObject dialogImage; 
    [SerializeField] private string Penjaga1;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>(); 
        playerAnimator = playerController.GetComponent<Animator>(); 
        dialogImage.SetActive(false); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController.enabled = false; 
            playerAnimator.SetBool("Run", false); 
            playerAnimator.SetBool("Jump", false); 
            dialogImage.SetActive(true); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController.enabled = true;
            dialogImage.SetActive(false); 
        }
    }

    public void DelayedSceneLoader()
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
        StartCoroutine(loadMiniGames(Penjaga1));
    }

    private IEnumerator loadMiniGames(string Name)
    {
        SceneManager.LoadScene(Name, LoadSceneMode.Additive);
        yield return new WaitUntil(() => SceneManager.GetSceneByName(Name).isLoaded);
    }
}
