using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class x2area : MonoBehaviour
{
    public Text PointNow;
   public bool x2isWork = false;
    public SpriteRenderer image;
    int field = 1;
                                // Start is called before the first frame update
  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerF2" && !x2isWork)
        {
            GameManager.PointsNow[field] *= 2;
            PointNow.text ="+"+ GameManager.NormalSum(GameManager.PointsNow[field]);
            image.color = Color.yellow;
            x2isWork = true;
        }
    }

}
