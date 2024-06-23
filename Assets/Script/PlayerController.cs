using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int kecepatan; // Kecepatan gerakan horizontal
    public int kekuatanlompat; // Kekuatan lompatan
    public bool balik; // Menyimpan informasi arah hadap karakter
    public int pindah; // Menyimpan arah gerakan karakter
    private Rigidbody2D lompat; // Komponen Rigidbody2D untuk lompatan
    public bool tanah; // Menyimpan informasi apakah karakter berada di tanah
    public LayerMask targetlayer; // Layer yang dikategorikan sebagai tanah
    public Transform deteksitanah; // Posisi deteksi tanah
    public float jangkauan; // Jarak deteksi tanah
    private Animator anim; // Komponen Animator untuk mengatur animasi karakter
    public int heart; // Jumlah nyawa karakter
    public TMP_Text info_heart; // Komponen TextMeshPro untuk menampilkan jumlah nyawa
    public bool play_again = false; // Menyimpan informasi apakah karakter dapat memulai dari checkpoint terakhir
    public GameObject over;
    Vector2 play; // Posisi checkpoint terakhir

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

        if (Input.GetKey(KeyCode.D))
        {
            Move(1);
        }
        else if (Input.GetKey(KeyCode.A))
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

        info_heart.text = "Nyawa : " + heart.ToString();

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
            Debug.Log("lompat");
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
}
