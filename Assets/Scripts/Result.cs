using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private GameObject highscore_text;

    [SerializeField]
    private float wait_time = 0.4f;

    [SerializeField]
    private float rise_time = 1.8f;

    [SerializeField]
    private Image black;

    private bool pushed = false;

    [SerializeField]
    private AudioClip explosion_sound_1;
    [SerializeField]
    private AudioClip explosion_sound_2;
    [SerializeField]
    private AudioClip push_sound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("HIGH_SCORE"))
        {
            high_score = PlayerPrefs.GetFloat("HIGH_SCORE");
        }
        else
        {
            PlayerPrefs.SetFloat("HIGH_SCORE", Timer.time);
            PlayerPrefs.SetString("SCORE_TEXT", Timer.TimerText());
            is_highscore = true;
        }

        score = Timer.time;

        if(high_score < score)
        {
            is_highscore = true;
            PlayerPrefs.SetFloat("HIGH_SCORE", Timer.time);
            PlayerPrefs.SetString("SCORE_TEXT", Timer.TimerText());
        }

        //逃走時間のテキストを書き換え
        scores[3].GetComponent<Text>().text = Timer.TimerText();

        //無敵使用回数のテキストを書き換え
        mutekis[3].GetComponent<Text>().text = ScoreManager.use_muteki_count + "回";

        //金塊取得回数のテキストを書き換え
        got_golds[3].GetComponent<Text>().text = ScoreManager.got_golds_count + "回";

        StartCoroutine(ShowResult());
    }

    private IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(wait_time);
        audioSource.PlayOneShot(explosion_sound_1, 0.8f);
        for (int i = 0; i < 2; i++)
        {
            Vector3 size = results[i].transform.localScale;
            results[i].SetActive(true);
            StartCoroutine(Expand(results[i], size));
        }
        yield return new WaitForSeconds(wait_time);
        yield return StartCoroutine(ShowStatus(scores));
        if(is_highscore) yield return StartCoroutine(ShowStatus(highscore_text));
        yield return StartCoroutine(ShowStatus(got_golds));
        yield return StartCoroutine(ShowStatus(mutekis));
        yield return StartCoroutine(Rise(buttons));
    }

    private IEnumerator ShowStatus(GameObject[] status)
    {
        audioSource.PlayOneShot(explosion_sound_1, 0.8f);
        for(int i = 0; i < 2; i++)
        {
            Vector3 size = status[i].transform.localScale;
            status[i].SetActive(true);
            
            StartCoroutine(Expand(status[i], size));
        }
        yield return new WaitForSeconds(wait_time);
        audioSource.PlayOneShot(explosion_sound_1, 0.8f);
        for(int i = 2; i < 4; i++)
        {
            Vector3 size = status[i].transform.localScale;
            status[i].SetActive(true);
            
            StartCoroutine(Expand(status[i], size));
        }
        yield return new WaitForSeconds(wait_time);
    }

    private IEnumerator ShowStatus(GameObject status)
    {
        Vector3 size = status.transform.localScale;
        status.SetActive(true);
        audioSource.PlayOneShot(explosion_sound_1, 0.8f);
        StartCoroutine(Expand(status, size));
        yield return new WaitForSeconds(wait_time);
    }

    private IEnumerator Expand(GameObject go, Vector3 size)
    {
        for (float f = 0; f < wait_time; )
        {
            go.transform.localScale = size * Mathf.Lerp(0, 1, 1 / wait_time * f);
            yield return null;
            f += Time.deltaTime;
        }
    }
    
    private IEnumerator Rise(GameObject[] go)
    {
        float y_start = -25;
        float y_end = 40;

        float step;
        step = (y_end - y_start);

        RectTransform[] rt = new RectTransform[2];
        for(int i = 0; i < 2; i++)
        {
            rt[i] = go[i].GetComponent<RectTransform>();
            go[i].SetActive(true);
            rt[i].anchoredPosition = new Vector2(rt[i].localPosition.x, y_start);
        }

        while (rt[0].anchoredPosition.y < y_end)
        {
            for (int i = 0; i < 2; i++)
            {
                rt[i].anchoredPosition += new Vector2(0, step * Time.deltaTime);
            }
            yield return null;
        }

        rt[0].anchoredPosition = new Vector2(rt[0].localPosition.x, y_end);
        rt[1].anchoredPosition = new Vector2(rt[1].localPosition.x, y_end);
    }

    public void ReturnToStart()
    {
        audioSource.PlayOneShot(push_sound, 0.9f);
        if (!pushed) StartCoroutine(Fadeout(0));
    }

    public void Retry()
    {
        audioSource.PlayOneShot(push_sound, 0.9f);
        if (!pushed) StartCoroutine(Fadeout(1));
    }

    private IEnumerator Fadeout(int scene_num)
    {
        pushed = true;
        for (float i = 0; i < 1f;)
        {
            black.color = new Color(0, 0, 0, i);
            yield return null;
            i += Time.deltaTime;
        }
        SceneManager.LoadScene(scene_num);
    }
}
