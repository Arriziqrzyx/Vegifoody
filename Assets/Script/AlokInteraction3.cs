using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlokInteraction3 : MonoBehaviour
{
    public PlayerController2 playerController2; 
    private Animator playerAnimator;
    public GameObject dialogImage;
    [SerializeField] private string Alok3;

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
            playerController2.enabled = false; 
            playerAnimator.SetBool("Run", false);
            playerAnimator.SetBool("Jump", false);
            dialogImage.SetActive(true);
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
        SceneManager.LoadScene(Alok3);
    }
}
