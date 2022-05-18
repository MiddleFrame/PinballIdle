using System.Collections;
using Controllers;
using Managers;
using Shop;
using UnityEngine;

public class LetsScript : MonoBehaviour
{
    [SerializeField]
    private Transform _tRing;

    [SerializeField]
    private int _numField;

    private SpriteRenderer _srRing;

    [SerializeField]
    private GameObject _sprite;

    public static int exp = 1;

    private const int FORCE = 4;

    [SerializeField]
    private int _pointLet = 1;

    private Color _defaultColor;

    [SerializeField]
    private AudioSource As;

    private Coroutine _anim;

    private void Start()
    {
        if (_tRing != null)
        {
            _srRing = _tRing.gameObject.GetComponent<SpriteRenderer>();
        }

        _defaultColor = _sprite.GetComponent<SpriteRenderer>().color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        long _point;
        if (ChallengeManager.IsStartChallenge[_numField])
        {
            if (ChallengeManager.progress.countCompleteChallenge[_numField] == 2 )
            {

                _point = ChallengeTwo();
            }
            else if (ChallengeManager.progress.countCompleteChallenge[_numField] == 3)
            {
                _point =  ChallengeThree(collision.gameObject.GetComponent<BallsChallenge>());
            }
            else if (ChallengeManager.progress.countCompleteChallenge[_numField] == 4)
            {
                _point = (ChallengeTwo() * ChallengeThree(collision.gameObject.GetComponent<BallsChallenge>())) switch
                {
                    9 => 3,
                    -9 => -3,
                    _ => ChallengeTwo() * ChallengeThree(collision.gameObject.GetComponent<BallsChallenge>())
                };
            }
            else
            {
                _point = _pointLet;
                ChallengeManager.progress.currentProgressChallenge[_numField]++;
                if (FieldManager.currentField == _numField)
                    ChallengeManager.Instance.ChangeTextAndFill();
            }
            if (ChallengeManager.progress.currentProgressChallenge[_numField] >=
                (ChallengeManager.progress.countCompleteChallenge[_numField] + 1) * 1000)
            {
                ChallengeManager.Instance.CompleteChallenge(_numField);
            }
        }
        else
        {
            _point = PlayerDataController.playerStats.lvl[_numField] * _pointLet *
                     DefaultBuff.grade.pointOnBit[_numField] * RewardPoint.hitMultiply[_numField];
            PlayerDataController.PointSum += _point;
            PlayerDataController.AddExp(_numField, exp);
        }

        if (_numField == FieldManager.currentField)
            As.Play();
        if (Setting.settings.exNum && (_numField == FieldManager.currentField || FieldManager.currentField == -1))
            ShowNumber(collision.contacts[0], GameManager.NormalSum(_point));
        if (_anim == null && _tRing != null)
        {
            _sprite.GetComponent<SpriteRenderer>().color = new Color32(0xDA, 0xFE, 0xFF, 0xFF);

            _anim = StartCoroutine(Anim());
        }
        else
        {
            _sprite.GetComponent<SpriteRenderer>().color = new Color32(0xDA, 0xFE, 0xFF, 0xFF);
            _anim = StartCoroutine(ChangeColor());
        }

        if (Setting.settings.vibration)
        {
            Vibration.Vibrate(10);
        }

        collision.rigidbody.AddForce(-collision.contacts[0].normal * FORCE, ForceMode2D.Impulse);
    }

    private int ChallengeTwo()
    {
        if (Random.Range(0, 1f) >= 0.85f)
        {
            ChallengeManager.progress.currentProgressChallenge[_numField]++;
            if (FieldManager.currentField == _numField)
                ChallengeManager.Instance.ChangeTextAndFill();
            return _pointLet;
                    
        }

        if (ChallengeManager.progress.currentProgressChallenge[_numField] <= 1) return 0;
        ChallengeManager.progress.currentProgressChallenge[_numField]--;

        if (FieldManager.currentField == _numField)
            ChallengeManager.Instance.ChangeTextAndFill();
        return -_pointLet;
    }

    private int ChallengeThree(BallsChallenge ball)
    {
        return ball.timeOnField > 5 ? 0 : _pointLet;
    }
    private IEnumerator Anim()
    {
        StartCoroutine(Ring());
        Vector3 _vec = new Vector3(0.03f, 0.03f, 0);
        float _startScale = _sprite.transform.localScale.x;
        float _endScale = _startScale * 1.3f;
        while (_sprite.transform.localScale.x < _endScale)
        {
            _sprite.transform.localScale += _vec;
            yield return null;
        }

        while (_sprite.transform.localScale.x > _startScale)
        {
            _sprite.transform.localScale -= _vec;
            yield return null;
        }

        StartCoroutine(ChangeColor());
    }

    private IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(0.02f);
        _sprite.GetComponent<SpriteRenderer>().color = _defaultColor;
    }

    private IEnumerator Ring()
    {
        Color _start = _srRing.color;
        Vector3 _startVector = _tRing.localScale;
        Color _c = new Color(0, 0, 0, 0.008f);
        Vector3 _vec = new Vector3(0.003f, 0.003f, 0);
        while (_srRing.color.a > 0.5f)
        {
            _srRing.color -= _c;
            _tRing.localScale += _vec;
            yield return null;
        }

        _tRing.localScale = _startVector;
        _srRing.color = _start;
        _anim = null;
    }

    private static void ShowNumber(ContactPoint2D cp2d, string point)
    {
        var _text = Instantiate(GameManager.Text, new Vector2(cp2d.point.x, cp2d.point.y), new Quaternion());
        _text.GetComponent<TextMesh>().text = "+" + point;
        _text.GetComponent<TextMesh>().color = ThemeManager.instance.themes[ThemeManager.currentTheme].textColor;
    }
}