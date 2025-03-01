using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public int force;
    Rigidbody2D rigid;
    public int scoreP1;
    public int scoreP2;
    public Text scoreUIP1;
    public Text scoreUIP2;
    GameObject panelSelesai;
    Text txPemenang;
    AudioSource audio;
    public AudioClip paddleHitSound;
    public AudioClip edgeHitSound;
    public AudioClip backgroundMusic;
    private AudioSource backgroundAudioSource;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Vector2 arah = new Vector2(2, 0).normalized;
        rigid.AddForce(arah * force);

        scoreP1 = 0;
        scoreP2 = 0;

        scoreUIP1 = GameObject.Find("score1").GetComponent<Text>();
        scoreUIP2 = GameObject.Find("score2").GetComponent<Text>();

        panelSelesai = GameObject.Find("panelSelesai");
        panelSelesai.SetActive(false);

        audio = GetComponent<AudioSource>();

        GameObject musicObject = new GameObject("BackgroundMusic");
        backgroundAudioSource = musicObject.AddComponent<AudioSource>();
        backgroundAudioSource.clip = backgroundMusic;
        backgroundAudioSource.loop = true;
        backgroundAudioSource.Play();
    }

    void Update()
    {
        scoreUIP1.text = scoreP1.ToString();
        scoreUIP2.text = scoreP2.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Suara untuk pemukul
        if (collision.gameObject.name == "pemukulKiri" || collision.gameObject.name == "pemukulKanan")
        {
            audio.PlayOneShot(paddleHitSound);
            float sudut = (transform.position.y - collision.transform.position.y) * 5f;
            Vector2 arah = new Vector2(rigid.linearVelocity.x, sudut).normalized;
            rigid.linearVelocity = new Vector2(0, 0);
            rigid.AddForce(arah * force * 2);
            return;
        }

        // Suara untuk tepi
        if (collision.gameObject.name == "tepiKanan")
        {
            scoreP1 += 1;
            audio.PlayOneShot(edgeHitSound); // Suara untuk tepi kanan
            ResetBall();
            Vector2 arah = new Vector2(2, 0).normalized;
            rigid.AddForce(arah * force);

            if (scoreP1 == 5)
            {
                panelSelesai.SetActive(true);
                txPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
                txPemenang.text = "Player Kuning \n   Menang!";
                Destroy(gameObject);
                return;
            }
        }

        if (collision.gameObject.name == "tepiKiri")
        {
            scoreP2 += 1;
            audio.PlayOneShot(edgeHitSound); // Suara untuk tepi kiri
            ResetBall();
            Vector2 arah = new Vector2(-2, 0).normalized;
            rigid.AddForce(arah * force);

            if (scoreP2 == 5)
            {
                panelSelesai.SetActive(true);
                txPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
                txPemenang.text = "Player Merah \n   Menang!";
                Destroy(gameObject);
                return;
            }
        }
    }

    void ResetBall()
    {
        transform.localPosition = new Vector2(0, 0);
        rigid.linearVelocity = new Vector2(0, 0);
    }

    void TampilkanScore()
    {
        Debug.Log("Score P1: " + scoreP1 + " Score P2: " + scoreP2);
        scoreUIP1.text = scoreP1 + "";
        scoreUIP2.text = scoreP2 + "";
    }

}
