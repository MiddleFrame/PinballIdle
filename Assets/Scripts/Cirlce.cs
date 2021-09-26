using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cirlce : MonoBehaviour
{
   
    public static int PointNeed = 10;
    public static int MaxPointNeed= 10;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerF2")
        {
            gameObject.SetActive(false);
            PointNeed = MaxPointNeed;
            GetComponent<SpriteRenderer>().color = new Color32(0xFF, 0xC9, 0x45, 0xFF);
        }
    }
}
