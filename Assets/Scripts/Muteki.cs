using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Muteki : MonoBehaviour
{
    private LifeManager LM_sc;

    private bool is_muteki = false;

    private Timer timer_sc;

    [SerializeField]
    private Text muteki_text;
    [SerializeField]
    private Text press_text;

    private SpriteRenderer SR_sc;

    private Color first_color_sr;
    private Color first_color_tx;

    [SerializeField]
    private float length = 8f;

    // Start is called before the first frame update
    void Start()
    {
        SR_sc = transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>();
        timer_sc = Camera.main.GetComponent<Timer>();
        LM_sc = GetComponent<LifeManager>();
        first_color_sr = SR_sc.color;
        first_color_tx = muteki_text.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_muteki)
        {
            //SR_sc.color = first_color_sr;
            if (LM_sc.life > 1)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartCoroutine(AwakeMuteki());
                }
                muteki_text.color = new Color(1, 1, 1, 1);
                press_text.color = new Color(1, 1, 1, 1);
            }
            else
            {
                muteki_text.color = first_color_tx;
                press_text.color = first_color_tx;
            }
        }
        else
        {
            Color color;
            color.r = 0.5f + (0.5f * Mathf.Sin(timer_sc.time * 6f));
            color.g = 0.5f + (0.5f * Mathf.Sin((timer_sc.time * 6f) + (Mathf.PI / 3f * 2f)));
            color.b = 0.5f + (0.5f * Mathf.Sin((timer_sc.time * 6f) + (Mathf.PI / 3f * 4f)));
            color.a = 1;

            muteki_text.color = color;
            press_text.color = color;
            SR_sc.color = color;
        }
    }

    private IEnumerator AwakeMuteki()
    {
        gameObject.layer = 8;  // 8:無敵レイヤー
        is_muteki = true;
        LM_sc.Muteki();
        yield return new WaitForSeconds(length);
        gameObject.layer = 0;  //0:Defaultレイヤー
        is_muteki = false;
    }
}
