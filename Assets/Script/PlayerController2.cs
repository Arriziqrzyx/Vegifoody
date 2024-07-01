using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController2 : MonoBehaviour
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
    [SerializeField] AudioSource jumpAudio;
    private bool Button_kiri; 
    private bool Button_kanan;
    private bool Button_atas;

    void Start()
    {
        lompat = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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

        if (Input.GetKeyDown(KeyCode.Space) || Button_atas)
        {
            Jump();
        }

        if ((pindah > 0 && !balik) || (pindah < 0 && balik))
        {
            Flip();
        }

        anim.SetBool("Jump", !tanah);
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

    IEnumerator loadMiniGames(string Name)
    {
        SceneManager.LoadScene(Name, LoadSceneMode.Additive);
        yield return new WaitUntil(() => SceneManager.GetSceneByName(Name).isLoaded);
    }

    public void tekan_kiri()
    {
        Button_kiri = true; 
    }

    public void Lepas_kiri()
    {
        Button_kiri = false; 
    }

    public void tekan_kanan()
    {
        Button_kanan = true; 
    }

    public void lepas_kanan()
    {
        Button_kanan = false; 
    }

    public void tekan_atas()
    {
        Button_atas = true;
    }

    public void lepas_atas()
    {
        Button_atas = false;
    }

}
