using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class DialogAlok : MonoBehaviour
{
    public TextMeshProUGUI dialogText; // Drag and drop your TextMeshProUGUI element here in the Inspector
    public float typingSpeed = 0.05f; // Speed of the typing effect
    public float initialDelay = 0.5f; // Initial delay before starting the typing effect
    public float fadeDuration = 1.0f; // Duration of the fade in/out
    public GameObject button; // Drag and drop your button GameObject here in the Inspector
    public GameObject alokObject; // Drag and drop GameObject with AlokInteraction.cs here in the Inspector

    private string message = "Halo Budi! Namaku Alok, si Labu Ajaib. Ayo kita bermain mini game mencari sayuran!";
    private PlayerController playerController;

    private void Start()
    {
        dialogText.text = ""; // Ensure the dialog text is initially empty
        button.SetActive(false); // Ensure the button is initially hidden
        playerController = FindObjectOfType<PlayerController>(); // Find the player controller in the scene
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        yield return new WaitForSeconds(initialDelay); // Initial delay before starting the typing effect

        foreach (char letter in message.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(1.0f); // Wait before enabling the button
        button.SetActive(true); // Activate the button after typing is complete
    }

    public void OnButtonClick()
    {
        // Enable player movement
        playerController.enabled = true;

        // Destroy the GameObject with AlokInteraction.cs
        Destroy(alokObject);

        // Optionally, you can also hide the dialog and button if needed
        gameObject.SetActive(false); // Hide the dialog object (assuming this script is attached to the same object as the dialog)
    }
}
