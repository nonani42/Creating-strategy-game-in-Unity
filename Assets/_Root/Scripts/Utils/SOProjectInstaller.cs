using UnityEngine;
using Utils;
using Zenject;

[CreateAssetMenu(fileName = "SOProjectInstaller", menuName = "Installers/SOProjectInstaller")]
public class SOProjectInstaller : ScriptableObjectInstaller<SOProjectInstaller>
{
    [SerializeField] private AssetsContext _legacyContext;

    public override void InstallBindings()
    {
        Container.Bind<AssetsContext>().FromInstance(_legacyContext);
    }
}