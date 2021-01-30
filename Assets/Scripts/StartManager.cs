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

    // Start is called before the first frame update
    void Start()
    {
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
        if(!pushed) StartCoroutine(Fadeout(1));  // 1:ゲームシーン
    }

    public void PlayOpening()
    {
        if (!pushed) StartCoroutine(Fadeout(2));  // 2:オープニングシーン
    }

    private IEnumerator Fadeout(int scene_num)
    {
        pushed = true;
        for(float i = 0; i < 1f; )
        {
            black.color = new Color(0, 0, 0, i);
            yield return null;
            i += Time.deltaTime;
        }
        SceneManager.LoadScene(scene_num);
    }
}
