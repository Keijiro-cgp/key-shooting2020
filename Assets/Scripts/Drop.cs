using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField]
    private float height = 1;
    [SerializeField]
    private float width = 1;
    [SerializeField]
    private float rot_speed = 360;
    [SerializeField]
    private float life = 1;

    private float t = 0;

    // Start is called before the first frame update
    void Start()
    {
        height *= Random.Range(-1f, 1f) * height;
        width *= Random.Range(0.5f, 1) * width;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.unscaledDeltaTime;
        transform.Rotate(0, 0, rot_speed * Time.unscaledDeltaTime);
        transform.position += new Vector3(width, height * quadratic(t), 0) * Time.unscaledDeltaTime;
        if(t > life)
        {
            Destroy(gameObject);
        }
    }

    float quadratic(float t)
    {
        float result;
        result = 1 - (t * t * 2);
        return result;
    }
}
