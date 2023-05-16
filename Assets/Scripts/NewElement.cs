using System.Collections;
using Managers;
using UnityEngine;

public class NewElement : MonoBehaviour
{
    [SerializeField]
    private int _field;

    public static bool isBallInElement;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (FieldManager.currentField == _field)
                isBallInElement = true;
            StartCoroutine(KickBall(col));
        }
    }

    private IEnumerator KickBall(Component col)
    {
        int point = (int) (Shop.DefaultBuff.grade.pointOnBit[_field] * Shop.DefaultBuff.grade.multiply[_field]);
        for (int i = 0; i < 5; i++)
        {
            Controllers.PlayerDataController.PointSum += point;
            if (FieldManager.currentField == _field)
                isBallInElement = true;
            LetsScript.ShowNumber(transform.position, point.ToString());
            yield return new WaitForSeconds(1f);
        }

        col.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500f, 1000f));
        if (FieldManager.currentField == _field)
            isBallInElement = false;
    }
}