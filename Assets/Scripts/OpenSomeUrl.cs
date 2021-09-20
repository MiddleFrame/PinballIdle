using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSomeUrl : MonoBehaviour
{
    public void MyUrl(string url)
    {
        Application.OpenURL(url);
    }
}
