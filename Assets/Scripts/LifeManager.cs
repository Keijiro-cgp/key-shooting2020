﻿using System.Collections;
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

    private SpriteRenderer SR_sc;

    private bool damaged = false;
    private int frame = 0;

    private Timer timer_sc;

    // Start is called before the first frame update
    void Start()
    {
        timer_sc = Camera.main.GetComponent<Timer>();
        SR_sc = transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>();
        RB2D = GetComponent<Rigidbody2D>();
        for(int i = 0; i < 5; i++)
        {
            if (i < life){
                image[i].SetActive(true);
            } else {
                image[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            float alpha;
            frame++;
            alpha = frame % 2;

            SR_sc.color = new Color(SR_sc.color.r, SR_sc.color.g, SR_sc.color.b, alpha);
        }
        else
        {
            SR_sc.color = new Color(SR_sc.color.r, SR_sc.color.g, SR_sc.color.b, 1);
            frame = 0;
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
