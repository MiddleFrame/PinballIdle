using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    float timeChangeColor=0;
    SpriteRenderer sr;
    public AudioClip Ac;
    public AudioSource As;
    public SpriteRenderer[] Sr = new SpriteRenderer[5];
    public static float TimeStoppers = 3;
    static public float coldownStopper = 12;
    static public long costOnGrade = 1000;
    static public long costOnCooldown = 1000;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!isCooldown)
        {
            if (timeChangeColor > 0.1f)
                timeChangeColor -= Time.deltaTime;
            if (timeChangeColor <= 0.1f && (sr.color != Color.white || sr.color != new Color32(0x0E, 0x43, 0x60, 0xFF)))
            {
                timeChangeColor = 0f;
                if (GameManager.DarkTheme)
                    sr.color = new Color32(0x0E, 0x43, 0x60, 0xFF);
                else
                sr.color = Color.white;
            }
        }
       
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isCooldown)
        {
            sr.color = new Color32(0xFF, 0xC9, 0x45, 0xFF);
            timeChangeColor += 1.5f;
            for (int i = 0; i < 5; i++)
            {
                if (Sr[i].color == Color.white || Sr[i].color == new Color32(0x0E, 0x43, 0x60, 0xFF))
                    break;
                if (i == 4)
                {
                   isAllChecked = true;
                    isCooldown = true;
                    Blind(Sr);
                    As.PlayOneShot(Ac);
                }
            }
        }
    }
    static public bool isAllChecked = false;
    static public bool isCooldown= false;
    public void Blind(SpriteRenderer[] Sr)
    {
        for (int i = 0; i < 5; i++)
        {
            if(GameManager.DarkTheme)
                Sr[i].color = new Color32(0x0E, 0x43, 0x60, 0xFF);
            else
            Sr[i].color = Color.white;
           
            StartCoroutine(_blind(Sr[i]));

        }
    }

    IEnumerator _blind(SpriteRenderer sr)
    {
        yield return new WaitForSeconds(0.5f);
        sr.color = new Color32(0xFF, 0xC9, 0x45, 0xFF);
        yield return new WaitForSeconds(0.5f);
        if (GameManager.DarkTheme)
           sr.color = new Color32(0x0E, 0x43, 0x60, 0xFF);
        else
           sr.color = Color.white;
        StartCoroutine(_Stopper());
        StartCoroutine(_Cooldown());
    }
    IEnumerator _Stopper()
    {
        yield return new WaitForSeconds(TimeStoppers);
        isAllChecked = false;
    }

    IEnumerator _Cooldown()
    {
        yield return new WaitForSeconds(coldownStopper);
        isCooldown = false;
    }
}
