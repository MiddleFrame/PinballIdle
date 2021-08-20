using System.Collections;
using UnityEngine;

public class Experemental : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer sr;
    float  alpha = 1f;
    void Start()
    {
       
        sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        alpha -= Time.deltaTime;
        transform.position += new Vector3(0, Time.deltaTime, 0);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
        if (sr.color.a <= 0)
            Destroy(gameObject);
    }
    
}
