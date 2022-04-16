using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LetsScript : MonoBehaviour
{
    [SerializeField]
    private Transform _tRing;

    private SpriteRenderer _srRing;
    [SerializeField]
    private GameObject _sprite;
    public static int exp = 1;
    
     int force = 4;
    [SerializeField]
    int _pointLet = 1;
    Color defaultColor;
    [SerializeField]
    private AudioSource As;

    private Coroutine anim;
    private void Start()
    {

        if (_tRing != null)
        {

            _srRing = _tRing.gameObject.GetComponent<SpriteRenderer>();
        }
        defaultColor = _sprite.GetComponent<SpriteRenderer>().color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        long point = PlayerDataController.Lvl* _pointLet * StandartBuff.grade.pointOnBit[0]* InterstitialAndReward.hitMultiply;
        PlayerDataController.PointSum += point;
        As.Play();
        PlayerDataController.Exp++;
        if (Setting.settings.exNum)
            ExperemetNumber(collision.contacts[0], GameManager.NormalSum(point));
        if (anim == null && _tRing != null)
        {
            _sprite.GetComponent<SpriteRenderer>().color = new Color32(0xDA, 0xFE, 0xFF, 0xFF);

            anim = StartCoroutine(Anim());
        }
        else
        {
            _sprite.GetComponent<SpriteRenderer>().color = new Color32(0xDA, 0xFE, 0xFF, 0xFF);
            anim = StartCoroutine(ChangeColor());
        }
        if (Setting.settings.vibro)
        {
            Vibration.Vibrate(1);
        }
        collision.rigidbody.AddForce(-collision.contacts[0].normal * force, ForceMode2D.Impulse);
    }

    public IEnumerator Anim()
    {
        StartCoroutine(Ring());
        Vector3 vec = new Vector3(0.03f, 0.03f, 0);

        while (_sprite.transform.localScale.x < 1.3f)
        {
            _sprite.transform.localScale += vec;
            yield return null;
        }
        while (_sprite.transform.localScale.x > 1f)
        {
            _sprite.transform.localScale -= vec;
            yield return null;
        }
        StartCoroutine(ChangeColor());

    }
    public IEnumerator ChangeColor()
    {

        yield return new WaitForSeconds(0.02f);
        _sprite.GetComponent<SpriteRenderer>().color = defaultColor;
    }

    private IEnumerator Ring()
    {
        Color start = _srRing.color;
        Vector3 startvec = _tRing.localScale;
        Color c = new Color(0, 0, 0, 0.008f);
        Vector3 vec = new Vector3(0.003f, 0.003f, 0);
        while (_srRing.color.a > 0.5f)
        {
            _srRing.color -= c;
            _tRing.localScale += vec;
            yield return null;
        }
        _tRing.localScale = startvec;
        _srRing.color = start;
        anim = null;
    }
    public void ExperemetNumber(ContactPoint2D cp2d, string point)
    {
        var text = Instantiate(GameManager.Text, new Vector2(cp2d.point.x, cp2d.point.y), new Quaternion());
        text.GetComponent<TextMesh>().text = "+" + point;
    }


}
