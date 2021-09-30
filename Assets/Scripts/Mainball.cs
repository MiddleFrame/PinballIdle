using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mainball : MonoBehaviour
{

    public static bool isPhenix = true;
    public bool isChost = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Finish")
        {
            gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
    }

    // Update is called once per frame
}
