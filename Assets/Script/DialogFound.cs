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
    public float messageDelay = 1.5f; 
    public GameObject button; 
    public AudioSource typingAudioSource;

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
    // private bool isTyping = false; 

    private void Start()
    {
        dialogText.text = ""; 
        button.SetActive(false); 
        
        avatars = new GameObject[] { alokAvatar, budiAvatar, alokAvatar, alokAvatar, budiAvatar, alokAvatar, budiAvatar };
        
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
        gameObject.SetActive(false); 
    }
}