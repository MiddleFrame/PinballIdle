using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

public class Field : MonoBehaviour
{
    public GameObject[] circles;
    public GameObject stroke;
    public TextMesh levelText;
    public TextMesh ballsText;

    public int CountCircles => circles.Length - 1;
    
    [field: SerializeField]
    public GameObject _allFieldElement;

    public Teleport spawnTeleport;

    [SerializeField]
    private GameObject _stopper;
    private void Awake()
    {
        FieldManager.openAllField += () => { _allFieldElement.SetActive(true); };
        FieldManager.openOneField += () => { _allFieldElement.SetActive(false); };
    }

    public void MakeTriple()
    {
        circles[Random.Range(0, circles.Length)].GetComponent<LetsScript>().MakeTriple();
    }

    public void OpenCircle(int circle)
    {
        circles[circle].SetActive(true);
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