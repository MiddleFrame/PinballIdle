using Managers;
using Shop;
using UnityEngine;

namespace Competition
{
    public class FlipperCompetition : MonoBehaviour
    {
        public static bool[] IsFlipper { get; } = {false, false,false,false, false,false,false, false,false};
        private FlipperLeftCompetition _flipperLeft;
        private HingeJoint2D _hjRight;
        public HingeJoint2D hjLeft;
        public static bool[] Right { get; } = {false, false,false,false, false,false,false, false,false};
        public static bool[] Left { get; } = {false, false, false,false,false, false,false,false, false};

        private void Start()
        {
            _flipperLeft = hjLeft.gameObject.GetComponent<FlipperLeftCompetition>();
            _hjRight = GetComponent<HingeJoint2D>();
        }

        

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (IsFlipper[_flipperLeft.Field])
            {
                if (Right[_flipperLeft.Field])
                    _hjRight.useMotor = true;
                if(Left[_flipperLeft.Field])
                    hjLeft.useMotor = true;
            }
            else
            {
                _hjRight.useMotor = false;

                hjLeft.useMotor = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_flipperLeft.Player) return;
            Right[_flipperLeft.Field] = true;
            IsFlipper[_flipperLeft.Field] = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            IsFlipper[_flipperLeft.Field] = false;
            
            Right[_flipperLeft.Field] = false;
        }
    }
}