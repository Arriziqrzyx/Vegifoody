using UnityEngine;

public class AlokInteraction : MonoBehaviour
{
    private PlayerController playerController;
    private Animator playerAnimator;
    private bool isPlayerNearby = false;
    public GameObject dialogImage; // Drag and drop your Image GameObject here in the Inspector

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>(); // Find the player controller in the scene
        playerAnimator = playerController.GetComponent<Animator>(); // Get the Animator component from the player
        dialogImage.SetActive(false); // Ensure the dialog image is initially hidden
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            playerController.enabled = false; // Disable player movement
            playerAnimator.SetBool("Run", false); // Set Run animation to false
            playerAnimator.SetBool("Jump", false); // Set Jump animation to false
            dialogImage.SetActive(true); // Show the dialog image
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            playerController.enabled = true; // Enable player movement
            dialogImage.SetActive(false); // Hide the dialog image
        }
    }
}
