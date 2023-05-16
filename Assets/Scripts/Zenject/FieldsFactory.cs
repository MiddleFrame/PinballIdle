using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FieldsFactory : IFieldsFactory
{
    private static Field[] _fields;
    private static List<Field> _fieldsObjects;
    private readonly DiContainer _diContainer;
    public static int FieldsCount => _fields.Length;
    
    public FieldsFactory(DiContainer diContainer)
    {
        _diContainer = diContainer;
        _fieldsObjects = new List<Field>();
    }

    public void Load()
    {
        _fields = Resources.LoadAll<Field>("Fields");
    }
    
    public void InstantiateField(int fieldNumber, Transform marker)
    {
        _fieldsObjects.Add(_diContainer.InstantiatePrefab(_fields[fieldNumber].gameObject, marker.position,
            Quaternion.identity, marker).GetComponent<Field>().Construct(_fields[fieldNumber]));
        
        if (!Managers.FieldManager.fields.isOpen[fieldNumber])
        {
            _fieldsObjects[fieldNumber].gameObject.SetActive(false);
        }
    }
    
    public static Field GetField(int fieldNumber)
    {
        return _fieldsObjects[fieldNumber];
    }
}