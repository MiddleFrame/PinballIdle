using System.Collections;
using Managers;
using UnityEngine;

namespace Competition
{
    public class CompetitionTeleport : MonoBehaviour
    {
        public GameObject centerSpawn;
        public GameObject spawnPoint;
        public GameObject[] balls;
        public float angle = 0.2f;
        public float speed;
        public float radius;
        public int fieldMultiply = 1;
        public float restrictions = 1.35f;
        private int _a = 1;
        public int field;
        private void Start()
        {
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
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.SetActive(false);
                Statistics.stats.lostBalls++;
                StartCoroutine(Spawn());
            }
            
        }

        private IEnumerator Spawn()
        {
            //  var _child = balls[0].GetComponentsInChildren<TrailRenderer>();


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

                yield return new WaitForSeconds(0.8f);
            }
        }

 
    }
}