using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int kecepatan; 
    public int kekuatanlompat; 
    public bool balik; 
    public int pindah; 
    private Rigidbody2D lompat; 
    public bool tanah; 
    public LayerMask targetlayer; 
    public Transform deteksitanah; 
    public float jangkauan; 
    private Animator anim; 
    public int heart; 
    public TMP_Text info_heart; 
    public bool play_again = false; 
    public GameObject over;
    Vector2 play; 
    [SerializeField] AudioSource jumpAudio;
    [SerializeField] AudioSource dieAudio;
    [SerializeField] AudioSource checkpointAudio;
    [SerializeField] AudioSource hatiAudio;
    private bool Button_kiri; 
    private bool Button_kanan; 

    void Start()
    {
        play = transform.position;
        lompat = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        over.SetActive(false);
    }

    void Update()
    {
        tanah = Physics2D.OverlapCircle(deteksitanah.position, jangkauan, targetlayer);

        if (Input.GetKey(KeyCode.D) || Button_kanan)
        {
            Move(1);
        }
        else if (Input.GetKey(KeyCode.A) || Button_kiri)
        {
            Move(-1);
        }
        else
        {
            anim.SetBool("Run", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if ((pindah > 0 && !balik) || (pindah < 0 && balik))
        {
            Flip();
        }

        anim.SetBool("Jump", !tanah);

        info_heart.text = "Nyawa Tersisa : " + heart.ToString();

        if (heart < 1)
        {
            gameObject.SetActive(false);
            Debug.Log("Player Wafat");

            over.SetActive(true);
        }

        if (play_again)
        {
            transform.position = play;
            play_again = false;
            dieAudio.Play();
        }
    }

    void Move(int direction)
    {
        transform.Translate(Vector2.right * kecepatan * direction * Time.deltaTime);
        pindah = direction;

        if (tanah)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }

    void Jump()
    {
        if (tanah)
        {
            float x = lompat.velocity.x;
            lompat.velocity = new Vector2(x, kekuatanlompat);
            jumpAudio.Play();
        }
    }

    void Flip()
    {
        balik = !balik;
        Vector3 Player = transform.localScale;
        Player.x *= -1;
        transform.localScale = Player;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            play = other.transform.position;
            Debug.Log("Checkpoint");
            StopAllCoroutines();
            checkpointAudio.Play();
        }

        if (other.gameObject.tag == "HatiHati")
        {
            hatiAudio.Play();
        }
    }

    public void RestarLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator loadMiniGames(string Name)
    {
        SceneManager.LoadScene(Name, LoadSceneMode.Additive);
        yield return new WaitUntil(() => SceneManager.GetSceneByName(Name).isLoaded);
    }

    public void tekan_kiri()
    {
        Button_kiri = true; // Ketika ditekan
    }

    public void lepas_kiri()
    {
        Button_kiri = false; // Ketika dilepas
    }

    public void tekan_kanan()
    {
        Button_kanan = true; // Ketika ditekan
    }

    public void lepas_kanan()
    {
        Button_kanan = false; // Ketika dilepas
    }

    public void tekan_lompat()
    {
        Jump();
    }

}
