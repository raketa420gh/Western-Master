using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(CharacterAnimation))]

public class Player : Character
{
    [BoxGroup("Data"), SerializeField] private PlayerData _data;

    private CharacterBehaviour _behaviour;
    private ICharacterAnimation _animation;

    private void Awake()
    {
        _animation = GetComponent<ICharacterAnimation>();
    }

    private void Start() => Setup();

    private void Setup()
    {
        base.Setup(_data);
        
        _behaviour = new CharacterBehaviour(this);
        _behaviour.InitializeStateMachine();
    }
}