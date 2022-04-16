
using UnityEngine;

public class Experemental : MonoBehaviour
{
    private TextMesh _tm;
    float  _alpha = 1f;
    void Start()
    {
       
        _tm = GetComponent<TextMesh>();
    }

    private void FixedUpdate()
    {
        _alpha -= Time.deltaTime;
        transform.position += new Vector3(0, Time.deltaTime, 0);
        _tm.color = new Color(_tm.color.r, _tm.color.g, _tm.color.b, _alpha);
        if (_tm.color.a <= 0)
            Destroy(gameObject);
    }
    
}
