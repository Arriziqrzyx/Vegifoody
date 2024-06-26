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
}
