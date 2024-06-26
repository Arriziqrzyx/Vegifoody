using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlokInteraction3 : MonoBehaviour
{
    public PlayerController2 playerController2; 
    private Animator playerAnimator;
    private bool isPlayerNearby = false;
    public GameObject dialogImage;
    [SerializeField] private string Alok1;

    private void Start()
    {
        playerController2 = FindObjectOfType<PlayerController2>(); 
        playerAnimator = playerController2.GetComponent<Animator>(); 
        dialogImage.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            playerController2.enabled = false; 
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
            playerController2.enabled = true; 
            dialogImage.SetActive(false);
        }
    }

    public void SceneLoader()
    {
        StartCoroutine(LoadMiniGames(Alok1));
    }

    IEnumerator LoadMiniGames(string Name)
    {
        SceneManager.LoadScene(Name, LoadSceneMode.Additive);
        yield return new WaitUntil(() => SceneManager.GetSceneByName(Name).isLoaded);
    }
}
