using System.Collections;
using Managers;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject centerSpawn;
    public GameObject spawnPoint;
    public GameObject[] balls;
    private BallsChallenge[] _balls;
    public float angle = 0.2f;
    public float speed;
    public float radius;
    public int fieldMultiply = 1;
    public float restrictions = 1.35f;
    private int _a = 1;
    public int field;

    private void Start()
    {
        _balls = new BallsChallenge[balls.Length];
        for (int _ball = 0; _ball < balls.Length; _ball++)
        {
            _balls[_ball] = balls[_ball].GetComponent<BallsChallenge>();
            StartCoroutine(_balls[_ball].StartTime());
        }
        StartCoroutine(Spawn());
    }

    private void Update()
    {
        angle += _a * Time.deltaTime;

        var _position = centerSpawn.transform.position;
        var _x = Mathf.Cos(angle * speed) * radius + _position.x;
        var _y = fieldMultiply * Mathf.Sin(angle * speed) * radius + _position.y;
        spawnPoint.transform.position = new Vector3(_x, _y, spawnPoint.transform.position.z);
        if ((_x < _position.x - restrictions && _a > 0) || (_x > _position.x + restrictions && _a < 0))
            _a = -_a;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.SetActive(false);
        Statistics.stats.lostBalls++;
        StartCoroutine(Spawn());
    }

/*
    private IEnumerator Phoenix(GameObject go)
    {
        go.GetComponent<Rigidbody2D>().gravityScale = 0;
        while (go.transform.position.y < spawnPoint.transform.position.y - 0.1f)
        {
            go.transform.position =
                Vector3.MoveTowards(go.transform.position, spawnPoint.transform.position, 10f * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }

        go.GetComponent<SpriteRenderer>().color = Color.black;
        go.GetComponent<CircleCollider2D>().enabled = true;
        go.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
*/
    private IEnumerator Spawn()
    {
        //  var _child = balls[0].GetComponentsInChildren<TrailRenderer>();


        for (int _j = 0; _j <= ChallengeManager.progress.balls[field]; _j++)
        {
            if (balls[_j].activeSelf) continue;

            /*foreach (var _t in _child)
            {
                _t.GetComponent<TrailRenderer>().Clear();
            }*/

            if (ChallengeManager.IsStartChallenge[field] &&
                (ChallengeManager.progress.countCompleteChallenge[field] == 3 ||
                 ChallengeManager.progress.countCompleteChallenge[field] == 4))
            {
                _balls[_j].timeOnField = 0;
            }
            balls[_j].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            balls[_j].GetComponent<Rigidbody2D>().angularVelocity = 0f;
            balls[_j].transform.localPosition = spawnPoint.transform.localPosition;
            balls[_j].SetActive(true);

            yield return new WaitForSeconds(0.8f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.isTrigger = false;
    }
}