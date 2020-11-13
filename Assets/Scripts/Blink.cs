using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    private SpriteRenderer SR_sc;
    private float span = 0.1f;
    private float t = 0f;

    // Start is called before the first frame update
    void Start()
    {
        SR_sc = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if(t >= span)
        {
            SR_sc.color *= -1;
            t = 0f;
        }
    }
}
