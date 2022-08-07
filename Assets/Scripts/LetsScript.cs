using System.Collections;
using Competition;
using Controllers;
using Managers;
using Shop;
using UnityEngine;

public class LetsScript : MonoBehaviour
{
    [SerializeField]
    private Transform[] _tRing;

    [SerializeField]
    private int _numField;

    private SpriteRenderer[] _srRing;

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

    public static bool isCompetitive = false;
    private int _animNum;

    private void Start()
    {
        if (isCompetitive && _numField>0)
            _animNum = CompetitionManager.patterns[_numField-1].patterns.Length % 4;
        if (_tRing != null && _tRing.Length>0)
        {
            _srRing = new SpriteRenderer[_tRing.Length];
            for (int _i = 0; _i < _tRing.Length; _i++)
            {
                _srRing[_i] = _tRing[_i].gameObject.GetComponent<SpriteRenderer>();
            }
        }

        _defaultColor = _sprite.GetComponent<SpriteRenderer>().color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        long _point;
        if (isCompetitive)
        {
            _point = _pointLet * CompetitionManager.upgrades[_numField] *
                     (CompetitionManager.isBuff[_numField] ? 2 : 1) *
                     (CompetitionManager.isUpgradeBuff[_numField] ? 2 : 1);
            CompetitionManager.AddPoint((int) _point, _numField);
            goto Anim;
        }

        if (ChallengeManager.IsStartChallenge[_numField])
        {
            if (ChallengeManager.progress.countCompleteChallenge[_numField] == 2)
            {
                _point = ChallengeTwo();
                ChallengeManager.progress.currentProgressChallenge[_numField] += (int) _point;

                if (FieldManager.currentField == _numField)
                    ChallengeManager.Instance.ChangeTextAndFill();
            }
            else if (ChallengeManager.progress.countCompleteChallenge[_numField] == 3)
            {
                _point = ChallengeThree(collision.gameObject.GetComponent<BallsChallenge>());
                ChallengeManager.progress.currentProgressChallenge[_numField] += (int) _point;

                if (FieldManager.currentField == _numField)
                    ChallengeManager.Instance.ChangeTextAndFill();
            }
            else if (ChallengeManager.progress.countCompleteChallenge[_numField] == 4)
            {
                _point = (ChallengeTwo() * ChallengeThree(collision.gameObject.GetComponent<BallsChallenge>())) switch
                {
                    9 => 3,
                    -9 => -3,
                    _ => ChallengeTwo() * ChallengeThree(collision.gameObject.GetComponent<BallsChallenge>())
                };
                ChallengeManager.progress.currentProgressChallenge[_numField] += (int) _point;

                if (FieldManager.currentField == _numField)
                    ChallengeManager.Instance.ChangeTextAndFill();
            }
            else
            {
                _point = _pointLet;
                ChallengeManager.progress.currentProgressChallenge[_numField] += _pointLet;
                if (FieldManager.currentField == _numField)
                    ChallengeManager.Instance.ChangeTextAndFill();
            }

            if (ChallengeManager.progress.currentProgressChallenge[_numField] >= 1000)
            {
                ChallengeManager.Instance.CompleteChallenge(_numField);
            }
        }
        else
        {
            _point = PlayerDataController.playerStats.lvl[_numField] * _pointLet *
                     DefaultBuff.grade.pointOnBit[_numField] * RewardPoint.hitMultiply[_numField] *
                     SkinShopController.buymentX2;
            PlayerDataController.PointSum += _point;
            PlayerDataController.AddExp(_numField, exp);
        }

        Anim:
        if (_numField == FieldManager.currentField)
            As.Play();

        if (Setting.settings.exNum &&
            (_numField == FieldManager.currentField || FieldManager.currentField == -1 || isCompetitive))
            ShowNumber(collision.contacts[0],
                _point == 0 ? "0" : (_point > 0 ? "+" : "") + GameManager.NormalSum(_point),
                (isCompetitive && _numField == 0) ? 4 : 1);
        if (_anim == null && _tRing != null && _tRing.Length>0)
        {
            _sprite.GetComponent<SpriteRenderer>().color = new Color32(0xDA, 0xFE, 0xFF, 0xFF);

            _anim = StartCoroutine(Anim());
        }
        else
        {
            _sprite.GetComponent<SpriteRenderer>().color = new Color32(0xDA, 0xFE, 0xFF, 0xFF);
            _anim = StartCoroutine(ChangeColor());
        }

        collision.rigidbody.AddForce(-collision.contacts[0].normal * FORCE, ForceMode2D.Impulse);
    }

    private int ChallengeTwo()
    {
        if (Random.Range(0, 1f) < 0.85f)
        {
            return _pointLet;
        }

        if (ChallengeManager.progress.currentProgressChallenge[_numField] <= 1) return 0;
        return -_pointLet;
    }

    private int ChallengeThree(BallsChallenge ball)
    {
        return ball.timeOnField < 3 ? 0 : _pointLet;
    }

    private IEnumerator Anim()
    {
        StartCoroutine(Ring(isCompetitive
            ? _numField == 0 ? SkinShopController.CurrentAnim : _animNum
            : SkinShopController.CurrentAnim));
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

    private IEnumerator Ring(int currAnim)
    {
        Color _start = _srRing[currAnim].color;
        Vector3 _startVector = _tRing[currAnim].localScale;
        Color _c = new Color(0, 0, 0, 0.008f);
        Vector3 _vec = new Vector3(0.003f, 0.003f, 0);
        while (_srRing[currAnim].color.a > 0.5f)
        {
            _srRing[currAnim].color -= _c;
            _tRing[currAnim].localScale += _vec;
            yield return null;
        }

        _tRing[currAnim].localScale = _startVector;
        _srRing[currAnim].color = _start;
        _anim = null;
    }

    private static void ShowNumber(ContactPoint2D cp2d, string point, int scale = 1)
    {
        var _text = Instantiate(isCompetitive ? CompetitionManager.Text : GameManager.Text,
            new Vector2(cp2d.point.x, cp2d.point.y), new Quaternion());
        _text.transform.localScale *= scale;
        _text.GetComponent<TextMesh>().text = point;
        _text.GetComponent<TextMesh>().color = ThemeManager.instance.themes[ThemeManager.currentTheme].textColor;
    }
}