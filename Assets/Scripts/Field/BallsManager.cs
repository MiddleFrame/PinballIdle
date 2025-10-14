using System.Collections;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
    public static StatsBall balls;

    [SerializeField]
    private GameObject[] _fieldBalls;

    private void Start()
    {
        if (balls.fieldBallCount > 0)
        {
            if (balls.fieldBallCount > 9)
            {
                balls.fieldBallCount = 9;
            }

            StartCoroutine(firstSpawnFieldBall());
        }
    }

    private IEnumerator firstSpawnFieldBall()
    {
        for (int _i = 0; _i < balls.fieldBallCount; _i++)
        {
            yield return new WaitForSeconds(1f);
            _fieldBalls[_i].SetActive(true);
        }
    }
}


public class StatsBall
{
    public int fieldBallCount;
}