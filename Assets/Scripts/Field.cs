using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

public class Field : MonoBehaviour
{
    public GameObject[] circles;
    public GameObject stroke;
    public TextMesh levelText;
    public TextMesh ballsText;

    [field: SerializeField]
    public GameObject _allFieldElement;

    private void Awake()
    {
        FieldManager.openAllField += () => { _allFieldElement.SetActive(true); };
        FieldManager.openOneField += () => { _allFieldElement.SetActive(false); };
    }

    public void MakeTriple()
    {
        circles[Random.Range(0, circles.Length)].GetComponent<LetsScript>().MakeTriple();
    }
}