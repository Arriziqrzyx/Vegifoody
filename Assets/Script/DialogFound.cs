using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class DialogFound : MonoBehaviour
{
    public TextMeshProUGUI dialogText; 
    public GameObject budiAvatar; 
    public GameObject alokAvatar; 
    public float typingSpeed = 0.05f; 
    public float initialDelay = 0.5f; 
    public float messageDelay = 1.0f; // Delay setelah audio selesai diputar
    public GameObject button; 
    public GameObject buttonSkip; 
    public AudioSource typingAudioSource;
    public AudioClip[] dialogAudioClips; // Array untuk klip audio dialog

    private string[] messages = new string[]
    {
        "Aku ada permainan kecil untukmu Budi. Permainan ini akan melatih ketajaman mata kamu.",
        "Permaianan apa itu Alok? Sepertinya menarik!.",
        "Aku sudah menyiapkan beberapa sayuran segar dan juga ada beberapa sayuran dan buah yang busuk. Kamu harus menemukan semua sayuran segar itu dalam waktu 2 menit. jika lebih dari 2 menit maka kamu gagal.",
        "Dan jika kamu mengambil sayuran atau buah busuk, akan mengurangi waktu 5 detik & nyawa kamu. Kamu akan memiliki nyawa atau kesempatan sebanyak 5 kali. Jika nyawa habis maka kamu gagal.",
        "Okeee. Permainan ini menantang juga. Jadi aku harus menemukan semua sayuran segar secepat mungkin dan sebisa mungkin tidak mengambil yang busuk ya?",
        "Benar. Penilian skornya adalah melalui waktu. Semakin cepat kamu menyelesaikannya, semakin besar skor yang akan di dapat.",
        "Seru juga nih! Baiklah, Aku mengerti. Ayo kita mulai"
    };

    private GameObject[] avatars; 
    private int messageIndex = 0;
    private const string dialog6Key = "Dialog6Passed";

    private void Start()
    {
        dialogText.text = ""; 
        button.SetActive(false); 
        buttonSkip.SetActive(false); 
        
        // Pastikan array dialogAudioClips memiliki panjang yang sama dengan messages
        if (dialogAudioClips.Length != messages.Length)
        {
            Debug.LogError("Length of dialogAudioClips array must be equal to the number of messages.");
            return;
        }

        avatars = new GameObject[] { alokAvatar, budiAvatar, alokAvatar, alokAvatar, budiAvatar, alokAvatar, budiAvatar };
        
        if (PlayerPrefs.GetInt(dialog6Key, 0) == 1)
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
        PlayerPrefs.SetInt(dialog6Key, 1);
    }

    public void ButtonSkip()
    {
        typingAudioSource.Stop();
    }
}
