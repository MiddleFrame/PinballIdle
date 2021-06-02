using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    public GameObject spawnPoint;
    public Text point;
    public float angle=0.2f;
    public float speed;
    public float radius;
    int a=1;
    private void Update()
    {
        angle += a*Time.deltaTime; 

        var x = Mathf.Cos(angle * speed) * radius+0;
        var y = Mathf.Sin(angle * speed) * radius+1.95f;
        spawnPoint.transform.position = new Vector2(x, y);
        if ((x < -1.35f && a>0)|| (x>1.35f&& a<0))
            a = -a;
      
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.rigidbody.velocity = Vector2.zero;
        collision.rigidbody.angularVelocity = 0f;
        GameManager.Point = 0;
        point.text =""+ 0;
        collision.gameObject.transform.position = spawnPoint.transform.position;
    }
 
}
