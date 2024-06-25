using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SenInteraction : MonoBehaviour
{
    public PlayerController playerController;
    private Animator playerAnimator;
    private bool isPlayerNearby = false;
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
            isPlayerNearby = true;
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
            isPlayerNearby = false;
            playerController.enabled = true; 
            dialogImage.SetActive(false); 
        }
    }

    public void SceneLoader() 
    {
        StartCoroutine(loadMiniGames(Penjaga1));
    }

    IEnumerator loadMiniGames(string Name)
    {
        SceneManager.LoadScene(Name, LoadSceneMode.Additive);
        yield return new WaitUntil(() => SceneManager.GetSceneByName(Name).isLoaded);
    }
}
