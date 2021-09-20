using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour
{

    public bool Statictics=false;
    private Transform EndPoint;
    public Transform StartPoint;
    public GameObject StatisticPanel;

    private void Start()
    {
        EndPoint = transform.parent.transform;
        
        

    }
    void Update()
    {
        if(!Statictics)
            transform.position = Vector3.MoveTowards(transform.position, StartPoint.position, 5f * Time.deltaTime);
        else
            transform.position = Vector3.MoveTowards(transform.position, EndPoint.position, 5f * Time.deltaTime);
       
    }


    public void Stat(bool Stat)
    {
        Statictics = Stat;
    }
}
