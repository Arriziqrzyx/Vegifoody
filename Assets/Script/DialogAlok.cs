using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;


public class DialogAlok : MonoBehaviour
{
    public TextMeshProUGUI dialogText; 
    public GameObject budiAvatar; 
    public GameObject alokAvatar; 
    public float typingSpeed = 0.05f; 
    public float initialDelay = 0.5f; 
    public float messageDelay = 1.5f; 
    public GameObject button; 
    public GameObject alokObject; 
    public AudioSource typingAudioSource;

    private string[] messages = new string[]
    {
        "Halo nama aku Alok, kamu siapa? Sedang apa kamu disini sendirian?",
        "Aku Budi, aku sedang berpetualang mencari kebun ajaib yang penuh dengan sayuran sehat.",
        "Kebun ajaib itu memang benar-benar ada. Tapi tidak seharusnya kamu pergi sendirian. Di luar sana banyak sekali rintangan yang akan kamu hadapi.",
        "Tidak masalah. Aku siap.",
        "Wah kamu sangat berani sekali. Karena kamu punya semangat yang tinggi, ini aku beri hadiah. Mungkin ini akan berguna untuk petualangan kamu.",
        "Wah keren! sebuah materi pelajaran tentang sayuran. Terima kasih Alok, aku janji aku akan mempelajarinya.",
        "Sama-sama. Hati-hati dalam perjalanan kamu selanjutnya. Semoga berhasil. Dan jangan lupa pelajari materi sayuran ini."
    };

    private GameObject[] avatars; 
    private PlayerController playerController;
    private int messageIndex = 0;
    // private bool isTyping = false; 

    private void Start()
    {
        dialogText.text = ""; 
        button.SetActive(false); 
        playerController = FindObjectOfType<PlayerController>(); 

        
        avatars = new GameObject[] { alokAvatar, budiAvatar, alokAvatar, budiAvatar, alokAvatar, budiAvatar, alokAvatar };

        StartCoroutine(StartDialog());
    }

    private IEnumerator StartDialog()
    {
        yield return new WaitForSeconds(initialDelay); 

        while (messageIndex < messages.Length)
        {
            // isTyping = true;
            dialogText.text = ""; 
            avatars[messageIndex].SetActive(true); 

            typingAudioSource.Play();
            foreach (char letter in messages[messageIndex].ToCharArray())
            {
                dialogText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
            typingAudioSource.Stop();
            
            // isTyping = false;

            yield return new WaitForSeconds(messageDelay); 

            
            if (messageIndex < messages.Length - 1)
            {
                avatars[messageIndex].SetActive(false); 
            }

            messageIndex++;
        }

        
        button.SetActive(true);
    }

    public void OnButtonClick()
    {
        playerController.enabled = true;
        alokObject.GetComponent<SpriteRenderer>().enabled = false;
        alokObject.GetComponent<BoxCollider2D>().enabled = false;
        alokObject.GetComponent<CapsuleCollider2D>().enabled = false;
        gameObject.SetActive(false); 
    }
}
