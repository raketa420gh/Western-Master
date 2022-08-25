using UnityEngine;

[RequireComponent(typeof(Animator))]

public class CharacterAnimation : MonoBehaviour, ICharacterAnimation
{
    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    public void PlayIdle() => _animator.SetTrigger(AnimationParameterNames.Idle);

    public void PlayAim() => _animator.SetTrigger(AnimationParameterNames.Aim);
}