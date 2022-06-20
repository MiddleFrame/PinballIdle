using Managers;
using Shop;
using UnityEngine;

namespace Controllers
{
    public class FlipperController : MonoBehaviour
    {
        public static bool[] IsFlipper { get; } = {false, false, false, false, false, false, false, false, false};
        private FlipperLeft _flipperLeft;
        private HingeJoint2D _hjRight;
        public HingeJoint2D hjLeft;
        public static bool[] RightOrLeft { get; } = {false, false, false, false, false, false, false, false, false};
        public AudioSource As;

        private void Start()
        {
            _flipperLeft = hjLeft.gameObject.GetComponent<FlipperLeft>();
            _hjRight = GetComponent<HingeJoint2D>();
        }

        private void Audio()
        {
            if (As != null && FieldManager.currentField == _flipperLeft.Field)
                As.Play();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (IsFlipper[_flipperLeft.Field])
            {
                if (RightOrLeft[_flipperLeft.Field])
                    _hjRight.useMotor = true;
                else
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
            if (!DefaultBuff.autoMod[_flipperLeft.Field] || other.gameObject.layer != 7) return;
            Audio();
            RightOrLeft[_flipperLeft.Field] = true;
            IsFlipper[_flipperLeft.Field] = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (DefaultBuff.autoMod[_flipperLeft.Field] && collision.gameObject.layer == 7)
            {
                IsFlipper[_flipperLeft.Field] = false;
            }
        }
    }
}