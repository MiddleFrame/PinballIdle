using Managers;
using Shop;
using UnityEngine;
using Random = UnityEngine.Random;

public class Field : MonoBehaviour
{
    public Let[] lets;
    public GameObject stroke;
    public TextMesh levelText;
    public TextMesh ballsText;
    private Field _resourceField;

    [field: SerializeField]
    public GameObject _allFieldElement;

    private int _createStopperChance = 20;

    [SerializeField]
    private int number;

    public Teleport spawnTeleport;

    [SerializeField]
    private GameObject _stopper;
    

    internal Field Init()
    {
        spawnTeleport.SpawnBall += () =>
        {
            if (!DefaultBuff.grade.stopper[number]) return;
            if (Random.Range(0, 100) < _createStopperChance)
            {
                OpenStopper();
            }
            else
            {
                CloseStopper();
            }
        };
        foreach (var _let in lets)
        {
            _let.Init(1);
        }
        if (DefaultBuff.grade.triple[number]) MakeTriple();
        FieldManager.openAllField += () => { _allFieldElement.SetActive(true); };
        FieldManager.openOneField += () => { _allFieldElement.SetActive(false); };
        return this;
    }
    public void MakeTriple()
    {
        lets[Random.Range(0, lets.Length)].MakeTriple();
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