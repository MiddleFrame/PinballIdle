using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LetsScript : MonoBehaviour
{
    public GameObject MoneyAbility;
    public static int exp = 1;
    public GameObject SafeCircle;
    public Text MaxPoint;
    public Text lvltext;
    public Text lvlbuff;
    public Text lvluplevel;
    public Text lvlupbuff;
    public GameObject lvlupPanel;
    public GameManager Gm;
    public Text[] PointsNow;
    public Text PointQuest1;
    public int force;
    public Text point;
    public int PointLet = 1;
    Color defaultColor;
    public GameObject text;
    public AudioSource As;
    public AudioClip Ac;
    public GameObject QuestCompeate;
    public static int hitneeded=500;
    public Image ProgressBar;
    private void Start()
    {
        defaultColor = GetComponent<SpriteRenderer>().color;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       GameManager.score++;
        // Handheld.Vibrate();
        As.PlayOneShot(Ac);
        if (!GameManager.isQuestStarted && PointLet>0)
        {
            if (GameManager.choosenBall == 3 && MoneyAbility && collision.gameObject.GetComponent<Mainball>())
                MoneyAbility.SetActive(true);
            if (GameManager.ExNum)
                ExperemetNumber(collision.contacts[0]);
            if (collision.gameObject.tag == "PlayerF2")
            {
                if(!Gm.buttonBuyCirlce.activeSelf)
                Cirlce.PointNeed--;
                if(Cirlce.PointNeed == 0)
                {
                    SafeCircle.SetActive(true);
                }
                Gm.pointField2.text = "" + (++GameManager.PointField2);
                GameManager.PointsNow[1] += StandartBuff.pointOnBit[1]* GameManager.lvl * PointLet;
                PointsNow[1].text ="+"+ GameManager.NormalSum(GameManager.PointsNow[1]);
            }
            else if (collision.gameObject.tag == "Player")
            {
                point.text = "" + (++GameManager.Point);
                GameManager.PointsNow[0] += StandartBuff.pointOnBit[0] * GameManager.lvl * PointLet;
                PointsNow[0].text = "+"+GameManager.NormalSum(GameManager.PointsNow[0]);
            }
            if (GameManager.Point > GameManager.maximumPoint)
            {
                MaxPoint.text = $"Record:   {GameManager.Point}";
                GameManager.maximumPoint = GameManager.Point;
            }
            else if(GameManager.PointField2 > GameManager.maximumPoint)
            {

                MaxPoint.text = $"Record:   {GameManager.PointField2}";
                GameManager.maximumPoint = GameManager.PointField2;
            }
            hitneeded-= exp;
            
            ProgressBar.fillAmount =(500f* GameManager.lvl- hitneeded) / 500f / GameManager.lvl;
            if (hitneeded<=0 && !lvlupPanel.activeSelf)
            {
                GameManager.lvl++;
                Gm.LvlUpGems();
                hitneeded = 500 * GameManager.lvl;
                lvltext.text = $" level - {GameManager.lvl}";
                lvlbuff.text = $"x{GameManager.lvl}";
                if(lvlbuff.color == Color.yellow)
                    lvlbuff.text = $"x{2*GameManager.lvl}";
                lvlupPanel.SetActive(true);
                lvluplevel.text = $"{GameManager.lvl} level";
                lvlupbuff.text = $"general \n multiplivate \n now - x{GameManager.lvl}";
            }
        }
        else if(GameManager.NumberQuest == 0||GameManager.NumberQuest == 2|| GameManager.NumberQuest == 3 || GameManager.NumberQuest == 4)
        {
            if (GameManager.ExNum)
                ExperemetNumber(collision.contacts[0]);
            int qch = 0;
            if (GameManager.NumberQuest == 0)
                qch = 30;
            else if (GameManager.NumberQuest == 2|| GameManager.NumberQuest == 3 || GameManager.NumberQuest == 4)
                qch = 20;
            PointQuest1.text = (++GameManager.quest1point)+$"\\{qch}";
            if (GameManager.quest1point == qch)
            {
                Gm.Stop();
                QuestManager.QuestsCompleate[GameManager.NumberQuest+FieldManager.CorrectField*5] = true;
                QuestCompeate.SetActive(true);
            }
        }
        if (PointLet > 0)
        {
            //Вернуться и удалить если хуйня
            GetComponent<SpriteRenderer>().color = Color.white;

            StartCoroutine(ChangeColor());
        }
        collision.rigidbody.AddForce(-collision.contacts[0].normal * force, ForceMode2D.Impulse);
        
     

        
    }
   /* private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Mainball>())
            collision.isTrigger = false;
    }
   */
    public  IEnumerator ChangeColor()
    {
        
        yield return new WaitForSeconds(0.02f);
        GetComponent<SpriteRenderer>().color = defaultColor;
    }


    public void ExperemetNumber(ContactPoint2D cp2d)
    {
       Instantiate(text, new Vector2(cp2d.point.x, cp2d.point.y), new Quaternion());
        
    }

  
}
