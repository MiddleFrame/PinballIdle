using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Competition
{
    public class Timer : MonoBehaviour
    {
        [SerializeField]
        private Text _timer;
        public static DateTime time; 
        private void Start()
        {
            time = new DateTime();
            StartCoroutine(StartTimer());
        }

        private IEnumerator StartTimer()
        {
            while (true)
            {
                time = time.AddSeconds(1);
                _timer.text = time.ToString("mm:ss");
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
