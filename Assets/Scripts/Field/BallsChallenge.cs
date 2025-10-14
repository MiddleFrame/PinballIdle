using System.Collections;
using UnityEngine;

public class BallsChallenge : MonoBehaviour
{
    public int timeOnField;

    private void Start()
    {
        StartCoroutine(StartTime());
    }

    private IEnumerator StartTime()
    {
        yield return new WaitForSecondsRealtime(1f);
        timeOnField++;
    }
}
