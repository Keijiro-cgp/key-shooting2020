using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float time = 0f;
    public int time_i = 0;
    private int c;

    private string minutes;
    private string seconds;
    private string comma;

    [SerializeField]
    private Text text_sc;

    public float acceleration = 1f;
    private int accel_count = 0;
    private float accel_span = 20f;

    // Start is called before the first frame update
    void Start()
    {
        c = (int)((time - time_i) * 100);
        minutes = (time_i / 60).ToString("D2");
        seconds = (time_i % 60).ToString("D2");
        comma = c.ToString("D2");

        text_sc.text = minutes + ":" + seconds + ":" + comma;
    }

    // Update is called once per frame
    void Update()
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

    public string TimerText()
    {
        return minutes + ":" + seconds + ":" + comma;
    }
}
