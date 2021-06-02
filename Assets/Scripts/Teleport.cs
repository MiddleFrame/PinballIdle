using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    public GameObject spawnPoint;
    public Text point;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.rigidbody.velocity = Vector2.zero;
        collision.rigidbody.angularVelocity = 0f;
        GameManager.Point = 0;
        point.text =""+ 0;
        collision.gameObject.transform.position = spawnPoint.transform.position;
    }
 
}
