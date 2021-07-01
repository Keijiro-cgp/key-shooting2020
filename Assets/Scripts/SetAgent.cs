using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAgent : MonoBehaviour
{
    private Timer timer_sc;
    private int count = 0;

    [SerializeField]
    private GameObject agent_p;
    [SerializeField]
    private GameObject key_p;
    [SerializeField]
    private float key_speed = 10f;

    [SerializeField]
    private float drop_span = 20f;
    private float t = 0;
    [SerializeField]
    private Vector2 field_size;

    [SerializeField]
    private float move_speed = 3f;

    [SerializeField]
    private float shoot_delay = 2f;

    [SerializeField]
    private float shot_spread = 60f;

    [SerializeField]
    private AudioClip shot_sound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        timer_sc = GetComponent<Timer>();
        field_size = new Vector2(3f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime * timer_sc.acceleration;
        if (t >= drop_span)
        {
            count++;
            t = 0;
            Generate();
        }
    }

    private void Generate()
    {
        int n;
        GameObject agent;
        n = Random.Range(0, 2);  // 0,1をランダムで選ぶ
        if (n == 0) n = -1;  // 1:右側に配置 / -1:左側に配置

        agent = Instantiate(agent_p,
            new Vector2((field_size.x + 1) * n, Random.Range(-0.9f, 0.9f) * field_size.y), Quaternion.identity);

        agent.transform.localScale = new Vector3(-n, 1, 1);  // 右側配置時左向き、左側配置時右向き
        audioSource = agent.GetComponent<AudioSource>();
        //agent.GetComponent<Rigidbody2D>().velocity = -Vector2.up * move_speed * timer_sc.acceleration;
        StartCoroutine(ShootKey(agent, n));
        Destroy(agent, 5f);
        StartCoroutine(IntoScene(agent, n));
    }

    private IEnumerator ShootKey(GameObject agent, int n)
    {
        Transform muzzle_tf;
        muzzle_tf = agent.transform.GetChild(0);   // Agentの0番目の子オブジェクトがMuzzle
        yield return new WaitForSeconds(2f);
        GameObject[] key = new GameObject[3];
        audioSource.PlayOneShot(shot_sound, 0.8f);
        for(int i = 0; i < 3; i++)
        {
            key[i] = Instantiate(key_p, muzzle_tf.position, Quaternion.Euler(0, 0, shot_spread * (i - 1) + (90 * -n)));

            key[i].GetComponent<Rigidbody2D>().velocity
                = -key[i].transform.up * key_speed * timer_sc.acceleration;
        }
    }

    private IEnumerator IntoScene(GameObject agent, int n)
    {
        for(int t = 0; t < 60; t++)
        {
            agent.transform.Translate(1f / 60f * -n, 0, 0);
            yield return null;
        }
        yield return new WaitForSeconds(3);
        for (int t = 0; t < 120; t++)
        {
            agent.transform.Translate(-1f / 60f * -n, 0, 0);
            yield return null;
        }
    }
}
