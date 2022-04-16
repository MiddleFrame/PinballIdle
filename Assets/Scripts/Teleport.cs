using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    public GameObject[] MoneyAbility;
    public BallsManager Bm;
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
    public int fieldMulty =1;
    public float restrictions = 1.35f;
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
        var y = fieldMulty*Mathf.Sin(angle * speed) * radius+centerSpawn.transform.position.y;
        spawnPoint.transform.position = new Vector3(x, y,spawnPoint.transform.position.z);
        if ((x < centerSpawn.transform.position.x - restrictions && a>0)|| (x> centerSpawn.transform.position.x+ restrictions && a<0))
            a = -a;
      
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        collision.gameObject.SetActive(false);
        if (collision.gameObject.layer == 9)
            collision.gameObject.layer = 7;
        if (collision.gameObject.GetComponent<SpriteRenderer>().color != new Color32(0xFF, 0x89, 0x12, 0xFF))
        {
            Statistics.stats.lostBalls++;
            if (Statistics.stats.lostBalls == 1000 && !BallsManager.isOpenBall[2])
            {
                BallsManager.isOpenBall[2] = true;
                
            }
            if (GameManager.choosenBall == 2 && collision.gameObject.GetComponent<Mainball>())
                collision.gameObject.GetComponent<SpriteRenderer>().color = new Color32(0xFF, 0x89, 0x12, 0xFF);
        }
        else
        {
            collision.collider.enabled = false;
            StartCoroutine(Phenix(collision.gameObject));
            Mainball.isPhenix = false;
            
        }

        StartCoroutine(Spawn());
        
    }

    IEnumerator Phenix(GameObject Go)
    {
        Go.GetComponent<Rigidbody2D>().gravityScale = 0;
        while (Go.transform.position.y < spawnPoint.transform.position.y-0.1f)
        {
                Go.transform.position = Vector3.MoveTowards(Go.transform.position, spawnPoint.transform.position, 10f * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
           Go.GetComponent<SpriteRenderer>().color = Color.black;
        Go.GetComponent<CircleCollider2D>().enabled = true;
        Go.GetComponent<Rigidbody2D>().gravityScale = 1;
     
    }
    IEnumerator Spawn()
    {
        var child = mainballs[0].GetComponentsInChildren<TrailRenderer>();
      
        for (int j = 0; j <= i[field]; j++)
        {
            if (!mainballs[j].activeSelf)
            {
                if (j == 0)
                    for (int h = 0; h < child.Length; h++)
                    {
                        child[h].GetComponent<TrailRenderer>().Clear();
                    }
                mainballs[j].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                mainballs[j].GetComponent<Rigidbody2D>().angularVelocity = 0f;
                mainballs[j].transform.localPosition = spawnPoint.transform.localPosition;
                mainballs[j].SetActive(true);

                yield return new WaitForSeconds(0.8f);
            }
        }
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.isTrigger = false;
      // OnCollisionEnter2D(collision.gameObject.GetComponent<Collision2D>());
    }
    /* static public void Ressed()
     {
         mainballsstatic[i].transform.position = SpawnPoint.transform.position;
         mainballsstatic[i].SetActive(true);
     }
  */
}
