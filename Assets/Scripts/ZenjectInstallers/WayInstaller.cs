using Dreamteck.Splines;
using Zenject;

public class WayInstaller : MonoInstaller
{
    public SplineComputer SplineComputer;
    
    public override void InstallBindings() => Bind();
    
    private void Bind()
    {
        Container.BindInstance(SplineComputer)
            .AsSingle()
            .NonLazy();
    }
}