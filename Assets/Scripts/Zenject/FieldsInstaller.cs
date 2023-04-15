using Managers;
using UnityEngine;
using Zenject;

public class FieldsInstaller : MonoInstaller, IInitializable
{
    [SerializeField]
    private Transform[] _fieldsMarkers;

    public override void InstallBindings()
    {
        BindInitializeInterfaces();
        
        BindFields();
    }

    private void BindInitializeInterfaces()
    {
        Container
            .BindInterfacesTo<FieldsInstaller>()
            .FromInstance(this)
            .AsSingle();
    }

    private void BindFields()
    {
        Container
            .Bind<IFieldsFactory>()
            .To<FieldsFactory>()
            .AsSingle();
    }

    public void Initialize()
    {
        var factory = Container.Resolve<IFieldsFactory>();
        factory.Load();
        for (int fieldNumber = 0; fieldNumber < FieldManager.fields.isOpen.Length; fieldNumber++)
        {
            factory.InstantiateField(fieldNumber, _fieldsMarkers[fieldNumber]);
            
        }
    }
}

public interface IFieldsFactory
{
    public void Load();
    public void InstantiateField(int fieldNumber, Transform marker);

}