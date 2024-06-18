using UnityEngine;

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

    void Start()
    {
        lompat = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
}
