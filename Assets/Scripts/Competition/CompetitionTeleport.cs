using System;
using System.Collections;
using Controllers;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Competition
{
    public class CompetitionTeleport : MonoBehaviour
    {
        public GameObject centerSpawn;
        public GameObject spawnPoint;
        public GameObject[] balls;
        public TrailRenderer[] _ballsTrail;
        public float angle = 0.2f;
        public float speed;
        public float radius;
        public int fieldMultiply = 1;
        public float restrictions = 1.35f;
        private int _a = 1;
        public int field;

        private void Awake()
        {
            _ballsTrail = new TrailRenderer[balls.Length];
            for (int _ball = 0; _ball < balls.Length; _ball++)
            {
                _ballsTrail[_ball] = balls[_ball].GetComponent<TrailRenderer>();
            }

           
        }

        private void Start()
        {
            if (field == 0)
            {
                ChangeTrail(SkinShopController.CurrentTrail);
            }
            else
            {
                ChangeTrail(Random.Range(0, 1f) > 0.6 ? 0 : Random.Range(1, 8));
            }
            StartCoroutine(Spawn());
        }

        private void Update()
        {
            if (CompetitionManager.isWinners[field]) return;
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
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.SetActive(false);
                Statistics.stats.lostBalls++;
                StartCoroutine(Spawn());
            }
             else if (collision.gameObject.CompareTag("FieldBall"))
             {
                 int _i = field;
                 _i = _i==8?0:_i+1;
                 CompetitionManager.instance._teleports[_i].Spawn(collision.gameObject);
                 collision.gameObject.GetComponent<TrailRenderer>().Clear();
             }
        }
        private void Spawn(GameObject ball)
        {
            ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            ball.GetComponent<Rigidbody2D>().angularVelocity = 0f;
            ball.transform.position = spawnPoint.transform.position;
            ball.GetComponent<BallsChallenge>().timeOnField = 0;
            ball.SetActive(true);
        }
        private void ChangeTrail(int trail)
        {
            foreach (var _trail in _ballsTrail)
            {
                _trail.colorGradient = GameManager.trails[trail];
            }


            if (trail == 0)
            {
                foreach (var _ball in balls)
                {
                    _ball.GetComponent<SpriteRenderer>().color =
                        ThemeManager.instance.themes[ThemeManager.currentTheme].textColor;
                }
            }
            else
            {
                foreach (var _ball in balls)
                {
                    _ball.GetComponent<SpriteRenderer>().color = GameManager.ballColor[trail];
                }
            }
        }

        private IEnumerator Spawn()
        {
            //  var _child = balls[0].GetComponentsInChildren<TrailRenderer>();

            if (CompetitionManager.isWinners[field]) yield break;

            for (int _j = 0; _j <= CompetitionManager.balls[field]; _j++)
            {
                if (balls[_j].activeSelf) continue;

                /*foreach (var _t in _child)
            {
                _t.GetComponent<TrailRenderer>().Clear();
            }*/
                balls[_j].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                balls[_j].GetComponent<Rigidbody2D>().angularVelocity = 0f;
                balls[_j].transform.localPosition = spawnPoint.transform.localPosition;
                balls[_j].SetActive(true);
                _ballsTrail[_j].Clear();
                yield return new WaitForSeconds(0.8f);
            }
        }
    }
}