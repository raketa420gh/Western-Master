using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public Player Player;
    
    public override void InstallBindings() => Bind();
    
    private void Bind()
    {
        Container
            .BindInstance(Player)
            .AsSingle()
            .NonLazy();
    }
}