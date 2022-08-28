using UnityEngine;

[RequireComponent(typeof(Animator))]

public class CharacterAnimation : MonoBehaviour, ICharacterAnimation
{
    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    public void PlayIdle() => _animator.SetTrigger(AnimationParameterNames.Idle);

    public void PlayAim() => _animator.SetTrigger(AnimationParameterNames.Aim);
    
    public void PlayRun() => _animator.SetBool(AnimationParameterNames.Run, true);

    public void StopRun() => _animator.SetBool(AnimationParameterNames.Run, false);
}