using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MainBall;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.isQuestStarted && GameManager.NumberQuest == 4)
            gameObject.transform.position =new Vector3( MainBall.transform.position.x, MainBall.transform.position.y, gameObject.transform.position.z);
    }
}
