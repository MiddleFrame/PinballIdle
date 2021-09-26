using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    public x2area[] x2Areas;
    public GameObject centerSpawn;
    public GameObject spawnPoint;
    public static GameObject SpawnPoint;
    public static GameObject[] mainballsstatic= new GameObject[6];
    public  GameObject[] mainballs;
    public Text PointQuest1;
    public Text TimeQuest2;
    public Text point;
    public Text pointField2;
    public Text PointSum;
    public Text[] PointsNow;
    public float angle=0.2f;
    public float speed;
    public float radius;
    static public int[] i = new int[] { 0, 0 }; //Кол-во шаров
    int a=1;
    public int field = 0;
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
        spawnPoint.transform.position = new Vector3(x, y,spawnPoint.transform.position.z);
        if ((x < centerSpawn.transform.position.x - 1.35f && a>0)|| (x> centerSpawn.transform.position.x+1.35f && a<0))
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
                    GameManager.PointSum+= GameManager.PointsNow[field];
                    GameManager.PointsNow[field] = 0;
                    PointsNow[field].text = "+0";
                    PointSum.text = GameManager.NormalSum(GameManager.PointSum);
                    if (field == 0)
                    {
                        GameManager.Point = 0;
                        point.text = "" + 0;
                    }
                    else if(field == 1)
                    {
                        for (int i = 0; i < x2Areas.Length; i++)
                        {
                            x2Areas[i].image.color = Color.white;
                            x2Areas[i].x2isWork = false;
                        }
                        GameManager.PointField2 = 0;
                        pointField2.text = "" + 0;
                    }
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
        for (int j = 0; j <= i[field]; j++)
        {
            mainballs[j].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            mainballs[j].GetComponent<Rigidbody2D>().angularVelocity = 0f;
            mainballs[j].transform.localPosition = spawnPoint.transform.localPosition;
            mainballs[j].SetActive(true);
            yield return new WaitForSeconds(0.8f);
        }
    }

   /* static public void Ressed()
    {
        mainballsstatic[i].transform.position = SpawnPoint.transform.position;
        mainballsstatic[i].SetActive(true);
    }
 */
}
