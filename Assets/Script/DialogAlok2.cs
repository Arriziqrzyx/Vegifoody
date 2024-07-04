using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class DialogAlok2 : MonoBehaviour
{
    public TextMeshProUGUI dialogText; 
    public GameObject budiAvatar; 
    public GameObject alokAvatar; 
    public float typingSpeed = 0.05f; 
    public float initialDelay = 0.5f; 
    public float messageDelay = 1.5f; 
    public GameObject button; 
    public GameObject buttonSkip;
    public GameObject alokObject; 
    public AudioSource typingAudioSource;

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
        "Kereennn!, sebuah materi tentang manfaat sayuran. Terima kasih, Alok. Tapi kenapa kamu selalu membantuku? Pertama kamu memberiku materi macam-macam sayuran. Sekarang kamu memberi materi tentang manfaat sayuran. Siapa kamu sebenarnya?",
        "Sama-sama. Aku membantu kamu karena aku sangat mengapresiasi anak yang rajin, berani, dan pantang menyerah. Kamu memiliki itu semua, Budi. Mengenai siapa diriku, nanti kamu akan tahu sendiri.",
        "Tapi, Alok...",
        "Sudah saatnya aku pergi, Budi. Sebaiknya kamu pelajari materi yang aku berikan ini agar kamu siap bertemu dengan Skibidi. Senang bertemu denganmu, Budi. Semoga kita bertemu lagi."
    };

    private PlayerController playerController;
    private int messageIndex = 0;
    private const string dialog3Key = "Dialog3Passed";

    private void Start()
    {
        dialogText.text = ""; 
        button.SetActive(false); 
        buttonSkip.SetActive(false); 
        playerController = FindObjectOfType<PlayerController>(); 

        if (PlayerPrefs.GetInt(dialog3Key, 0) == 1)
        {
            buttonSkip.SetActive(true);
        }

        StartCoroutine(StartDialog());
    }

    private IEnumerator StartDialog()
    {
        yield return new WaitForSeconds(initialDelay); 

        while (messageIndex < messages.Length)
        {
            // isTyping = true;
            dialogText.text = ""; 

            
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

        
        button.SetActive(true);
    }

    public void OnButtonClick()
    {
        PlayerPrefs.SetInt(dialog3Key, 1);

        playerController.enabled = true;
        alokObject.GetComponent<SpriteRenderer>().enabled = false;
        alokObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.SetActive(false);  
    }

    public void ButtonSkip()
    {
        playerController.enabled = true;
        alokObject.GetComponent<SpriteRenderer>().enabled = false;
        alokObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.SetActive(false); 
        typingAudioSource.Stop(); 
    }
}
