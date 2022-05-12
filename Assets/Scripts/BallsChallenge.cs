using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsChallenge : MonoBehaviour
{
    public int timeOnField;

    public IEnumerator StartTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            timeOnField++;
        }
    }
}
