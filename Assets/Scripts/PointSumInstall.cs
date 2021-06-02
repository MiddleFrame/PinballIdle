using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointSumInstall : MonoBehaviour
{
   
   public void Update()
   {
        GetComponent<Text>().text = "" + GameManager.Point;
   }
}
