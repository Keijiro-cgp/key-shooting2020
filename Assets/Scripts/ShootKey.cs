using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootKey : MonoBehaviour
{
    private Timer timer_sc;
    [SerializeField]
    private float shoot_span = 4f;

    private float t = 0;

    [SerializeField]
    private GameObject key_p;

    [SerializeField]
    private float shoot_speed = 10f;

    [SerializeField]
    private float dead_time = 3f;

    [SerializeField]
    private Vector2 field_size;

    private int count = 0;

    private GameObject player_go;

    private float shoot_range = 1.2f;

    [SerializeField]
    private float warning_time = 0.6f;

    [SerializeField]
    private GameObject warning_p;

    private int shoot_level = 0;

    [SerializeField]
    private int next_level_count = 5;

    [SerializeField]
    private AudioClip warn_sound;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        timer_sc = GetComponent<Timer>();
        field_size = new Vector2(3f, 5f);
        player_go = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime * timer_sc.acceleration;
        if(t >= shoot_span)
        {
            
            t = 0;
            
            if (count == next_level_count)
            {
                shoot_level++;
                next_level_count = next_level_count * 2 + 1;
            }

            if(shoot_level != 0) audioSource.PlayOneShot(warn_sound, 1f); ;

            for (int i = 0; i < shoot_level; i++)
            {
                StartCoroutine(RandomShoot());
            }
            count++;
        }
    }

    private IEnumerator RandomShoot()
    {
        int n = Random.Range(0,4);  //0～3をランダムで選ぶ
        float pos = Random.Range(1.0f, -1.0f) * shoot_range;
        GameObject key, warning;
        Vector2 key_pos = Vector2.zero, warning_pos = Vector2.zero, force_vec = Vector2.zero;
        Quaternion key_rot = Quaternion.Euler(0, 0, 0);

        //Debug.Log("shoot: pos = " + pos);
        switch (n)
        {
            case 0:  //上から
                key_pos = new Vector2(player_go.transform.position.x + pos, field_size.y);
                warning_pos = key_pos - Vector2.up * 0.8f;
                key_rot = Quaternion.Euler(0, 0, 0);
                force_vec = -Vector2.up;
                break;
            case 1:  //右から
                key_pos = new Vector2(field_size.x, player_go.transform.position.y + pos);
                warning_pos = key_pos - Vector2.right * 0.4f;
                key_rot = Quaternion.Euler(0, 0, -90);
                force_vec = -Vector2.right;
                break;
            case 2:  //下から
                key_pos = new Vector2(player_go.transform.position.x + pos, -field_size.y);
                warning_pos = key_pos + Vector2.up * 0.8f;
                key_rot = Quaternion.Euler(0, 0, 180);
                force_vec = Vector2.up;
                break;
            case 3:  //左から
                key_pos = new Vector2(-field_size.x, player_go.transform.position.y + pos);
                warning_pos = key_pos + Vector2.right * 0.4f;
                key_rot = Quaternion.Euler(0, 0, 90);
                force_vec = Vector2.right;
                break;
            default:
                break;
        }
        Debug.Log("" + n + " / " + key_pos);
        warning = Instantiate(warning_p, warning_pos, Quaternion.identity);
        warning.transform.GetChild(0).transform.rotation = key_rot;
        Destroy(warning, warning_time);
        yield return new WaitForSeconds(warning_time);
        key = Instantiate(key_p, key_pos, key_rot);
        key.GetComponent<Rigidbody2D>().velocity = force_vec * shoot_speed * timer_sc.acceleration;
        Destroy(key, dead_time);
    }
}
