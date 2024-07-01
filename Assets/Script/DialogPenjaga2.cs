using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class DialogPenjaga2 : MonoBehaviour
{
    public TextMeshProUGUI dialogText; 
    public GameObject budiAvatar; 
    public GameObject skibidiAvatar; 
    public float typingSpeed = 0.05f; 
    public float initialDelay = 0.5f; 
    public float messageDelay = 1.5f; 
    public GameObject button; 
    public GameObject skibidiObject; 
    public AudioSource typingAudioSource;

    private string[] messages = new string[]
    {
        "Hai, pasti kamu Skibidi, saudara kembar Mewing. Aku Budi, dan aku bersemangat ingin pergi ke kebun ajaib.",
        "Ya, aku Skibidi. Pintu masuk kebun ajaib ada di belakangku. Kamu berhasil melewati ujian Mewing, tapi jangan kira ujianku akan semudah itu. Bahkan sampai saat ini, hampir tak ada yang lolos dari ujianku.",
        "Pintu masuk itu ada di belakangmu? Aku tidak melihat apapun di sana.",
        "Tentu saja kamu tidak bisa melihatnya. Hanya mereka yang berhasil melewati ujianku yang bisa melihatnya. Aku jauh lebih tegas dan galak daripada Mewing. Siapkah kamu untuk menghadapiku?",
        "Aku telah bersiap sejak awal. Ujian dari Mewing telah mengajarkanku banyak hal berharga.",
        "Baiklah, mari kita lihat seberapa kuat tekadmu. Tapi ingat, aku tidak akan memberi keringanan seperti Mewing. Kamu harus benar-benar siap dan fokus. Jika tidak, kamu akan gagal.",
        "Aku mengerti, Skibidi. Aku akan memberikan yang terbaik. Apa ujian yang harus aku hadapi kali ini?",
        "Ujian ini akan menguji pengetahuanmu tentang manfaat sayuran. Kamu harus menjawab setiap pertanyaan dengan benar.",
        "Aku sudah mempelajari materi dari Alok dengan baik. Aku siap untuk menjawab pertanyaanmu.",
        "Bagus. Jika kamu bisa menjawab setidaknya 5 dari 7 pertanyaan dengan benar, kamu akan lolos. Namun, jika kurang dari itu, kamu harus mengulangi ujian ini. Tidak banyak yang berhasil melewati ujianku.",
        "Baiklah, Skibidi. Mari mulai ujiannya.",
        "Aku berharap yang terbaik untukmu, Budi. Jangan buat aku kecewa. Jika kamu berhasil, kamu akan melihat pintu masuk kebun ajaib."
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

        
        avatars = new GameObject[] { budiAvatar, skibidiAvatar, budiAvatar, skibidiAvatar, budiAvatar, skibidiAvatar, budiAvatar, skibidiAvatar, budiAvatar, skibidiAvatar, budiAvatar, skibidiAvatar };

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
        skibidiObject.GetComponent<SpriteRenderer>().enabled = false;
        skibidiObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.SetActive(false);  
    }
}