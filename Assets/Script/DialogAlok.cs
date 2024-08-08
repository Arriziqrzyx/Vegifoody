using UnityEngine;
using TMPro;
using System.Collections;

public class DialogAlok : MonoBehaviour
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
    private const string dialog1Key = "Dialog1Passed"; // Key untuk PlayerPrefs

    private void Start()
    {
        dialogText.text = "";
        button.SetActive(false);
        buttonSkip.SetActive(false);
        playerController = FindObjectOfType<PlayerController>();

        avatars = new GameObject[] { alokAvatar, budiAvatar, alokAvatar, budiAvatar, alokAvatar, budiAvatar, alokAvatar };

        // Cek PlayerPrefs untuk status dialog
        if (PlayerPrefs.GetInt(dialog1Key, 0) == 1)
        {
            buttonSkip.SetActive(true); // Jika dialog sudah pernah dilakukan, aktifkan tombol skip
        }

        // Pastikan array dialogAudioClips memiliki panjang yang sama dengan messages
        if (dialogAudioClips.Length != messages.Length)
        {
            Debug.LogError("Length of dialogAudioClips array must be equal to the number of messages.");
            return;
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

            // Mulai memutar audio
            AudioClip clip = dialogAudioClips[messageIndex];
            if (clip != null)
            {
                AudioSource audioSource = GetComponent<AudioSource>();
                audioSource.clip = clip;
                audioSource.Play();
            }

            // Tampilkan teks dengan kecepatan ketikan
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
        // Simpan status dialog ke PlayerPrefs
        PlayerPrefs.SetInt(dialog1Key, 1);

        playerController.enabled = true;
        alokObject.GetComponent<SpriteRenderer>().enabled = false;
        alokObject.GetComponent<BoxCollider2D>().enabled = false;
        alokObject.GetComponent<CapsuleCollider2D>().enabled = false;
        gameObject.SetActive(false);
    }

    public void ButtonSkip()
    {
        playerController.enabled = true;
        alokObject.GetComponent<SpriteRenderer>().enabled = false;
        alokObject.GetComponent<BoxCollider2D>().enabled = false;
        alokObject.GetComponent<CapsuleCollider2D>().enabled = false;
        gameObject.SetActive(false);
        typingAudioSource.Stop();
    }
}
