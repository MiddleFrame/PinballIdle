using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopper : MonoBehaviour
{
    public   Transform EndPoint;
    public   Transform StartPoint;

    // Update is called once per frame
    void Update()
    {
        if(Checker.isAllChecked)
        transform.position = Vector3.MoveTowards(transform.position, EndPoint.transform.position, 1 *Time.deltaTime);
        else
            transform.position = Vector3.MoveTowards(transform.position, StartPoint.transform.position, 1 * Time.deltaTime);
    }
}
