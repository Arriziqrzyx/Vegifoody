using UnityEngine;
using TMPro;
using System.Collections;

public class DialogPenjaga1 : MonoBehaviour
{
    public TextMeshProUGUI dialogText; 
    public GameObject budiAvatar; 
    public GameObject mewingAvatar; 
    public float typingSpeed = 0.05f; 
    public float initialDelay = 0.5f; 
    public float messageDelay = 1.0f; // Delay setelah audio selesai diputar
    public GameObject button; 
    public GameObject buttonSkip;
    public GameObject mewingObject; 
    public AudioSource typingAudioSource;
    public AudioClip[] dialogAudioClips; // Array untuk klip audio dialog

    private string[] messages = new string[]
    {
        "Berhenti! Aku Mewing, penjaga Kebun Ajaib. Siapa kamu dan apa yang kamu lakukan di sini?",
        "Aku Budi, aku sedang berpetualang mencari Kebun Ajaib yang penuh dengan sayuran sehat.",
        "Kebun Ajaib tidak bisa dimasuki oleh sembarang orang. Hanya mereka yang benar-benar layak dan berani yang boleh aku biarkan lewat.",
        "Aku siap untuk menghadapi tantangan apapun! Aku sangat ingin menemukan Kebun Ajaib dan mendapatkan sayuran sehat itu.",
        "Keberanianmu mengagumkan, Budi. Tapi keberanian saja tidak cukup. Kamu harus membuktikan bahwa kamu layak masuk ke Kebun Ajaib.",
        "Bagaimana caranya aku bisa membuktikan bahwa aku layak?",
        "Aku akan memberimu ujian dengan 8 pertanyaan. Kamu akan berhasil lolos jika kamu bisa menjawab benar setidaknya 4 dari 8 pertanyaan tersebut",
        "Aku siap menghadapi ujian apapun yang kamu berikan, Mewing.",
        "Baiklah. Bersiaplah, Budi. Ujianmu akan dimulai sekarang. Jika kamu berhasil, kamu boleh lewat dan melanjutkan perjalanan."
    };

    private GameObject[] avatars; 
    private PlayerController playerController;
    private int messageIndex = 0;
    private const string dialog2Key = "Dialog2Passed"; // Key untuk PlayerPrefs

    private void Start()
    {
        dialogText.text = ""; 
        button.SetActive(false); 
        buttonSkip.SetActive(false); 
        playerController = FindObjectOfType<PlayerController>(); 

        // Pastikan array dialogAudioClips memiliki panjang yang sama dengan messages
        if (dialogAudioClips.Length != messages.Length)
        {
            Debug.LogError("Length of dialogAudioClips array must be equal to the number of messages.");
            return;
        }

        avatars = new GameObject[] { mewingAvatar, budiAvatar, mewingAvatar, budiAvatar, mewingAvatar, budiAvatar, mewingAvatar, budiAvatar, mewingAvatar };

        if (PlayerPrefs.GetInt(dialog2Key, 0) == 1)
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
        PlayerPrefs.SetInt(dialog2Key, 1);

        playerController.enabled = true;
        mewingObject.GetComponent<SpriteRenderer>().enabled = false;
        mewingObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.SetActive(false);  
    }

    public void ButtonSkip()
    {
        playerController.enabled = true;
        mewingObject.GetComponent<SpriteRenderer>().enabled = false;
        mewingObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.SetActive(false);
        typingAudioSource.Stop(); 
    }
}
