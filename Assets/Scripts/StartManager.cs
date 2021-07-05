using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    [SerializeField]
    private Image black;

    private bool pushed = false;

    [SerializeField]
    private Text score_tx;

    [SerializeField]
    private AudioClip push_sound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(Fadein());
        if (PlayerPrefs.HasKey("SCORE_TEXT"))
        {
            score_tx.text = "ハイスコア:" + PlayerPrefs.GetString("SCORE_TEXT");
        }
        else
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        audioSource.PlayOneShot(push_sound, 0.9f);
        if(!pushed) StartCoroutine(Fadeout(1));  // 1:ゲームシーン
    }

    public void PlayOpening()
    {
        audioSource.PlayOneShot(push_sound, 0.9f);
        if (!pushed) StartCoroutine(Fadeout(2));  // 2:オープニングシーン
    }

    private IEnumerator Fadeout(int scene_num)
    {
        //Debug.Log("Fade out start");
        pushed = true;
        for(float i = 0; i < 1f; )
        {
            black.color = new Color(0, 0, 0, i);
            yield return null;
            i += Time.deltaTime;
        }
        //Debug.Log("Fade out end");
        SceneManager.LoadScene(scene_num);
    }

    private IEnumerator Fadein()
    {
        //Debug.Log("Fade in start");
        for (float i = 1f; i > 0; )
        {
            //Debug.Log("i = " + i);
            black.color = new Color(0, 0, 0, i);
            yield return null;
            i -= Time.deltaTime;
        }
        black.color = new Color(0, 0, 0, 0); ;
        //Debug.Log("Fade in end");
    }
}
