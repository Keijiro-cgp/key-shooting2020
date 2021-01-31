using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{

    private Timer timer_sc;
    private float speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        timer_sc = Camera.main.GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -12) transform.position = new Vector2(0, 12f);
        transform.Translate(-Vector2.up * speed * timer_sc.acceleration * Time.deltaTime);
        if (transform.position.y <= -12) transform.position = new Vector2(0, 12f);
    }
}
