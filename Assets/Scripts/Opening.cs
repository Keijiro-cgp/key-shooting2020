using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour
{
    private float time = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var info = gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        time = info.normalizedTime;
        if(time >= 1f)
        {
            SceneManager.LoadScene(0);  // タイトル画面に移る
        }
    }
}
