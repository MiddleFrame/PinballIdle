using System;
using Managers;
using Shop;
using UnityEngine;
using Random = UnityEngine.Random;

public class Field : MonoBehaviour
{
    public GameObject[] circles;
    public GameObject stroke;
    public TextMesh levelText;
    public TextMesh ballsText;
    private Field _resourceField;

    [field: SerializeField]
    public GameObject _allFieldElement;

    private int _createStopperChange = 20;

    [SerializeField]
    private int number;

    public Teleport spawnTeleport;

    public Field Construct(Field resourceField)
    {
        _resourceField = resourceField;
        return this;
    }

    [SerializeField]
    private GameObject _stopper;

    private void Awake()
    {
        spawnTeleport.SpawnBall += () =>
        {
            if (!DefaultBuff.grade.stopper[number]) return;
            if (Random.Range(0, 100) < _createStopperChange)
            {
                OpenStopper();
            }
            else
            {
                CloseStopper();
            }
        };
        if (DefaultBuff.grade.triple[number]) MakeTriple();
        FieldManager.openAllField += () => { _allFieldElement.SetActive(true); };
        FieldManager.openOneField += () => { _allFieldElement.SetActive(false); };
    }

    public void MakeTriple()
    {
        circles[Random.Range(0, circles.Length)].GetComponent<LetsScript>().MakeTriple();
    }

    public void OpenStopper()
    {
        _stopper.SetActive(true);
    }

    public void CloseStopper()
    {
        _stopper.SetActive(false);
    }
}