using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class DialogPenjaga2 : MonoBehaviour
{
    public TextMeshProUGUI dialogText; // Drag and drop your TextMeshProUGUI element here in the Inspector
    public GameObject budiAvatar; // Drag and drop the UI Image GameObject for Budi's avatar here in the Inspector
    public GameObject skibidiAvatar; // Drag and drop the UI Image GameObject for Alok's avatar here in the Inspector
    public float typingSpeed = 0.05f; // Speed of the typing effect
    public float initialDelay = 0.5f; // Initial delay before starting the typing effect
    public float messageDelay = 1.5f; // Delay between messages
    public GameObject button; // Drag and drop your button GameObject here in the Inspector
    public GameObject skibidiObject; // Drag and drop GameObject with AlokInteraction.cs here in the Inspector

    private string[] messages = new string[]
    {
        "Hai, kamu pasti Skibidi, saudara kembar dari Mewing. Aku Budi, dan aku sudah berhasil melewati ujian dari Mewing.",
        "Benar, aku Skibidi. Jadi, kamu berhasil melewati ujian dari saudaraku. Tapi jangan berpikir bahwa ujianku akan semudah itu Budi. Bahkan sampai saat ini, jarang ada yang lolos dari ujianku.",
        "Aku sudah siap untuk ujianmu Skibidi. Aku ingin sekali mencapai kebun ajaib.",
        "Semangatmu mungkin mengagumkan, tapi jangan meremehkan tantanganku. Aku jauh lebih tegas dan galak dari Mewing. Siapkah kamu untuk menghadapi tantanganku?",
        "Aku sudah bilang kepada Alok, aku siap menghadapi apa pun. Ujian dari Mewing sudah memberi banyak pelajaran berharga.",
        "Baiklah, kita lihat seberapa kuat tekadmu. Tapi ingat, aku tidak akan memberi keringanan seperti Mewing. Kamu harus benar-benar siap dan fokus. Jika tidak, kamu akan gagal.",
        "Aku mengerti, Skibidi. Aku akan memberikan yang terbaik. Apa ujian yang harus aku hadapi kali ini?",
        "Ujian ini akan menguji pengetahuanmu tentang manfaat sayuran. Kamu harus menjawab setiap pertanyaan dengan benar.",
        "Aku sudah belajar materi dari Alok dengan baik. Aku siap untuk menjawab pertanyaanmu.",
        "Bagus. Jika kamu bisa menjawab setidaknya 5 dari 7 pertanyaan dengan benar, kamu akan lolos. Tapi jika kurang dari itu, kamu harus mengulangi ujian ini. Banyak yang sudah mencoba, namun sedikit yang berhasil.",
        "Baik, Skibidi. Mari mulai ujiannya.",
        "Aku berharap yang terbaik untukmu, Budi. Jangan buat aku kecewa."
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
        avatars = new GameObject[] { budiAvatar, skibidiAvatar, budiAvatar, skibidiAvatar, budiAvatar, skibidiAvatar, budiAvatar, skibidiAvatar, budiAvatar, skibidiAvatar, budiAvatar, skibidiAvatar };

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
        Destroy(skibidiObject);

        // Optionally, hide the dialog
        gameObject.SetActive(false); // Hide the dialog object (assuming this script is attached to the same object as the dialog)
    }
}
