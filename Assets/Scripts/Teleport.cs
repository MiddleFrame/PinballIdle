using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    public GameObject centerSpawn;
    public GameObject spawnPoint;
    public static GameObject SpawnPoint;
    public static GameObject[] mainballsstatic= new GameObject[6];
    public  GameObject[] mainballs;
    public Text PointQuest1;
    public Text TimeQuest2;
    public Text point;
    public float angle=0.2f;
    public float speed;
    public float radius;
    static public int i = 0; //Кол-во шаров
    int a=1;

    private void Start()
    {
        SpawnPoint = spawnPoint;
        for (int j = 0; j < 6; j++)
        {
            mainballsstatic[j] = mainballs[j];
        }
    }
    private void Update()
    {
        angle += a*Time.deltaTime; 

        var x = Mathf.Cos(angle * speed) * radius+centerSpawn.transform.position.x;
        var y = Mathf.Sin(angle * speed) * radius+centerSpawn.transform.position.y;
        spawnPoint.transform.position = new Vector2(x, y);
        if ((x < -1.35f && a>0)|| (x>1.35f&& a<0))
            a = -a;
      
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.SetActive(false);
        for (int j = 0; j < 6; j++)
        {
            if (mainballs[j].activeSelf)
                break;
            else if (j == 5)
            {
                if (!GameManager.isQuestStarted)
                {
                    GameManager.Point = 0;
                    point.text = "" + 0;
                }
                else if (GameManager.NumberQuest == 0)
                {
                    PointQuest1.text = 0 + "\\30";
                    GameManager.quest1point = 0;
                }
                else if (GameManager.NumberQuest == 1)
                {
                    TimeQuest2.text = "30";
                    GameManager.timeQuest = 30;
                }
                else if (GameManager.NumberQuest == 2 || GameManager.NumberQuest == 3|| GameManager.NumberQuest == 4)
                {
                    PointQuest1.text = 0 + "\\20";
                    GameManager.quest1point = 0;
                }
                StartCoroutine(Spawn());
            }
        }
    }

    IEnumerator Spawn()
    {
        for (int j = 0; j <= i; j++)
        {
            mainballs[j].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            mainballs[j].GetComponent<Rigidbody2D>().angularVelocity = 0f;
            mainballs[j].transform.position = spawnPoint.transform.position;
            mainballs[j].SetActive(true);
            yield return new WaitForSeconds(1f);
        }
    }

   /* static public void Ressed()
    {
        mainballsstatic[i].transform.position = SpawnPoint.transform.position;
        mainballsstatic[i].SetActive(true);
    }
 */
}
