﻿using System.Collections;
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

    private bool _isTriple;

    public static bool isCompetitive = false;
    private int _animNum;

    private void Start()
    {
        if (isCompetitive && _numField > 0)
            _animNum = CompetitionManager.patterns[_numField - 1].patterns.Length % 4;
        if (_tRing != null && _tRing.Length > 0)
        {
            _srRing = new SpriteRenderer[_tRing.Length];
            for (int _i = 0; _i < _tRing.Length; _i++)
            {
                _srRing[_i] = _tRing[_i].gameObject.GetComponent<SpriteRenderer>();
            }
        }

        _defaultColor = GameManager.instance.defaultColor;
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
            switch (ChallengeManager.progress.countCompleteChallenge[_numField])
            {
                case 2:
                {
                    _point = ChallengeTwo();
                    ChallengeManager.progress.currentProgressChallenge[_numField] += (int) _point;
                    if (ChallengeManager.progress.currentProgressChallenge[_numField] < 0)
                    {
                        ChallengeManager.progress.currentProgressChallenge[_numField] = 0;
                    }

                    if (FieldManager.currentField == _numField)
                        ChallengeManager.Instance.ChangeTextAndFill(_numField);
                    break;
                }
                case 3:
                {
                    _point = ChallengeThree(collision.gameObject.GetComponent<BallsChallenge>());
                    ChallengeManager.progress.currentProgressChallenge[_numField] += (int) _point;

                    if (FieldManager.currentField == _numField)
                        ChallengeManager.Instance.ChangeTextAndFill(_numField);
                    break;
                }
                case 4:
                {
                    _point =
                        (ChallengeFour() * ChallengeThree(collision.gameObject.GetComponent<BallsChallenge>())) switch
                        {
                            9 => 3,
                            -9 => -3,
                            _ => ChallengeFour() * ChallengeThree(collision.gameObject.GetComponent<BallsChallenge>())
                        };
                    ChallengeManager.progress.currentProgressChallenge[_numField] += (int) _point;
                    if (ChallengeManager.progress.currentProgressChallenge[_numField] < 0)
                    {
                        ChallengeManager.progress.currentProgressChallenge[_numField] = 0;
                    }

                    if (FieldManager.currentField == _numField)
                        ChallengeManager.Instance.ChangeTextAndFill(_numField);
                    break;
                }
                default:
                {
                    _point = _pointLet;
                    ChallengeManager.progress.currentProgressChallenge[_numField] += _pointLet;
                    if (FieldManager.currentField == _numField)
                        ChallengeManager.Instance.ChangeTextAndFill(_numField);
                    break;
                }
            }

            if (ChallengeManager.progress.currentProgressChallenge[_numField] >=
                1000f * ((ChallengeManager.progress.countCompleteChallenge[_numField] + 1) % 5 + 1))
            {
                ChallengeManager.Instance.CompleteChallenge(_numField);
            }
        }
        else
        {
            _point = (long) ((1 + 0.1f * (PlayerDataController.playerStats.lvl[_numField] - 1)) * _pointLet *
                             DefaultBuff.grade.pointOnBit[_numField] * RewardPoint.hitMultiply[_numField] *
                             SkinShopController.buyElementX2 * DefaultBuff.grade.multiplyPoint[_numField]);
            PlayerDataController.PointSum += _point;
            PlayerDataController.AddExp(_numField, exp);
        }

        if (_isTriple && !isCompetitive)
        {
            _point *= 10;
            _isTriple = false;
            if (_tRing.Length > 0)
                _sprite.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite =
                    GameManager.instance.defaultShadowBall;
            if (QuestManager.progress[_numField + 1].isComplete[0])
                GameManager.instance.fields[_numField].MakeTriple();
        }

        Anim:
        if (_numField == FieldManager.currentField)
            As.Play();

        if (Setting.settings.exNum &&
            (_numField == FieldManager.currentField || FieldManager.currentField == -1 || isCompetitive))
            ShowNumber(collision.contacts[0].point,
                _point == 0 ? "0" : (_point > 0 ? "+" : "") + GameManager.NormalSum(_point),
                (isCompetitive && _numField == 0) ? 4 : 1);
        if (_anim == null && _tRing != null && _tRing.Length > 0)
        {
            _sprite.GetComponent<SpriteRenderer>().color = new Color32(0xDA, 0xFE, 0xFF, 0xFF);

            _anim = StartCoroutine(Anim());
        }
        else
        {
            _sprite.GetComponent<SpriteRenderer>().color = new Color32(0xDA, 0xFE, 0xFF, 0xFF);
            _anim = StartCoroutine(ChangeColor());
        }

        if (Setting.settings.vibration && _numField == FieldManager.currentField)
        {
            Vibration.Vibrate(10);
        }

        collision.rigidbody.AddForce(-collision.contacts[0].normal * FORCE, ForceMode2D.Impulse);
    }

    private int ChallengeTwo()
    {
        if (Random.Range(0, 1f) < 0.7f)
        {
            return _pointLet;
        }

        if (ChallengeManager.progress.currentProgressChallenge[_numField] <= 1) return 0;
        return -_pointLet;
    }

    private int ChallengeFour()
    {
        if (Random.Range(0, 1f) < 0.3f)
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

    public void MakeTriple()
    {
        _isTriple = true;
        _sprite.GetComponent<SpriteRenderer>().color = GameManager.instance.tripleColor;
        if (_tRing.Length > 0)
            _sprite.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameManager.instance.goldShadowBall;
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
        _sprite.GetComponent<SpriteRenderer>().color = _isTriple ? GameManager.instance.tripleColor : _defaultColor;
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

    public static void ShowNumber(Vector2 cp2d, string point, int scale = 1)
    {
        var _text = Instantiate(isCompetitive ? CompetitionManager.Text : GameManager.Text,
            cp2d, new Quaternion());
        _text.transform.localScale *= scale;
        _text.GetComponent<TextMesh>().text = point;
    }
}