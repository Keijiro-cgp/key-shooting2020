using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public int life = 3;
    [SerializeField]
    private GameObject[] image;

    private Rigidbody2D RB2D;

    [SerializeField]
    private float muteki = 2f;

    [SerializeField]
    private SpriteRenderer SR_sc;

    private Image image_sc;

    [SerializeField]
    private bool damaged = false;
    private int flame = 0;

    private Timer timer_sc;

    [SerializeField]
    private AudioClip damage_sound;
    [SerializeField]
    private AudioClip get_gold_sound;
    private AudioSource audioSource;

    public bool is_muteki = false;

    [SerializeField]
    private GameObject gold_drop;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //timer_sc = Camera.main.GetComponent<Timer>();
        //SR_sc = transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>();
        RB2D = GetComponent<Rigidbody2D>();
        for(int i = 0; i < 5; i++)
        {
            if (i < life){
                image[i].SetActive(true);
            } else {
                image[i].SetActive(false);
            }
        }
        image_sc = image[0].GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Color c = new Color(1, 1, 1, 1);
        Color image_color = new Color(1, 1, 1, 1);

        if (damaged)
        {
            flame++;
            flame = flame % 2;
            //Debug.Log(SR_sc.color);
            c.a = flame;
            SR_sc.color = c;
        }
        else
        {
            c = new Color(1, 1, 1, 1);
            SR_sc.color = c;
            flame = 0;
        }

        if (life == 1)
        {
            image_color.a = 0.5f + (0.5f * Mathf.Sin(Timer.time * 6f));
            image_sc.color = image_color;
        }
        else
        {
            image_color = new Color(1, 1, 1, 1);
            image_sc.color = image_color;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Key")
        {
            Destroy(collision.gameObject);
            StartCoroutine(Damaged());
        }

        if (collision.transform.tag == "Gold")
        {
            Destroy(collision.gameObject);
            ScoreManager.AddCountGold();
            audioSource.PlayOneShot(get_gold_sound, 1);
            if (life < 5)
            {
                life++;
                for (int i = 0; i < 5; i++)
                {
                    if (i < life)
                    {
                        image[i].SetActive(true);
                    }
                    else
                    {
                        image[i].SetActive(false);
                    }
                }
            }
        }
    }

    private IEnumerator Damaged()
    {
        audioSource.PlayOneShot(damage_sound, 1);
        Instantiate(gold_drop, transform.position, Quaternion.identity);
        if (life == 1)
        {
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(1f);
            Time.timeScale = 1;
            SceneManager.LoadScene(3);  //リザルトシーンへ
        }
        life--;
        for (int i = 0; i < 5; i++)
        {
            if (i < life)
            {
                image[i].SetActive(true);
            }
            else
            {
                image[i].SetActive(false);
            }
        }

        gameObject.layer = 8;  //8:無敵レイヤー
        damaged = true;
        yield return new WaitForSeconds(muteki);
        damaged = false;
        if(!is_muteki) gameObject.layer = 0;  //0:Defaultレイヤー
    }

    public void Muteki()
    {
        life--;
        for (int i = 0; i < 5; i++)
        {
            if (i < life)
            {
                image[i].SetActive(true);
            }
            else
            {
                image[i].SetActive(false);
            }
        }
    }
}
