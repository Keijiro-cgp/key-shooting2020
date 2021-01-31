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



    // Start is called before the first frame update
    void Start()
    {
        timer_sc = Camera.main.GetComponent<Timer>();
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
            Debug.Log(SR_sc.color);
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
            image_color.a = 0.5f + (0.5f * Mathf.Sin(timer_sc.time * 6f));
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
        if (life == 1)
        {
            if (PlayerPrefs.HasKey("HIGH_SCORE"))
            {
                if (PlayerPrefs.GetFloat("HIGH_SCORE") < timer_sc.time)
                {
                    PlayerPrefs.SetFloat("HIGH_SCORE", timer_sc.time);
                    PlayerPrefs.SetString("SCORE_TEXT", timer_sc.TimerText());
                }
            }
            else
            {
                PlayerPrefs.SetFloat("HIGH_SCORE", timer_sc.time);
                PlayerPrefs.SetString("SCORE_TEXT", timer_sc.TimerText());
            }
            
            SceneManager.LoadScene(0);
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
        gameObject.layer = 0;  //0:Defaultレイヤー
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
