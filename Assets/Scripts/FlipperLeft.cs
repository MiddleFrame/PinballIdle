using Controllers;
using Managers;
using Shop;
using UnityEngine;

public class FlipperLeft : MonoBehaviour
{
    public AudioSource As;
    public int Field;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!DefaultBuff.autoMod[Field] || other.gameObject.layer != 7) return;
        if (FieldManager.currentField == Field)
            As.Play();
        FlipperController.RightOrLeft[Field] = false;
        FlipperController.IsFlipper[Field] = true;
        Invoke(nameof(DisableFlipper),1f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (DefaultBuff.autoMod[Field] && collision.gameObject.layer == 7)
        {
            FlipperController.IsFlipper[Field] = false;
        }
    }

    private void DisableFlipper()
    {
        FlipperController.IsFlipper[Field] = false;
    }
}