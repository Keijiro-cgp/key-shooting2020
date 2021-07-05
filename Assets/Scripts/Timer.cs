using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static float time = 0f;
    public int time_i = 0;
    private int c;

    private static string minutes;
    private static string seconds;
    private static string comma;

    [SerializeField]
    private Text text_sc;

    public float acceleration = 1f;
    private int accel_count = 0;
    private float accel_span = 20f;

    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private Text ready_text;
    private Animator animator;

    private bool is_ready = true;

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip pi_1;
    [SerializeField]
    private AudioClip pi_2;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(Ready());

        time = 0;
        c = (int)((time - time_i) * 100);
        minutes = (time_i / 60).ToString("D2");
        seconds = (time_i % 60).ToString("D2");
        comma = c.ToString("D2");

        text_sc.text = minutes + ":" + seconds + ":" + comma;
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_ready)
        {
            time += Time.deltaTime;
            time_i = (int)Mathf.Floor(time);
            c = (int)((time - time_i) * 100);
            minutes = (time_i / 60).ToString("D2");
            seconds = (time_i % 60).ToString("D2");
            comma = c.ToString("D2");

            text_sc.text = minutes + ":" + seconds + ":" + comma;

            if (time - (accel_count * accel_span) > accel_span)
            {
                acceleration += 0.1f / acceleration;
                accel_count++;
            }
        }
    }

    public static string TimerText()
    {
        return minutes + ":" + seconds + ":" + comma;
    }

    private IEnumerator Ready()
    {
        animator = canvas.GetComponent<Animator>();
        ready_text.text = "";
        animator.Play("ready");
        float t = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        while (t < 4)
        {
            t = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (t >= 3)
            {
                if (ready_text.text != "GO!") audioSource.PlayOneShot(pi_2, 1);
                ready_text.text = "GO!";
            } else if(t >= 2)
            {
                if (ready_text.text != "1") audioSource.PlayOneShot(pi_1, 1);
                ready_text.text = "1";
            } else if(t >= 1)
            {
                if (ready_text.text != "2") audioSource.PlayOneShot(pi_1, 1);
                ready_text.text = "2";
            } else if(t >= 0)
            {
                if (ready_text.text != "3") audioSource.PlayOneShot(pi_1, 1);
                ready_text.text = "3";
            }
            yield return null;
        }
        animator.speed = 0;
        is_ready = false;
    }
}
