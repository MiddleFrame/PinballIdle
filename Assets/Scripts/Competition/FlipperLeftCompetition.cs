using Controllers;
using Managers;
using Shop;
using UnityEngine;

namespace Competition
{
    public class FlipperLeftCompetition : MonoBehaviour
    {
        public bool Player;

        public int Field;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (Player)
                return;
            FlipperCompetition.Left[Field] = true;
            FlipperCompetition.IsFlipper[Field] = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (Player) return;
                FlipperCompetition.IsFlipper[Field] = false;
            
                FlipperCompetition.Left[Field] = false;
        }
    }
}