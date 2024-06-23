using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class DialogAlok : MonoBehaviour
{
    public TextMeshProUGUI dialogText; // Drag and drop your TextMeshProUGUI element here in the Inspector
    public GameObject budiAvatar; // Drag and drop the UI Image GameObject for Budi's avatar here in the Inspector
    public GameObject alokAvatar; // Drag and drop the UI Image GameObject for Alok's avatar here in the Inspector
    public float typingSpeed = 0.05f; // Speed of the typing effect
    public float initialDelay = 0.5f; // Initial delay before starting the typing effect
    public float messageDelay = 1.5f; // Delay between messages
    public GameObject button; // Drag and drop your button GameObject here in the Inspector
    public GameObject alokObject; // Drag and drop GameObject with AlokInteraction.cs here in the Inspector

    private string[] messages = new string[]
    {
        "Halo nama aku Alok, kamu siapa? Sedang apa kamu disini sendirian?",
        "Aku Budi, aku sedang berpetualang mencari kebun ajaib yang penuh dengan sayuran sehat.",
        "Kebun ajaib itu memang benar-benar ada. Tapi tidak seharusnya kamu pergi sendirian. Di luar sana banyak sekali rintangan yang akan kamu hadapi.",
        "Tidak masalah. Aku siap.",
        "Wah kamu sangat berani sekali. Karena kamu punya semangat yang tinggi, ini aku beri hadiah. Mungkin ini akan berguna untuk petualangan kamu.",
        "Anjay sebuah materi pelajaran tentang sayuran. Terima kasih Alok, aku janji aku akan mempelajarinya.",
        "Sama-sama. Hati-hati dalam perjalanan kamu selanjutnya. Semoga berhasil. Dan jangan lupa pelajari materi sayuran ini."
    };

    private GameObject[] avatars; // Array to store avatar GameObjects
    private PlayerController playerController;
    private int messageIndex = 0;
    private bool isTyping = false; // Flag to check if currently typing

    private void Start()
    {
        dialogText.text = ""; // Ensure the dialog text is initially empty
        button.SetActive(false); // Ensure the button is initially hidden
        playerController = FindObjectOfType<PlayerController>(); // Find the player controller in the scene

        // Initialize avatar array with corresponding GameObjects
        avatars = new GameObject[] { alokAvatar, budiAvatar, alokAvatar, budiAvatar, alokAvatar, budiAvatar, alokAvatar };

        StartCoroutine(StartDialog());
    }

    private IEnumerator StartDialog()
    {
        yield return new WaitForSeconds(initialDelay); // Initial delay before starting the typing effect

        while (messageIndex < messages.Length)
        {
            isTyping = true;
            dialogText.text = ""; // Clear the dialog text
            avatars[messageIndex].SetActive(true); // Activate the correct avatar

            foreach (char letter in messages[messageIndex].ToCharArray())
            {
                dialogText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

            isTyping = false;

            yield return new WaitForSeconds(messageDelay); // Delay before showing the next message

            // Check if it's the last message before activating the button
            if (messageIndex < messages.Length - 1)
            {
                avatars[messageIndex].SetActive(false); // Deactivate the avatar if it's not the last message
            }

            messageIndex++;
        }

        // Dialog is finished, activate the button
        button.SetActive(true);
    }

    public void OnButtonClick()
    {
        // Enable player movement and destroy Alok object
        playerController.enabled = true;
        Destroy(alokObject);

        // Optionally, hide the dialog
        gameObject.SetActive(false); // Hide the dialog object (assuming this script is attached to the same object as the dialog)
    }
}
