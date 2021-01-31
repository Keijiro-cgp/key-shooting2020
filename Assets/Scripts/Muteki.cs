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

    [SerializeField]
    private Color color_ready;
    [SerializeField]
    private Color color_disable;

    [SerializeField]
    private Slider gage_slider;
    [SerializeField]
    private Image gage_image;

    [SerializeField]
    private float length = 8f;

    // Start is called before the first frame update
    void Start()
    {
        SR_sc = transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>();
        //timer_sc = Camera.main.GetComponent<Timer>();
        LM_sc = GetComponent<LifeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_muteki)
        {
            //SR_sc.color = first_color_sr;
            gage_slider.value = 1;
            gage_image.color = new Color(1, 1, 1, 1);
            if (LM_sc.life > 1)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartCoroutine(AwakeMuteki());
                }
                muteki_text.color = color_ready;
                //press_text.color = new Color(1, 1, 1, 1);
            }
            else
            {
                muteki_text.color = color_disable;
                //press_text.color = first_color_tx;
            }
        }
        else
        {
            Color color;
            color.r = 0.5f + (0.5f * Mathf.Sin(Timer.time * 6f));
            color.g = 0.5f + (0.5f * Mathf.Sin((Timer.time * 6f) + (Mathf.PI / 3f * 2f)));
            color.b = 0.5f + (0.5f * Mathf.Sin((Timer.time * 6f) + (Mathf.PI / 3f * 4f)));
            color.a = 1;

            //muteki_text.color = color;

            gage_image.color = color;
            //press_text.color = color;
            SR_sc.color = color;
        }
    }

    private IEnumerator AwakeMuteki()
    {
        ScoreManager.AddCountMuteki();
        gameObject.layer = 8;  // 8:無敵レイヤー
        is_muteki = true;
        LM_sc.Muteki();
        //yield return new WaitForSeconds(length);
        for(float f = 0; f <= length; f += Time.deltaTime)
        {
            gage_slider.value = Mathf.Lerp(1, 0, f / length);
            yield return null;
        }
        gameObject.layer = 0;  //0:Defaultレイヤー
        is_muteki = false;
    }
}
