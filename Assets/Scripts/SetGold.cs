using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGold : MonoBehaviour
{
    private Timer timer_sc;
    private int count = 0;
    [SerializeField]
    private GameObject gold_p;
    [SerializeField]
    private float drop_span = 20f;
    private float t = 0;
    [SerializeField]
    private Vector2 field_size;

    [SerializeField]
    private float move_speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        timer_sc = GetComponent<Timer>();
        field_size = new Vector2(3f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject gold;
        t += Time.deltaTime * timer_sc.acceleration;
        if(t >= drop_span)
        {
            count++;
            t = 0;
            gold = Instantiate(gold_p, new Vector2(Random.Range(-0.9f, 0.9f) * field_size.x, field_size.y), Quaternion.identity);
            gold.GetComponent<Rigidbody2D>().velocity = -Vector2.up * move_speed * timer_sc.acceleration;
            Destroy(gold, 5f);
        }
    }
}
