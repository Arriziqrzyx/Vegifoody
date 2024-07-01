using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class DialogAlok3 : MonoBehaviour
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
        "Selamat datang Budi, kamu berhasil sampai di kebun ajaib.",
        "Alok, jadi selama ini...",
        "Benar. Aku yang membangun kebun ajaib ini. Aku selalu menjaga tempat ini dari orang-orang jahat dan malas untuk belajar. Berkat Mewing & Skibidi, mereka berdua mencegah orang jahat masuk kesini.",
        "Jadi, kamu dan Mewing serta Skibidi bekerja sama untuk menjaga kebun ini?",
        "Ya, kami bertiga adalah penghuni kebun ajaib ini. Mewing & Skibidi memastikan hanya orang-orang yang rajin, berani dan pantang menyerah yang boleh masuk kesini. Orang yang berniat jahat dan malas tidak akan pernah sampai kesini.",
        "Itu sangat keren! Aku senang bisa bertemu dengan kalian bertiga.",
        "Kami juga senang bertemu denganmu, Budi. Kami percaya kamu memiliki potensi besar untuk menghargai keindahan dan manfaat dari kebun ajaib ini.",
        "Terima kasih, Alok. Aku akan belajar dengan sungguh-sungguh dan menjaga kebun ini sebaik mungkin.",
        "Itu dia. Sekarang, mari ikut aku mencari sayuran sehat di kebun ini. Kamu layak mengambil dan membawanya pulang. Sayuran-sayuran ini akan sangat bermanfaat untuk kesehatan dan pertumbuhan kamu budi."
    };


    private GameObject[] avatars; 
    private PlayerController2 playerController2;
    private int messageIndex = 0;
    // private bool isTyping = false; 

    private void Start()
    {
        dialogText.text = ""; 
        button.SetActive(false); 
        playerController2 = FindObjectOfType<PlayerController2>(); 

        
        avatars = new GameObject[] { alokAvatar, budiAvatar, alokAvatar, budiAvatar, alokAvatar, budiAvatar, alokAvatar, budiAvatar, alokAvatar };

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
        playerController2.enabled = true;
        alokObject.GetComponent<SpriteRenderer>().enabled = false;
        alokObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.SetActive(false);  
    }
}
