using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    private float high_score;
    private float score;
    private Timer timer_sc;
    private bool is_highscore = false;
    [SerializeField]
    private GameObject[] results;
    [SerializeField]
    private GameObject[] scores;
    [SerializeField]
    private GameObject[] got_golds;
    [SerializeField]
    private GameObject[] mutekis;
    [SerializeField]
    private GameObject[] buttons;

    [SerializeField]
    private float wait_time = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("HIGH_SCORE"))
        {
            high_score = PlayerPrefs.GetFloat("HIGH_SCORE");
        }

        //score = timer_sc.time;

        if(high_score <= score)
        {
            is_highscore = true;
        }

        StartCoroutine(ShowResult());
    }

    private IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(wait_time);
        for(int i = 0; i < 2; i++)
        {
            Vector3 size = results[i].transform.localScale;
            results[i].SetActive(true);
            StartCoroutine(Expand(results[i], size));
        }
        yield return new WaitForSeconds(wait_time);
        yield return StartCoroutine(ShowStatus(scores));
        yield return StartCoroutine(ShowStatus(got_golds));
        yield return StartCoroutine(ShowStatus(mutekis));
        yield return StartCoroutine(Rise(buttons));
    }

    private IEnumerator ShowStatus(GameObject[] status)
    {
        for(int i = 0; i < 2; i++)
        {
            Vector3 size = status[i].transform.localScale;
            status[i].SetActive(true);
            StartCoroutine(Expand(status[i], size));
        }
        yield return new WaitForSeconds(wait_time);
        for(int i = 2; i < 4; i++)
        {
            Vector3 size = status[i].transform.localScale;
            status[i].SetActive(true);
            StartCoroutine(Expand(status[i], size));
        }
        yield return new WaitForSeconds(wait_time);
    }

    private IEnumerator Expand(GameObject go, Vector3 size)
    {
        for(int f = 0; f < 60 * wait_time; f++)
        {
            go.transform.localScale = size * Mathf.Lerp(0, 1, (float)(f / 60f / wait_time));
            yield return null;
        }
    }
    
    private IEnumerator Rise(GameObject[] go)
    {
        float y_start = -25;
        float y_end = 40;

        RectTransform[] rt = new RectTransform[2];
        for(int i = 0; i < 2; i++)
        {
            rt[i] = go[i].GetComponent<RectTransform>();
            go[i].SetActive(true);
            rt[i].anchoredPosition = new Vector2(rt[i].localPosition.x, y_start);
        }

        for(int f = 0; f < 60 * wait_time; f++)
        {
            for(int i = 0; i < 2; i++)
            {
                rt[i].anchoredPosition = new Vector2(rt[i].localPosition.x, Mathf.Lerp(y_start, y_end, (float)(f / 60f / wait_time)));
                //if (i == 0) Debug.Log(Mathf.Lerp(y_start, y_end, (float)(f / 60f / wait_time)));
            }
            yield return null;
        }
    }
}
