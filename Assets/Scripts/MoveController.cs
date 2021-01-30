using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField]
    private float player_speed = 3f;
    private Vector2 move_limit;

    private float input_x, input_y;

    private LifeManager LM_sc;

    private bool is_muteki = false;

    private Timer timer_sc;

    // Start is called before the first frame update
    void Start()
    {
        timer_sc = Camera.main.GetComponent<Timer>();
        LM_sc = GetComponent<LifeManager>();
        move_limit = new Vector2(3f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        input_x = Input.GetAxis("Horizontal");
        input_y = Input.GetAxis("Vertical");

        if (transform.position.x > move_limit.x)
            transform.position = new Vector2(move_limit.x, transform.position.y);
        if (transform.position.y > move_limit.y)
            transform.position = new Vector2(transform.position.x, move_limit.y);
        if (transform.position.x < -move_limit.x)
            transform.position = new Vector2(-move_limit.x, transform.position.y);
        if (transform.position.y < -move_limit.y)
            transform.position = new Vector2(transform.position.x, -move_limit.y);

        transform.Translate(new Vector3(input_x * player_speed * Time.deltaTime, input_y * player_speed * Time.deltaTime));
    }
}
