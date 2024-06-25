using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class DialogAlok2 : MonoBehaviour
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
        "Hai Budi, kita bertemu lagi.",
        "Alok, kenapa kamu ada di sini? Siapa kamu sebenarnya?",
        "Jika kamu sampai kesini, berarti kamu sudah bertemu dengan Mewing si penjaga.",
        "Iya, aku bertemu dengan Mewing. Dan dia memberiku ujian agar aku bisa lewat. Dan aku berhasil lolos ujian itu karena materi yang kamu berikan. Aku sangat berterimakasih padamu untuk itu, Alok.",
        "Aku sudah menduga kamu pasti bisa menguasai materi itu. Kebun ajaib sudah dekat, Budi. Tapi...",
        "Tapi kenapa? Ada apa?",
        "Mewing adalah penjaga pertama. Dia punya saudara kembar bernama Skibidi yang sangat lebih tegas dari Mewing. Sampai saat ini, jarang ada orang yang berhasil lolos ujian dari Skibidi.",
        "Aku sudah bilang kepadamu bahwa aku sangat bersungguh-sungguh ingin pergi ke kebun ajaib. Aku siap menghadapi rintangan apapun untuk itu, bahkan ujian Skibidi pun aku berani menghadapinya.",
        "Seperti biasa, kamu memang anak yang pemberani dan pantang menyerah. Aku hargai sikapmu itu, Budi. Jarang sekali orang yang seperti kamu. Oleh karena itu, ini aku beri kamu hadiah lagi.",
        "Bejirrr, sebuah materi tentang manfaat sayuran. Terima kasih, Alok. Tapi kenapa kamu selalu membantuku? Pertama kamu memberiku materi macam-macam sayuran. Sekarang kamu memberi materi tentang manfaat sayuran. Siapa kamu sebenarnya?",
        "Sama-sama. Aku membantu kamu karena aku sangat mengapresiasi anak yang rajin, berani, dan pantang menyerah. Kamu memiliki itu semua, Budi. Mengenai siapa diriku, nanti kamu akan tahu sendiri.",
        "Tapi, Alok...",
        "Sudah saatnya aku pergi, Budi. Sebaiknya kamu pelajari materi yang aku berikan ini agar kamu siap bertemu dengan Skibidi. Senang bertemu denganmu, Budi. Semoga kita bertemu lagi."
    };

    private PlayerController playerController;
    private int messageIndex = 0;
    private bool isTyping = false; // Flag to check if currently typing

    private void Start()
    {
        dialogText.text = ""; // Ensure the dialog text is initially empty
        button.SetActive(false); // Ensure the button is initially hidden
        playerController = FindObjectOfType<PlayerController>(); // Find the player controller in the scene

        StartCoroutine(StartDialog());
    }

    private IEnumerator StartDialog()
    {
        yield return new WaitForSeconds(initialDelay); // Initial delay before starting the typing effect

        while (messageIndex < messages.Length)
        {
            isTyping = true;
            dialogText.text = ""; // Clear the dialog text

            // Activate the correct avatar based on the speaker
            if (messageIndex % 2 == 0)
            {
                alokAvatar.SetActive(true);
                budiAvatar.SetActive(false);
            }
            else
            {
                alokAvatar.SetActive(false);
                budiAvatar.SetActive(true);
            }

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
                // Deactivate the avatar if it's not the last message
                if (messageIndex % 2 == 0)
                {
                    alokAvatar.SetActive(false);
                }
                else
                {
                    budiAvatar.SetActive(false);
                }
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
