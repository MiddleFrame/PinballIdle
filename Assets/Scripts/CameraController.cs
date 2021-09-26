using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] MainBall;
    public GameObject[] Field;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.isQuestStarted && GameManager.NumberQuest == 4)
        {
           // gameObject.transform.position = new Vector3(MainBall[FieldManager.CorrectField].transform.position.x, MainBall[FieldManager.CorrectField].transform.position.y, gameObject.transform.position.z);
            Field[FieldManager.CorrectField].transform.position = new Vector3(-MainBall[FieldManager.CorrectField].transform.localPosition.x, -MainBall[FieldManager.CorrectField].transform.localPosition.y, Field[FieldManager.CorrectField].transform.position.z);
        }
    }
}
