using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LetsScript : MonoBehaviour
{
    public Text MaxPoint;
    public Text lvltext;
    public Text lvlbuff;
    public Text lvluplevel;
    public Text lvlupbuff;
    public GameObject lvlupPanel;
    public GameManager Gm;
    public Text PointQuest1;
    public int force;
    public Text point;
    public Text pointSum;
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
        As.PlayOneShot(Ac);
        if (!GameManager.isQuestStarted)
        {
            point.text = "" + (++GameManager.Point);
            if (GameManager.Point > GameManager.maximumPoint)
            {
                MaxPoint.text = $"Record:   {GameManager.Point}";
                GameManager.maximumPoint = GameManager.Point;
            }
            GameManager.PointSum += StandartBuff.pointOnBit*GameManager.lvl;
            pointSum.text = GameManager.NormalSum(GameManager.PointSum);
            hitneeded--;
            ProgressBar.fillAmount =(500f* GameManager.lvl- hitneeded) / 500f / GameManager.lvl;
            if (hitneeded==0)
            {
                GameManager.lvl++;
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
            int qch = 0;
            if (GameManager.NumberQuest == 0)
                qch = 30;
            else if (GameManager.NumberQuest == 2|| GameManager.NumberQuest == 3 || GameManager.NumberQuest == 4)
                qch = 20;
            PointQuest1.text = (++GameManager.quest1point)+$"\\{qch}";
            if (GameManager.quest1point == qch)
            {
                Gm.Stop();
                QuestManager.QuestsCompleate[GameManager.NumberQuest] = true;
                QuestCompeate.SetActive(true);
            }
        }
        
        //Вернуться и удалить если хуйня
        if(GameManager.ExNum)
        ExperemetNumber(collision.contacts[0]);
       GetComponent<SpriteRenderer>().color = Color.white;
        
        StartCoroutine(ChangeColor());
        collision.rigidbody.AddForce(-collision.contacts[0].normal * force, ForceMode2D.Impulse);

     

        
    }
 
    
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
