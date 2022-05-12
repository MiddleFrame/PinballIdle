using System;
using Managers;
using UnityEngine;

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
}