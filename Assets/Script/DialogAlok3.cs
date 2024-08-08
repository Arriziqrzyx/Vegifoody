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
    public float messageDelay = 1.0f; // Delay setelah audio selesai diputar
    public GameObject button; 
    public GameObject buttonSkip; 
    public GameObject alokObject;
    public AudioSource typingAudioSource; 
    public AudioClip[] dialogAudioClips; // Array untuk klip audio dialog

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
    private const string dialog5Key = "Dialog5Passed"; // Key untuk PlayerPrefs

    private void Start()
    {
        dialogText.text = ""; 
        button.SetActive(false); 
        buttonSkip.SetActive(false); 
        playerController2 = FindObjectOfType<PlayerController2>(); 

        // Pastikan array dialogAudioClips memiliki panjang yang sama dengan messages
        if (dialogAudioClips.Length != messages.Length)
        {
            Debug.LogError("Length of dialogAudioClips array must be equal to the number of messages.");
            return;
        }

        avatars = new GameObject[] { alokAvatar, budiAvatar, alokAvatar, budiAvatar, alokAvatar, budiAvatar, alokAvatar, budiAvatar, alokAvatar };

        if (PlayerPrefs.GetInt(dialog5Key, 0) == 1)
        {
            buttonSkip.SetActive(true); // Jika dialog sudah pernah dilakukan, aktifkan tombol skip
        }

        StartCoroutine(StartDialog());
    }

    private IEnumerator StartDialog()
    {
        yield return new WaitForSeconds(initialDelay); 

        while (messageIndex < messages.Length)
        {
            dialogText.text = ""; 
            avatars[messageIndex].SetActive(true); 

            // Memutar audio dan menampilkan teks secara bersamaan
            AudioClip clip = dialogAudioClips[messageIndex];
            if (clip != null)
            {
                AudioSource audioSource = GetComponent<AudioSource>();
                audioSource.clip = clip;
                audioSource.Play();
            }

            typingAudioSource.Play();
            float typingDuration = 0f;
            foreach (char letter in messages[messageIndex].ToCharArray())
            {
                dialogText.text += letter;
                typingDuration += typingSpeed;
                yield return new WaitForSeconds(typingSpeed);
            }
            typingAudioSource.Stop();

            // Tunggu hingga audio selesai diputar
            yield return new WaitForSeconds(clip.length - typingDuration);

            // Tunggu tambahan delay 1 detik setelah audio selesai
            yield return new WaitForSeconds(messageDelay);

            // Sembunyikan avatar jika perlu
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
        PlayerPrefs.SetInt(dialog5Key, 1);

        playerController2.enabled = true;
        alokObject.GetComponent<SpriteRenderer>().enabled = false;
        alokObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.SetActive(false);  
    }

    public void ButtonSkip()
    {
        playerController2.enabled = true;
        alokObject.GetComponent<SpriteRenderer>().enabled = false;
        alokObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.SetActive(false);  
        typingAudioSource.Stop();
    }
}
